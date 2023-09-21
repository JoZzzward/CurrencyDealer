using Crawler.Core.Services.AppHttpClient;
using Hangfire;
using Hangfire.Storage.Monitoring;

namespace Crawler.Core.Services.Hangfire;

public class HangfireService : IHangfireService
{
    public DateTime CheckIfDateIsCorrect(DateTime dateTime)
    {
        var lastDate = GetLastDate();
        var minimalDate = new DateTime(2015, 1, 1);

        if (lastDate == DateTime.MinValue)
            dateTime = minimalDate;

        else if (dateTime == DateTime.MinValue)
            dateTime = DateTime.UtcNow.AddYears(-2);

        else if (dateTime < minimalDate)
            dateTime = minimalDate;

        else if (dateTime < lastDate)
            dateTime = lastDate;
        
        return dateTime.Date;
    }

    private static DateTime GetLastDate()
    {
        var jobs = GetJobsFromStorage();

        var date = jobs.FirstOrDefault(x => x.Value.InSucceededState &&
                        x.Value.Job.Method.Name == nameof(HttpClientService.GetExchangeRatesAsync)).Value;

        if (date is null || date.SucceededAt is null)
            return new DateTime();

        return date.SucceededAt!.Value.Date;
    }

    private static JobList<SucceededJobDto> GetJobsFromStorage()
    {
        var monitoringApi = JobStorage.Current.GetMonitoringApi();

        var datesCount = (int)monitoringApi.SucceededListCount();

        var jobs = monitoringApi.SucceededJobs(0, datesCount);

        return jobs;
    }
}