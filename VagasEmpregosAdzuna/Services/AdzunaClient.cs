namespace VagasEmpregosAdzuna.Services;
using System.Text.Json;
using VagasEmpregosAdzuna.Models;
using Microsoft.Extensions.Options;
using System.Text;

public class AdzunaSettings
{
    public string BaseUrl { get; set; }
    public string AppId { get; set; }
    public string AppKey { get; set; }
}

public class AdzunaClient : IJobProvider
{
    private readonly HttpClient _httpClient;
    private readonly AdzunaSettings _settings;

    public AdzunaClient(HttpClient httpClient, IOptions<AdzunaSettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }

    public async Task<IEnumerable<Job>> Search(string keyword, int page = 1)
    {
        var url = $"https://api.adzuna.com/v1/api/jobs/br/search/{page}?app_id={_settings.AppId}&app_key={_settings.AppKey}" +
                  $"&what={Uri.EscapeDataString(keyword)}";

        Console.WriteLine("Calling URL: " + url);
        var response = await _httpClient.GetAsync(url);
        Console.WriteLine("Response Status: " + response.RequestMessage);
        response.EnsureSuccessStatusCode();

        var json = Encoding.UTF8.GetString(await response.Content.ReadAsByteArrayAsync());

        var adzunaResponse = JsonSerializer.Deserialize<AdzunaResponse>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        return adzunaResponse?.Results.Select(j => new Job
        {
            Title = j.title,
            Company = j.company?.display_name,
            Location = j.location?.display_name,
            SalaryMin = j.salary_min,
            SalaryMax = j.salary_max,
            Url = j.redirect_url,
            description = j.description 
        }) ?? Enumerable.Empty<Job>();
    }
}