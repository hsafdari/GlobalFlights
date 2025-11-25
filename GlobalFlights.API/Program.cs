using FluentValidation;
using GlobalFlights.API.middleware;
using GlobalFlights.API.Models;
using GlobalFlights.API.Services;
using GlobalFlights.Application;
using GlobalFlights.ExternalServices;
using Scalar.AspNetCore;
using System.Text.Json;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
       
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddScoped<SSEService>();
        builder.Services.AddOpenApi();
        builder.Services.RegisterExternalServices();
        builder.Services.RegisterApplicationServices();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        //builder.Services.AddSwaggerGen();
        builder.Services.AddHttpClient();
        // Add CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp",
                policy => policy.WithOrigins("http://localhost:12658", "http://localhost:12659")
                                .AllowAnyHeader()
                                .AllowAnyMethod());
        });
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            //app.UseSwagger();
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        app.UseCors("AllowReactApp");
        app.UseRouting();
        //app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

