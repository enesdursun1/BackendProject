using DataAccess;
using Business;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.DependencyResolvers;
using Core.Security;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Core.WebAPI.Extensions.Swagger;
using Core.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Security.Encryption;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Business.Concrete;
using Core.CrossCuttingConcerns.Authorization;
using WebAPI;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessServices();
builder.Services.AddDependencyResolvers();
builder.Services.AddSecurityServices();
builder.Services.AddHttpContextAccessor();


const string tokenOptionsConfigurationSection = "TokenOptions";
TokenOptions tokenOptions =
    builder.Configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>()
    ?? throw new InvalidOperationException($"\"{tokenOptionsConfigurationSection}\" section cannot found in configuration.");
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(p =>
    {
        _ = p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    })
);
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition(
        name: "Bearer",
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer YOUR_TOKEN\". \r\n\r\n"
                + "`Enter your token in the text input below.`"
        }
    );
    opt.OperationFilter<BearerSecurityRequirementOperationFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LogMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();
const string webApiConfigurationSection = "WebAPIConfiguration";
WebApiConfiguration webApiConfiguration =
    app.Configuration.GetSection(webApiConfigurationSection).Get<WebApiConfiguration>()
    ?? throw new InvalidOperationException($"\"{webApiConfigurationSection}\" section cannot found in configuration.");
app.UseCors(opt => opt.WithOrigins(webApiConfiguration.AllowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
app.Run();
