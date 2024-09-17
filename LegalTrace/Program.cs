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
using System.Net;

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
                try
                {
                    options.AddPolicy("SpecificOrigins", policyBuilder =>
                    {
                        policyBuilder.SetIsOriginAllowed(origin =>
                        {
                            // Get the host part from the origin
                            var host = new Uri(origin).Host;

                            // Check if it's an IP address or a domain name
                            if (IPAddress.TryParse(host, out var ipAddress))
                            {
                                // List of allowed IPs
                                var allowedIPs = new List<IPAddress>
                                {
                                IPAddress.Parse("172.18.0.9"), // Replace with your allowed IPs
                                IPAddress.Parse("64.23.144.63")
                                };
                                if (builder.Environment.IsDevelopment())
                                    allowedIPs.Add(IPAddress.Parse("127.0.0.1"));
                                return allowedIPs.Contains(ipAddress);
                            }
                            else
                            {
                                // List of allowed domains
                                var allowedDomains = new List<string>
                                    {
                                    "www.legalcont.com"
                                    };
                                if (builder.Environment.IsDevelopment())
                                    allowedDomains.Add("localhost");

                                return allowedDomains.Contains(host);
                            }
                        })
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
                    string jsonFilePath = @"C:\Users\gvera\Downloads\teak-territory-418313-03c448d99067.json"; // Replace with the actual path
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

            if(!app.Environment.IsDevelopment())
                app.UseHttpsRedirection();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }
            app.UseRouting();
            app.UseCors("SpecificOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
