using Converter.Main;
using Converter.Main.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(
        Bootstrapper.AddAppServices()
    )
    .AddLogger()
    .Build();

host.Run();

