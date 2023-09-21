using Storage.Core;
using Storage.Database;
using Storage.Database.Setup;
using Storage.Main.Configuration;
using Storage.Main.Configuration.MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

var configuration = builder.Configuration;

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAppServices();

services.AddAppMassTransit(configuration);

services.AddAppDbContext(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

DbInitializer.Execute(app.Services);

app.Run();
