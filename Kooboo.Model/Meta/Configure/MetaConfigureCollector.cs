using System;
using System.Collections.Generic;
using System.Linq;

using Kooboo.Data.Interface;

namespace Kooboo.Model.Meta.Configure
{
    public class MetaConfigureCollector : IMetaConfigureCollector
    {
        private static Type ConfigureGeneric = typeof(IMetaConfigure<,>);
        private static Type InterfaceEntryGeneric = typeof(InterfaceEntry<,>);

        private delegate void ConfigureDeletage<T>(T meta) where T : IViewMeta;

        private Dictionary<Type, InterfaceEntry> _configures;
        private IAssemblyProvider _assemblyProvider;

        public MetaConfigureCollector(IAssemblyProvider assemblyProvider)
        {
            _assemblyProvider = assemblyProvider;
        }

        public IViewMeta CreateMeta(Type modelType, Type metaType)
        {
            var configureInterface = ConfigureGeneric.MakeGenericType(modelType, metaType);
            if (!_configures.TryGetValue(configureInterface, out InterfaceEntry entry))
                throw new KeyNotFoundException($"No configure found for model {modelType.FullName} and meta {metaType.FullName}");

            var meta = Activator.CreateInstance(metaType) as IViewMeta;
            meta.ModelType = modelType.Name;
            entry.Configure(meta);

            return meta as IViewMeta;
        }

        public bool Initialized => _configures != null;

        public void Initialize(Action<ConfiugreFoundCallback> onConfigureFound)
        {
            _configures = new Dictionary<Type, InterfaceEntry>();

            // Traverse assemblies and types
            var assemblies = _assemblyProvider.GetAssemblies();
            foreach (var type in assemblies.SelectMany(o => o.GetTypes()))
            {
                if (!type.IsClass || type.IsAbstract)
                    continue;

                TryExtractType(type, onConfigureFound);
            }
        }

        private void TryExtractType(Type configureClass, Action<ConfiugreFoundCallback> onConfigureFound)
        {
            var configureInterfaces = configureClass.GetInterfaces().Where(o => o.IsGenericType && o.GetGenericTypeDefinition() == ConfigureGeneric).ToArray();
            if (configureInterfaces.Length == 0)
                return;

            var entry = new ConfigureEntry(configureClass);
            foreach (var interfaceType in configureInterfaces)
            {
                AddConfigure(entry, interfaceType, onConfigureFound);
            }
        }

        private void AddConfigure(ConfigureEntry configureEntry, Type interfaceType, Action<ConfiugreFoundCallback> onConfigureFound)
        {
            var argTypes = interfaceType.GetGenericArguments();
            var modelType = argTypes[0];
            var metaType = argTypes[1];

            onConfigureFound?.Invoke(new ConfiugreFoundCallback
            {
                ModelType = modelType,
                MetaType = metaType
            });

            // Add configure type
            if (!_configures.TryGetValue(interfaceType, out InterfaceEntry interfaceEntry))
            {
                interfaceEntry = Activator.CreateInstance(InterfaceEntryGeneric.MakeGenericType(modelType, metaType)) as InterfaceEntry;
                _configures.Add(interfaceType, interfaceEntry);
            }

            interfaceEntry.Configures.Add(configureEntry);
        }

        class InterfaceEntry<TModel, TMeta> : InterfaceEntry
            where TModel : ISiteObject
            where TMeta : IViewMeta
        {
            public override  void Configure(object meta)
            {
                var typedMeta = (TMeta)meta;
                var configures = Configures.Select(o => o.Object as IMetaConfigure<TModel, TMeta>);
                var creators = configures.Where(o => o.IsCreator).ToArray();
                if (creators.Length > 1)
                    throw new InvalidOperationException($"More than one creator found for model {typeof(TModel).FullName}");

                creators.FirstOrDefault()?.Configure(typedMeta);

                foreach (var each in configures.Where(o => !o.IsCreator))
                {
                    each.Configure(typedMeta);
                }
            }
        }

        abstract class InterfaceEntry
        {
            public bool CreatorMerged { get; set; }

            public List<ConfigureEntry> Configures { get; } = new List<ConfigureEntry>();

            public abstract void Configure(object meta);
        }

        class ConfigureEntry
        {
            private object _lock = new object();

            public ConfigureEntry(Type type)
            {
                Type = type;
            }

            public Type Type { get; }

            private object _object;
            public object Object
            {
                get
                {
                    if (_object == null)
                    {
                        lock (_lock)
                        {
                            if (_object == null)
                            {
                                _object = Activator.CreateInstance(Type);
                            }
                        }
                    }
                    return _object;
                }
            }
        }

    }
}
