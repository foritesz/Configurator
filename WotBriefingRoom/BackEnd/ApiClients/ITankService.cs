namespace BackEnd.ApiClients
{
    public interface ITankService
    {
        Task<List<int>> GetTankIdsAsync();
    }
}
