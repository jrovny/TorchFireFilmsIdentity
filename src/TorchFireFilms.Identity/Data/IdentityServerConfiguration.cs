using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace TorchFireFilms.Identity.Data
{
    public class IdentityServerConfiguration
    {
        public IEnumerable<IdentityResource> IdentityResources { get; set; }
        public IEnumerable<ApiScope> ApiScopes { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public string X509CertificatePath { get; set; }
    }
}
