using Microsoft.Extensions.Configuration;

namespace TorchFireFilms.Identity
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConfiguration _configuration;

        public ConnectionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetDefaultConnectionString()
        {
            return _configuration.GetConnectionString("Default");
        }
    }
}
