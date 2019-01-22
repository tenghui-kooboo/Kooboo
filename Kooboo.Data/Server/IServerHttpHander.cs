using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Data.Server
{
    public interface IServerHttpHander
    {
#if NETSTANDARD2_0
        Task Handle(Microsoft.AspNetCore.Http.HttpContext context);
#else
        Task Handle(Kooboo.HttpServer.HttpContext context);
#endif
    }
}
