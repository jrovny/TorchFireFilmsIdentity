using System.Collections.Generic;
using IdentityServer4.Models;

namespace TorchFireFilms.Identity.Data
{
    public class IdentityServerConfiguration
    {
        public IEnumerable<IdentityResource> IdentityResources { get; set; }
        public IEnumerable<ApiScope> ApiScopes { get; set; }
        public IEnumerable<Client> Clients { get; set; }
    }
}
