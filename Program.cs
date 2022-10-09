using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation;
using minimalAPI.Validation;
using minimalAPI.Contracts.Requests;
using minimalAPI.Webservices.Interfaces;
using minimalAPI.Webservices;
using System.Text.Json;
using System.Text.Json.Serialization;
using minimalAPI.Dtos;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

IEnumerable<Server> servers = builder.Configuration.GetSection("servers").Get<IEnumerable<Server>>();

foreach (var server in servers)
{
    builder.Services.AddHttpClient(server.name, client =>
    {
        client.BaseAddress = new Uri(server.url);
        client.Timeout = new TimeSpan(0, 0, 30);
    });
}

builder.Services.AddScoped<ICommonWebservice, CommonWebservice>();
//builder.Services.AddScoped<IValidator<ID>, IDValidator>();

var app = builder.Build();

app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());
app.Run();
