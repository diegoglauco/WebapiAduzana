using VagasEmpregosAdzuna.Models;

public interface IJobProvider
{
    Task<IEnumerable<Job>> Search(string keyword, int page = 1);
}