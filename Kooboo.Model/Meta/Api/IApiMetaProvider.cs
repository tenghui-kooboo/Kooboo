using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Api
{
    public interface IApiMetaProvider
    {
        ApiInfo GetMeta(string url);

        string GetModelName(Type type);
    }
}
