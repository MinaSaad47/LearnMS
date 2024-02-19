using System.Text.Json.Serialization;
using LearnMS.API.Common;
using Microsoft.Extensions.Options;

namespace LearnMS.API.ThirdParties;

public sealed class VdoService(IHttpClientFactory httpClientFactory)
{

    public async Task<VideoOTP> GetVideoOTPAsync(string videoId)
    {
        var client = httpClientFactory.CreateClient(nameof(VdoService));

        var response = await client.PostAsync($"videos/{videoId}/otp", null);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<VideoOTP>() ?? throw new ApiException(ServerErrors.Internal);
    }

    public async Task<VdoVideo> GetVideoAsync(string videoId)
    {

        var client = httpClientFactory.CreateClient(nameof(VdoService));

        var response = await client.GetAsync($"videos/{videoId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<VdoVideo>() ?? throw new ApiException(ServerErrors.Internal);

    }


    public async Task<string> UploadVideoAsync(Stream fs, string? videoId = null)
    {
        var policy = await GetUploadingPolicyAsync(videoId);

        await UploadVideoAsync(policy, fs);

        return policy.VideoId;
    }

    public async Task DeleteVideoAsync(string videoId)
    {
        var client = httpClientFactory.CreateClient(nameof(VdoService));

        var response = await client.DeleteAsync($"videos?videos={videoId}");

        response.EnsureSuccessStatusCode();
    }

    private async Task<VdoCipherUploadingPolicy> GetUploadingPolicyAsync(string? videoId = null)
    {
        var client = httpClientFactory.CreateClient(nameof(VdoService));

        if (!string.IsNullOrWhiteSpace(videoId))
        {
            await DeleteVideoAsync(videoId);
        }


        var response = await client.PutAsync("videos?title=lesson", null);


        response.EnsureSuccessStatusCode();


        var result = await response.Content.ReadFromJsonAsync<VdoCipherNewUploadingPolicy>() ?? throw new ApiException(ServerErrors.Internal);

        return new VdoCipherUploadingPolicy
        {
            Policy = result.ClientPayload.Policy,
            Key = result.ClientPayload.Key,
            XAmzSignature = result.ClientPayload.XAmzSignature,
            XAmzAlgorithm = result.ClientPayload.XAmzAlgorithm,
            XAmzDate = result.ClientPayload.XAmzDate,
            XAmzCredential = result.ClientPayload.XAmzCredential,
            UploadLink = result.ClientPayload.UploadLink,
            VideoId = result.VideoId,
        };
    }

    private async Task UploadVideoAsync(VdoCipherUploadingPolicy policy, Stream fs)
    {
        var client = httpClientFactory.CreateClient();
        var form = new MultipartFormDataContent
        {
            { new StringContent(policy.Policy), "policy" },
            { new StringContent(policy.Key), "key" },
            { new StringContent(policy.XAmzSignature), "x-amz-signature" },
            { new StringContent(policy.XAmzAlgorithm), "x-amz-algorithm" },
            { new StringContent(policy.XAmzDate), "x-amz-date" },
            { new StringContent(policy.XAmzCredential), "x-amz-credential" },
            { new StringContent("201"), "success_action_status" },
            { new StringContent(""), "success_action_redirect" },
            { new StreamContent(fs), "file", "lesson1.mp4" }
        };
        var response = await client.PostAsync(policy.UploadLink, form);
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

            client.BaseAddress = new Uri("https://www.vdocipher.com/api/");
            client.DefaultRequestHeaders.Add("Authorization", "Apisecret " + secret);
        });

        services.AddSingleton<VdoService>();

        return services;
    }

}

public sealed class VdoConfig
{
    public const string Section = "VdoCipher";
    public required string ApiSecret { get; init; }
}

public record VdoCipherUploadingPolicy
{
    [JsonPropertyName("policy")]
    public string Policy { get; set; } = default!;

    [JsonPropertyName("key")]
    public string Key { get; set; } = default!;

    [JsonPropertyName("x-amz-signature")]
    public string XAmzSignature { get; set; } = default!;

    [JsonPropertyName("x-amz-algorithm")]
    public string XAmzAlgorithm { get; set; } = default!;

    [JsonPropertyName("x-amz-date")]
    public string XAmzDate { get; set; } = default!;

    [JsonPropertyName("x-amz-credential")]
    public string XAmzCredential { get; set; } = default!;

    [JsonPropertyName("uploadLink")]
    public string UploadLink { get; set; } = default!;

    [JsonPropertyName("videoId")]
    public string VideoId { get; set; } = default!;

}
public record VdoCipherNewUploadingPolicy
{
    [JsonPropertyName("clientPayload")]
    public ClientPayload ClientPayload { get; set; } = default!;

    [JsonPropertyName("videoId")]
    public string VideoId { get; set; } = default!;

}

public record ClientPayload
{
    [JsonPropertyName("policy")]
    public string Policy { get; set; } = default!;

    [JsonPropertyName("key")]
    public string Key { get; set; } = default!;

    [JsonPropertyName("x-amz-signature")]
    public string XAmzSignature { get; set; } = default!;

    [JsonPropertyName("x-amz-algorithm")]
    public string XAmzAlgorithm { get; set; } = default!;

    [JsonPropertyName("x-amz-date")]
    public string XAmzDate { get; set; } = default!;

    [JsonPropertyName("x-amz-credential")]
    public string XAmzCredential { get; set; } = default!;

    [JsonPropertyName("uploadLink")]
    public string UploadLink { get; set; } = default!;
}

public sealed record VideoOTP
{
    public string Otp { get; set; } = default!;
    public string PlaybackInfo { get; set; } = default!;
}

public sealed record VdoVideo
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required string Status { get; set; }
}