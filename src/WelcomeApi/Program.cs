using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WelcomeApi.Options;
using WelcomeApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<WelcomeOptions>(builder.Configuration.GetSection(WelcomeOptions.SectionName));
builder.Services.AddSingleton<WelcomeService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

public partial class Program { }
