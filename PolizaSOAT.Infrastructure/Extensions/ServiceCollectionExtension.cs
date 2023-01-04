using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Core.Services;
using PolizaSOAT.Infrastructure.Data;
using PolizaSOAT.Infrastructure.Filters;
using PolizaSOAT.Infrastructure.Interfaces;
using PolizaSOAT.Infrastructure.Options;
using PolizaSOAT.Infrastructure.Repositories;
using PolizaSOAT.Infrastructure.Services;
using System.Text;

namespace PolizaSOAT.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static WebApplicationBuilder AddDbContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<PolizaSoatContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PolizaSoat")));
            return builder;
        }
        public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<PaginationOptions>(builder.Configuration.GetSection("Pagination"));

            builder.Services.Configure<PasswordsOptions>(builder.Configuration.GetSection("PasswordOptions"));
            return builder;

        }
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IPolizaService, PolizaService>();
            builder.Services.AddTransient<IClienteService, ClienteService>();
            builder.Services.AddTransient<ICiudadService, CiudadService>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ISecurityService, SecurityService>();
            builder.Services.AddSingleton<IPasswordService, PasswordService>();
            //builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped(typeof(PolizaSOAT.Core.Interfaces.IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });
            return builder;

        }
        public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Authentication:Isser"],
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]))
                };
            });
            return builder;

        }
        public static WebApplicationBuilder AddMVCServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

            return builder;
        }
        
    }
}
