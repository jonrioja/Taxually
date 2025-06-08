using FluentValidation;
using FluentValidation.AspNetCore;
using Taxually.TechnicalTest.Middleware;
using Taxually.TechnicalTest.Services.Abstract;
using Taxually.TechnicalTest.Services.Implementation;
using Taxually.TechnicalTest.Services.Strategies.Abstract;
using Taxually.TechnicalTest.Services.Strategies.Implementation;
using Taxually.TechnicalTest.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<VatRegistrationRequestValidator>();
builder.Services.AddScoped<IVatRegistrationService, VatRegistrationService>();
builder.Services.AddScoped<IVatRegistrationStrategy, GbVatRegistrationStrategy>();
builder.Services.AddScoped<IVatRegistrationStrategy, FrVatRegistrationStrategy>();
builder.Services.AddScoped<IVatRegistrationStrategy, DeVatRegistrationStrategy>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
