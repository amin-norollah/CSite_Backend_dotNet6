using AutoWrapper;
using CSite.Configurations;
using CSite.Helpers;
using CSite.Models;
using CSite.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

///////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// DB context and unit of work
builder.Services
    .AddDbContext<CSiteDbContext>(opt =>
        opt.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), sqlServerOptions =>
        {
            var assembly = typeof(CSiteDbContext).Assembly;
            var assemblyName = assembly.GetName();

            sqlServerOptions.MigrationsAssembly(assemblyName.Name);
        })).AddUnitOfWork<CSiteDbContext>();

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
    options.SwaggerDoc("1.0", new OpenApiInfo { Title = "FaraWAMS Main API", Version = "1.0" });
});

// Adding simple and basic authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("s923r4jvJ-DSvsxoi8y-9vJDf6-832bnFV"))
    };
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
builder.Services.AddSingleton<ControllerHelper>();

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
