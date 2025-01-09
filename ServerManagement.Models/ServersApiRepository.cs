using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManagement.Models
{
    public class ServersApiRepository : IServersApiRepository
    {
        private readonly IHttpClientFactory httpClientFactory;
        private const string apiName = "ServerApi";

        public ServersApiRepository(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<List<Server>> GetServersAsync()
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.GetAsync("api/servers");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Server>>(content) ?? new List<Server>();

        }

        public async Task<Server> GetServersAsyncById(int serverId)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);

            var response = await httpClient.GetAsync($"api/servers/{serverId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Server>(content) ?? new Server();
        }

        public async Task AddServersAsync(Server server)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var content = new StringContent(JsonConvert.SerializeObject(server), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/servers", content);

            response.EnsureSuccessStatusCode();
        }


        public async Task UpdateServersAsync(int serverId, Server server)
        {
            if (serverId != server.ServerId) return;
            var httpClient = httpClientFactory.CreateClient(apiName);
            var content = new StringContent(JsonConvert.SerializeObject(server), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"api/servers/{server.ServerId}", content);

            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteServersAsync(int serverId)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.DeleteAsync($"api/servers/{serverId}");
            response.EnsureSuccessStatusCode();
        }

    }
}
