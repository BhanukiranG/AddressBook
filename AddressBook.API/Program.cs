using AddressBook.API.Middleware;
using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Application.Interfaces.Services;
using AddressBook.Application.Mapping;
using AddressBook.Application.Services;
using AddressBook.Infrastructure.Configurations;
using AddressBook.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using PetaPoco;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Validation config
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value is { Errors.Count: > 0 })
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors!.Select(e => e.ErrorMessage).ToArray()
            );

        return new BadRequestObjectResult(new
        {
            Message = "Validation failed",
            Successful = false,
            Errors = errors
        });
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(ContactProfile));

// DI Setup
builder.Services.AddSingleton<IDatabaseFactory, DatabaseFactory>();
builder.Services.AddScoped<IDatabase>(provider =>
{
    var factory = provider.GetRequiredService<IDatabaseFactory>();
    return factory.GetDatabase();
});

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
// Exception middleware
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();