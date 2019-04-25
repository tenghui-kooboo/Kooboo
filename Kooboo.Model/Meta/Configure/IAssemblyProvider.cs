using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kooboo.Model.Meta.Configure
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}
