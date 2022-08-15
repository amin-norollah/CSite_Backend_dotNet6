using CSite.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string ploicy = " ";
ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();


builder.Services
    .AddDbContext<CompanyContext>(opt =>
        opt.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), sqlServerOptions =>
        {
            var assembly = typeof(CompanyContext).Assembly;
            var assemblyName = assembly.GetName();

            sqlServerOptions.MigrationsAssembly(assemblyName.Name);
        }));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_sercret_key_123456"))
    };
});

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
