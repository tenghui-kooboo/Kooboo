using Kooboo.Lib.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kooboo.Meta
{
    public class MetaProvider
    {
        readonly Type[] _types = new Type[0];
        readonly object[] _objects = new object[0];
        readonly Type _metaConfigurationType = typeof(IMetaConfiguration);
        readonly Lazy<IEnumerable<Views.KbMeta>> _metas;

        public static MetaProvider Instance { get; private set; }

        static MetaProvider()
        {
            Instance = new MetaProvider(AssemblyLoader.AllAssemblies);
        }

        public MetaProvider(IEnumerable<Assembly> assemblies)
        {
            _metas = new Lazy<IEnumerable<Views.KbMeta>>(() =>
            {
                var types = assemblies.SelectMany(s => s.GetTypes().Where(w => w.GetInterfaces().Any(a => a == _metaConfigurationType))).Distinct();
                return types.Select(s =>
                {
                    var meta = new Views.KbMeta();
                    meta.SetRoute(DefaultRouteName(s.Name));
                    var configuration = s.GetConstructor(_types).Invoke(_objects);
                    s.GetMethod("Configure").Invoke(configuration, new[] { meta });
                    return meta;
                });
            }, true);
        }

        public Views.KbMeta GetMeta(string metaName)
        {
            return _metas.Value.FirstOrDefault(f => f.Name == metaName);
        }

        string DefaultRouteName(string name)
        {
            name = name.Trim().ToLower();

            if (name.EndsWith("metaconfiguration"))
            {
                return name.Substring(0, name.Length - 17);
            }

            return name;
        }
    }

}
