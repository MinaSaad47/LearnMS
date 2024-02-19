using LearnMS.API.Common;
using Microsoft.Extensions.Options;

namespace LearnMS.API.ThirdParties;

public sealed class VdoService(IHttpClientFactory httpClientFactory)
{

    public async Task<string> CreateVideoAsync()
    {
        var client = httpClientFactory.CreateClient(nameof(VdoService));

        var response = await client.PutAsync("/videos", null);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<UploadingPolicy>() ??
                throw new ApiException(ServerErrors.Internal);

        return result.VideoId;
    }

    public async Task<UploadingPolicy> GetUploadingPolicyAsync(string videoId)
    {
        var client = httpClientFactory.CreateClient(nameof(VdoService));

        var response = await client.PutAsync($"/videos/{videoId}", null);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UploadingPolicy>() ??
                throw new ApiException(ServerErrors.Internal);
    }

    public async Task DeleteVideoAsync(string videoId)
    {
        var client = httpClientFactory.CreateClient(nameof(VdoService));

        var response = await client.DeleteAsync($"/videos?videos={videoId}");

        response.EnsureSuccessStatusCode();
    }
}

public static class VdoServiceExtensions
{
    public static IServiceCollection AddVdoService(this IServiceCollection services, IConfiguration cfg)
    {
        services.Configure<VdoConfig>(cfg.GetSection(VdoConfig.Section));

        services.AddHttpClient(nameof(VdoService), client =>
        {
            var secret = services.BuildServiceProvider().GetRequiredService<IOptions<VdoConfig>>().Value.ApiSecret;

            client.BaseAddress = new Uri("https://api.vdocipher.com");
            client.DefaultRequestHeaders.Add("Authorization", "Apisecret " + secret);
        });

        services.AddSingleton<VdoService>();

        return services;
    }

}

public sealed class VdoConfig
{
    public const string Section = "VdoService";
    public required string ApiSecret { get; init; }
}

public record UploadingPolicy
{
    public string Policy { get; init; }
    public string UploadLink { get; init; }
    public string VideoId { get; init; }
    public string Key { get; init; }
    public string XAmzSignature { get; init; }
    public string XAmzAlgorithm { get; init; }
    public string XAmzCredentials { get; init; }
    public string XAmzDate { get; init; }
}