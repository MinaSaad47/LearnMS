using System.Security.Claims;
using LearnMS.API.Common;
using LearnMS.API.Data;
using LearnMS.API.Features;
using LearnMS.API.Features.Administration;
using LearnMS.API.Features.Auth;
using LearnMS.API.Middlewares;
using LearnMS.API.Security;
using LearnMS.API.Security.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using tusdotnet;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Stores;

var builder = WebApplication.CreateBuilder(args);
{
    var cfg = builder.Configuration;

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=LearnMS.db"));

    builder.Services.Configure<JwtBearerConfig>(cfg.GetSection(JwtBearerConfig.Section));
    builder.Services.Configure<AdministrationConfig>(cfg.GetSection(AdministrationConfig.Section));

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


var app = builder.Build();
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


app.Run();
