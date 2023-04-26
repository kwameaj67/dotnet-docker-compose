using Innoloft_Test.Interfaces;
using Innoloft_Test.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Net;
using System.Text;

namespace Innoloft_Test.Repositories
{
    public class UserRepository : IUser
    {
        private HttpClient _client;

        public UserRepository(HttpClient client)
        {
            _client = client;
        }
        public async Task<User?> GetUser(int id)
        {
            try
            {
                string resource = $"users/{id}";
                var response = await _client.GetAsync(resource).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                
                return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
