using System.Security.Claims;
using System.Text.Json.Serialization;
using LearnMS.API.Common;
using LearnMS.API.Common.EmailService;
using LearnMS.API.Data;
using LearnMS.API.Features;
using LearnMS.API.Features.Administration;
using LearnMS.API.Features.Auth;
using LearnMS.API.Features.Courses;
using LearnMS.API.Features.Courses.Contracts;
using LearnMS.API.Middlewares;
using LearnMS.API.Security;
using LearnMS.API.Security.JwtBearer;
using LearnMS.API.ThirdParties;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Configuration;
using tusdotnet;
using tusdotnet.Interfaces;
using tusdotnet.Models;
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

    builder.Services.AddVdoService(cfg);

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
builder.Services.Configure<ApiBehaviorOptions>(opts =>
{
    opts.InvalidModelStateResponseFactory = ApiWrapper.Failure.GenerateErrorResponse;
});
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

app.MapTus("/api/courses/{courseId}/lectures/{lectureId}/lessons/{lessonId}/video", async context =>
{
    await Task.CompletedTask;

    string courseId = context.Request.RouteValues["courseId"]?.ToString() ?? throw new ArgumentNullException();
    string lectureId = context.Request.RouteValues["lectureId"]?.ToString() ?? throw new ArgumentNullException();
    string lessonId = context.Request.RouteValues["lessonId"]?.ToString() ?? throw new ArgumentNullException();

    var scope = context.RequestServices.CreateScope();
    var coursesService = scope.ServiceProvider.GetRequiredService<ICoursesService>();
    var store = new TusDiskStore("/tmp", deletePartialFilesOnConcat: true);

    return new DefaultTusConfiguration
    {
        Store = store,
        Events = new()
        {
            OnFileCompleteAsync = async ctx =>
            {
                ITusFile file = await ctx.GetFileAsync();
                var fs = await file.GetContentAsync(ctx.CancellationToken);
                try
                {
                    await coursesService.ExecuteAsync(new UploadLessonVideoCommand
                    {
                        CourseId = Guid.Parse(courseId),
                        LectureId = Guid.Parse(lectureId),
                        FS = fs,
                        LessonId = Guid.Parse(lessonId),
                    });
                }
                catch (Exception)
                {
                    await fs.DisposeAsync();
                    var terminationStore = (ITusTerminationStore)ctx.Store;
                    await terminationStore.DeleteFileAsync(file.Id, ctx.CancellationToken);
                    await store.RemoveExpiredFilesAsync(ctx.CancellationToken);
                    throw;
                }
                finally
                {
                    await fs.DisposeAsync();
                    var terminationStore = (ITusTerminationStore)ctx.Store;
                    await terminationStore.DeleteFileAsync(file.Id, ctx.CancellationToken);
                    await store.RemoveExpiredFilesAsync(ctx.CancellationToken);
                }
            }
        }
    };
});
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
