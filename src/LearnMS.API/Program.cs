using System.Security.Claims;
using System.Text.Json.Serialization;
using LearnMS.API.Common;
using LearnMS.API.Common.EmailService;
using LearnMS.API.Data;
using LearnMS.API.Features;
using LearnMS.API.Features.Administration;
using LearnMS.API.Features.Auth;
using LearnMS.API.Middlewares;
using LearnMS.API.Security;
using LearnMS.API.Security.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Configuration;
using tusdotnet;
using tusdotnet.Interfaces;
using tusdotnet.Stores;

var builder = WebApplication.CreateBuilder(args);
{
    var cfg = builder.Configuration;


    builder.Services.AddHttpContextAccessor();

    builder.Services.AddSpaStaticFiles(x =>
    {
        x.RootPath = "ClientApp";
    });

    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(cfg.GetConnectionString("DefaultConnection")));

    builder.Services.Configure<JwtBearerConfig>(cfg.GetSection(JwtBearerConfig.Section));
    builder.Services.Configure<AdministrationConfig>(cfg.GetSection(AdministrationConfig.Section));
    builder.Services.Configure<EmailConfig>(cfg.GetSection(EmailConfig.Section));

    builder.Services.AddScoped<IEmailService, EmailService>();

    builder.Services.AddFeaturesServices();
    builder.Services.AddSecurityServices();

    builder.Services
        .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer();

    builder.Services
        .AddAuthorization();

}

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// builder.Services.AddControllers().AddJsonOptions(opts =>
// {
//     opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
// });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});


var app = builder.Build();

app.UseSerilogRequestLogging();

await app.InitializeAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(opt => { });

app.MapTus("/api/files", async context =>
{
    await Task.CompletedTask;

    return new()
    {
        MaxAllowedUploadSizeInBytes = 1024 * 1024 * 1024,
        Store = new TusDiskStore(@"/home/mina/Public"),
        Events = new()
        {
            OnCreateCompleteAsync = async eventContext =>
            {
                ITusFile? file = await eventContext.GetFileAsync();


            }
        }
    };
}).AllowAnonymous();

app.UseAuthentication();


app.UseAuthorization();


app.MapControllers();

app.MapWhen(ctx => !ctx.Request.Path.StartsWithSegments("/api"), x =>
{

    x.UseSpaStaticFiles();
    x.UseStaticFiles();

    x.UseSpa(spa =>
    {
        spa.Options.SourcePath = "ClientApp";
        if (app.Environment.IsDevelopment())
        {
            spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
        }
    });
});


app.Run();
