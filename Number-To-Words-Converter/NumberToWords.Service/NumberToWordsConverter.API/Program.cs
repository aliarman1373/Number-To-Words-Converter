using FluentValidation.AspNetCore;
using NumberToWordsConverter.Services;
using NumberToWordsConverter.Services.Interfaces;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(o=>o.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddScoped<IConverterService, ConverterService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
