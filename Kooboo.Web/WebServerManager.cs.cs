using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Data.Server;
using System.Security.Cryptography.X509Certificates;

namespace Kooboo.Web
{
    public class WebServerManager
    {
        public static IWebServer Create(int port, ISSLProvider sslProvider)
        {
            IWebServer server;
#if NETSTANDARD2_0
            server = new KestrelWebServer(port, sslProvider);
#else
            server=new WebServer(port,sslProvider);
#endif
            return server;
        }
    }
}
