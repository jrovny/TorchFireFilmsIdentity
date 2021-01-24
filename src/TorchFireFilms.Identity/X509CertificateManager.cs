using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging;
using Serilog;

namespace TorchFireFilms.Identity
{
    public class X509CertificateManager
    {
        private readonly ILogger<X509CertificateManager> _logger;

        public X509CertificateManager(ILogger<X509CertificateManager> logger)
        {
            _logger = logger;
        }
        public X509Certificate2 GetX509Certificate2(string x509CertificatePath)
        {
            if (string.IsNullOrWhiteSpace(x509CertificatePath))
                throw new ArgumentNullException(nameof(x509CertificatePath));
            _logger.LogInformation($"Looking for cert in '{x509CertificatePath}'");

            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser, OpenFlags.ReadWrite))
            {
                store.Add(new X509Certificate2(x509CertificatePath, string.Empty, X509KeyStorageFlags.PersistKeySet));
                store.Open(OpenFlags.ReadOnly);
                var certs = store.Certificates.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                if (certs.Count == 0)
                    return null;

                _logger.LogInformation($"Found and returning cert with thumbprint {certs[0].Thumbprint}");

                return certs[0];
            }
        }
    }
}
