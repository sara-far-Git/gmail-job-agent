using Google.Apis.Gmail.v1;
using Microsoft.Extensions.Hosting;

public class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var helper = new GmailServiceHelper();
        var service = await helper.CreateServiceAsync();

        while (!stoppingToken.IsCancellationRequested)
        {
            var request = service.Users.Messages.List("me");
            request.MaxResults = 5;

            var response = await request.ExecuteAsync();

            Console.WriteLine($"Found {response.Messages?.Count ?? 0} emails");

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
