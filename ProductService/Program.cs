using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductService.Application;
using ProductService.Application.Dtos.Request;
using ProductService.Application.Services;
using ProductService.Infrastructure;
using ProductService.Infrastructure.Entity;
using ProductService.Presentation;
using ProductService.Presentation.Middlewares;
using ProductService.Presentation.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddPresentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();