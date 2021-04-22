using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Configurations
{
    public class AuthOptions
    {
        public const string Issuer = "Nano35.Identity.Api";
        const string Key = "mysupersecret_secretkey!123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
    
    public class AuthenticationConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidIssuer = AuthOptions.Issuer,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = false
                    };
                });
        }
    }
}