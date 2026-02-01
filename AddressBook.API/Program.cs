using AddressBook.API.Extensions;
using AddressBook.API.Filters;
using AddressBook.API.Middleware;
using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Application.Interfaces.Services;
using AddressBook.Application.Mapping;
using AddressBook.Application.Services;
using AddressBook.Application.Validators.Contact;
using AddressBook.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using PetaPoco;

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
builder.Services.AddSwaggerGen();

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

// Authorization middleware
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();