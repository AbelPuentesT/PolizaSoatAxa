using FluentValidation;
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
            builder.Services.AddDbContext<PolicySoatContext>(options =>
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
            builder.Services.AddTransient<IPolicyService, PolicyService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<ICityService, CityService>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ISecurityService, SecurityService>();
            builder.Services.AddSingleton<IPasswordService, PasswordService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            builder.Services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext?.Request;
                var absoluteUri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
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
        public static WebApplicationBuilder AddMVCFilters(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllers(options => options.Filters.Add<GlobalExceptionFilters>());

            builder.Services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            return builder;
        }
        public static WebApplicationBuilder AutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return builder;
        }

    }
}
