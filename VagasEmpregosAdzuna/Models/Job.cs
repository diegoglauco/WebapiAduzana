namespace VagasEmpregosAdzuna.Models;

public class Job
{
    public string Title { get; set; }
    public string Company { get; set; }
    public string Location { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public string Url { get; set; }
    public string description { get; set; } 
}