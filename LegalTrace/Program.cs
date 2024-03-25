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
using Microsoft.Extensions.Configuration;

namespace LegalTrace
{
    class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(
                    o => o.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"))
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
