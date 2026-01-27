using AddressBook.Application.Interfaces.Services;
using AddressBook.Application.Services;
using AddressBook.Application.Interfaces.Repositories;
using AddressBook.Infrastructure.Repositories;
using AddressBook.Infrastructure.Configurations;
using PetaPoco;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(AddressBook.Application.Mapping.ContactProfile));

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
app.MapControllers();

app.Run();