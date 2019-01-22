#if NETSTANDARD2_0
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Kooboo.Data.Server
{
    public class KestrelWebServer:IWebServer
    {
        private List<IKoobooMiddleWare> MiddleWares = new List<IKoobooMiddleWare>();
        private IKoobooMiddleWare StartWare = null;


        private Dictionary<int, WebHostBuilder> servers = new Dictionary<int, WebHostBuilder>();
        private IWebHost host;


        #region kestrel iStartup
        public KestrelWebServer()
        {

        }

        public void Configure(IApplicationBuilder app)
        {
            var serverHandle = new ServerHandler(r => this.StartWare.Invoke(r));
            app.Run((httpContext) =>
            {
                return serverHandle.Handle(httpContext);
            });
        }
        #endregion

        public KestrelWebServer(int port, ISSLProvider sslProvider)
        {

            host = CreateHost(port, sslProvider);
            

        }
        
        private IWebHost CreateHost(int port, ISSLProvider sslProvider)
        {
            var host = new WebHostBuilder()
                .UseKestrel(options =>
                {
                    if (port == 443)
                    {
                        options.Listen(IPAddress.Loopback, 443, listenOptions =>
                        {
                            listenOptions.UseHttps(httpsOptions =>
                            {
                                httpsOptions.ServerCertificateSelector = (connectionContext, name) =>
                                {
                                    if (sslProvider != null)
                                    {
                                        var cer = sslProvider.SelectCertificate(name);
                                        if (cer != null)
                                        {
                                            return new X509Certificate2(cer);
                                        }
                                    }
                                    return null;

                                };
                            });
                        });
                    }
                    else
                    {
                        options.Listen(IPAddress.Loopback, port);
                    }
                })

                .UseStartup<KestrelWebServer>()
                .Build();
            return host;
        }
        public void Start()
        {
            host.Start();
        }

        public void Stop()
        {
            host.StopAsync();
        }

        public void SetMiddleWares(List<IKoobooMiddleWare> middlewares)
        {
            this.MiddleWares = middlewares;
            OrganizeChain();
        }

        private void OrganizeChain()
        {
            bool HasEnd = false;
            foreach (var item in this.MiddleWares)
            {
                if (item.GetType() == typeof(EndMiddleWare))
                {
                    HasEnd = true;
                }
            }
            if (!HasEnd)
            {
                this.MiddleWares.Add(new EndMiddleWare());
            }

            int count = this.MiddleWares.Count;
            for (int i = 0; i < count; i++)
            {
                if (i < count - 1)
                {
                    this.MiddleWares[i].Next = this.MiddleWares[i + 1];
                }
            }
            this.StartWare = this.MiddleWares[0];
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}
#endif