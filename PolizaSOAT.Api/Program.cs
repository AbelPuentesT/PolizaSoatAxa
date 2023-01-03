using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Core.Services;
using PolizaSOAT.Infrastructure.Data;
using PolizaSOAT.Infrastructure.Interfaces;
using PolizaSOAT.Infrastructure.Options;
using PolizaSOAT.Infrastructure.Repositories;
using PolizaSOAT.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IPolizaService, PolizaService>();
builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<ICiudadService, CiudadService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ISecurityService, SecurityService>();
builder.Services.AddSingleton<IPasswordService, PasswordService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(PolizaSOAT.Core.Interfaces.IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.Configure<PasswordsOptions>(builder.Configuration.GetSection("PasswordOptions"));


builder.Services.AddDbContext<PolizaSoatContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PolizaSoat")));

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






// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddControllers().AddNewtonsoftJson(options =>
//{
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
//    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
//}).ConfigureApiBehaviorOptions(options => { });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
