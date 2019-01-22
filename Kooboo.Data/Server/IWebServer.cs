using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Data.Server
{
    public interface IWebServer
    {
        void Start();

        void Stop();

        void SetMiddleWares(List<IKoobooMiddleWare> middlewares);

    }
}
