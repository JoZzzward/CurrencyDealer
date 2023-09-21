using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Database.Setup;

public static class DbInitializer
{
    public static void Execute(ServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.EnsureCreated();
    }
}
