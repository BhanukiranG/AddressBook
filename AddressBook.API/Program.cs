using AddressBook.API.Extensions;
using AddressBook.API.Filters;
using AddressBook.API.Middleware;
using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Application.Interfaces.Security;
using AddressBook.Application.Interfaces.Services;
using AddressBook.Application.Mapping;
using AddressBook.Application.Services;
using AddressBook.Application.Validators.Contact;
using AddressBook.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using AddressBook.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetaPoco;
using System.Text;

// Create the WebApplicationBuilder
// This initializes configuration, logging, and dependency injection
var builder = WebApplication.CreateBuilder(args);

// Add controllers and global API response wrapper filter
builder.Services.AddControllers(options =>
{
    // Wrap all responses in ApiResponse format
    options.Filters.Add<ApiResponseWrapperFilter>();
});

// Configure custom API behavior for model validation errors
// Returns a consistent ApiResponse object instead of default errors
builder.Services.AddCustomApiBehavior();

// Enables automatic validation of incoming DTOs
builder.Services.AddFluentValidationAutoValidation();

// Enables client-side adapters (optional for frontend JS validation)
builder.Services.AddFluentValidationClientsideAdapters();

// Scan the assembly containing the validator and register them
builder.Services.AddValidatorsFromAssemblyContaining<CreateContactDtoValidator>();

// Swagger/OpenAPI configuration
// Generates API documentation automatically
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AddressBook API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// AutoMapper configuration
// Scans the mapping profile in the Application layer
builder.Services.AddAutoMapper(typeof(ContactProfile));

// PetaPoco Database registration
builder.Services.AddScoped<IDatabase>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new Database(connectionString, "Microsoft.Data.SqlClient");
});

// Repository and Service registration
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

// Build the WebApplication
// Auth / Security
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// User repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

// Build the WebApplication
var app = builder.Build();

// Enable Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

app.UseAuthentication();

// Authorization middleware
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();