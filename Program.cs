using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation;
using minimalAPI.Validation;
using minimalAPI.Contracts.Requests;
using minimalAPI.Webservices.Interfaces;
using minimalAPI.Webservices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

builder.Services.AddHttpClient("AntaresWebservice", client =>
{
    client.BaseAddress = new Uri("https://s117-pl.ogame.gameforge.com/api/");
    client.Timeout = new TimeSpan(0, 0, 30);
});
builder.Services.AddScoped<IAntaresWebservice, AntaresWebservice>();
//builder.Services.AddScoped<IValidator<ID>, IDValidator>();

var app = builder.Build();

app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());
app.Run();
