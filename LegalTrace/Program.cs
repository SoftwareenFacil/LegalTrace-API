using Microsoft.EntityFrameworkCore;
using LegalTrace.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using LegalTrace.BLL.Controllers.JwtControllers;
using DinkToPdf.Contracts;
using DinkToPdf;
using LegalTrace.GoogleDrive.Models;

namespace LegalTrace
{
    class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"))
                       .EnableDetailedErrors() // Optional: enables detailed errors for debugging
                       .EnableSensitiveDataLogging() // Optional: enables logging of sensitive data for debugging
                       .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Warning))) // Set log level to Warning or higher
            );
            builder.Services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["ConfiguracionJwt:Llave"] ?? string.Empty)
                        )
                    };
                });

            builder.Services.AddSingleton<GoogleServiceAccountJson>(provider =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    // Load JSON from file in development environment
                    string jsonFilePath = "path/to/your/credentials.json"; // Replace with the actual path
                    if (!File.Exists(jsonFilePath))
                    {
                        throw new InvalidOperationException("Service account JSON file not found.");
                    }

                    string jsonContent = File.ReadAllText(jsonFilePath);
                    return new GoogleServiceAccountJson(jsonContent);
                }
                else
                {
                    // Load JSON from environment variable in production
                    string jsonContent = Environment.GetEnvironmentVariable("GOOGLE_SERVICE_ACCOUNT_JSON");

                    if (string.IsNullOrEmpty(jsonContent))
                    {
                        throw new InvalidOperationException("Google service account JSON is not configured in the environment variables.");
                    }

                    return new GoogleServiceAccountJson(jsonContent);
                }
            });

            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddScoped<IManejoJwt, ManejoJwt>();
            builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            var app = builder.Build();


            app.UseExceptionHandler("/Error");
            app.UseHttpsRedirection();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseCors("MyAllowSpecificOrigins");
            app.MapControllers();

            app.Run();
        }
    }
}
