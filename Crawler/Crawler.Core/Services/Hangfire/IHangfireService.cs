namespace Crawler.Core.Services.Hangfire;

public interface IHangfireService
{
    DateTime CheckIfDateIsCorrect(DateTime dateTime);
}