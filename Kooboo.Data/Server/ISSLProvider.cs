using System.Security.Cryptography.X509Certificates;

namespace Kooboo.Data.Server
{
    public interface ISSLProvider
    {
        X509Certificate SelectCertificate(string hostName);
    }
}
