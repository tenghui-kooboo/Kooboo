using System;
using Kooboo.Data.Context;
using System.Threading.Tasks;

namespace Kooboo.Data.Server
{
   
    public class ServerHandler : IServerHttpHander
    {
        public Func<RenderContext, Task> _handle;

        public ServerHandler(Func<RenderContext, Task> handle)
        {
            _handle = handle;
        }
#if NETSTANDARD2_0
        public async Task Handle(Microsoft.AspNetCore.Http.HttpContext context)
#else
        public async Task Handle(Kooboo.HttpServer.HttpContext context)
#endif
        {
            RenderContext renderContext = await WebServerContext.GetRenderContext(context);
            try
            {
                await _handle(renderContext);
                await WebServerContext.SetResponse(context, renderContext);
            }
            catch (Exception ex)
            {
                renderContext.Response.StatusCode = 500;
                renderContext.Response.Body = System.Text.Encoding.UTF8.GetBytes(ex.Message);
                await WebServerContext.SetResponse(context, renderContext);
            }
         
        }
    } 
}
