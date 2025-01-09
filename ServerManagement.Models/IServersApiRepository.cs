
namespace ServerManagement.Models
{
    public interface IServersApiRepository
    {
        Task AddServersAsync(Server server);
        Task DeleteServersAsync(int serverId);
        Task<List<Server>> GetServersAsync();
        Task<Server> GetServersAsyncById(int serverId);
        Task UpdateServersAsync(int serverId, Server server);
    }
}