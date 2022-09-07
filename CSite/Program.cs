using AutoWrapper;
using CSite.Configurations;
using CSite.Data.DdContexts;
using CSite.Helpers;
using CSite.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

///////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.File(
                    path: "logs\\log-.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Warning
                ));

ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// DB context and unit of work
builder.Services
    .AddDbContext<CSiteDBContext>(opt =>
        opt.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), sqlServerOptions =>
        {
            var assembly = typeof(CSiteDBContext).Assembly;
            var assemblyName = assembly.GetName();

            sqlServerOptions.MigrationsAssembly(assemblyName.Name);
        })).AddUnitOfWork<CSiteDBContext>();

builder.Services.AddEndpointsApiExplorer();

//Adding URL based versioning 
builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
    x.ApiVersionReader = new UrlSegmentApiVersionReader();
    x.UseApiBehavior = true;
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "VV";
    setup.SubstitutionFormat = "VV";
    setup.SubstituteApiVersionInUrl = true;
});

//swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("1.0", new OpenApiInfo { Title = "CSite API", Version = "1.0" });

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:8800/connect/authorize"),
                TokenUrl = new Uri("https://localhost:8800/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"CSite_API", "CSite API scope for test."}
                }
            }
        }
    });

    options.OperationFilter<AuthorizeCheckOperationFilter>();
});

//security
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        options.Authority = "https://localhost:8800/";
//        options.RequireHttpsMetadata = false;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false
//        };
//    });

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        // required audience of access tokens
        options.ApiName = "CSite_API";

        // auth server base endpoint (this will be used to search for disco doc)
        options.Authority = "https://localhost:8800";
    });

// Adding CORS
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// Adding ControllerHelper
builder.Services.AddScoped<ControllerHelper>();

//disable the automatic 400 behavior
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Configure Auto mapper
builder.Services.AddAutoMapper(typeof(MapperInitilizer));

///////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(c =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        foreach (var description in provider.ApiVersionDescriptions)
            c.SwaggerEndpoint(
               $"{swaggerJsonBasePath}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

        c.OAuthClientId("CSite_swagger_client");
        c.OAuthAppName("Swagger UI for CSite");
        c.OAuthUsePkce();
    });
}

// Enable middleware to serve AutoWrapper
app.UseApiResponseAndExceptionWrapper(
    new AutoWrapperOptions
    {
        ShowApiVersion = true,
        ShowStatusCode = true,
        IsApiOnly = false
    }
);

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
