using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.HttpServer.Http;

namespace Kooboo.HttpServer
{
    public class HttpApplication : IHttpApplication<HttpContext>
    {
        private Func<HttpContext, Task> _handler;

        public HttpApplication(Func<HttpContext,Task> handler)
        {
            _handler = handler;
        }

        public HttpContext CreateContext(HttpProtocol protocal)
        {
            return new HttpContext
            {
                Features = protocal.Features
            };
        }

        public void DisposeContext(HttpContext context, Exception exception)
        {
        }

        public async Task ProcessRequestAsync(HttpContext context)
        {
            await _handler(context);
        }
    }
}
