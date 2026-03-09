using System.Text.Json.Serialization;

namespace VagasEmpregosAdzuna.Models;

public class AdzunaResponse
{
    [JsonPropertyName("results")]
    public List<AdzunaJob> Results { get; set; }
}

public class AdzunaJob
{
    public string title { get; set; }
    public Company company { get; set; }
    public Location location { get; set; }
    public decimal? salary_min { get; set; }
    public decimal? salary_max { get; set; }
    public string redirect_url { get; set; }
     public string description { get; set; }    
}

public class Company
{
    public string display_name { get; set; }
}

public class Location
{
    public string display_name { get; set; }
}