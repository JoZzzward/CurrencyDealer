using Crawler.Core;
using Crawler.Database;
using Crawler.Main.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var configuration = builder.Configuration;

var services = builder.Services;

services.AddHangFireDatabase(configuration);

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddHttpClient();

services.AddAppServices();

services.AddAppMassTransit(configuration);

services.AddAppHangfire(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAppHangfire();

app.UseAuthorization();

app.MapControllers();

app.Run();
