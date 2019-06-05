using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Kooboo.Model.Meta.Configure;
using Kooboo.Model.Meta.Serialization;

namespace Kooboo.Model.Meta
{
    public partial class MetaProvider
    {
        private Dictionary<string, Type> _typeNameMap = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<Type, HashSet<Type>> _modelMetaMap = new Dictionary<Type, HashSet<Type>>();
        private Dictionary<string, MetaCacheEntry> _metaCache = new Dictionary<string, MetaCacheEntry>();
        private Dictionary<string, JsonCacheEntry> _jsonCache = new Dictionary<string, JsonCacheEntry>();
        private object _metaCacheLock = new object();
        private object _jsonCacheLock = new object();
        private object _initConfiguresLock = new object();

        private IMetaConfigureCollector _configures;
        private IMetaSerializer _serializer;

        public MetaProvider(IMetaConfigureCollector configures, IMetaSerializer serializer)
        {
            _configures = configures;
            _serializer = serializer;
        }

        public bool IsValidModelName(string name)
        {
            return _typeNameMap.ContainsKey(name);
        }

        public bool IsValidPair(Type modelType, Type metaType)
        {
            if (!_modelMetaMap.TryGetValue(modelType, out HashSet<Type> metaTypes))
                return false;

            return metaTypes.Contains(metaType);
        }

        public string GetMeta(string modelName, string metaName, SerializationContext context)
        {
            var meta = GetMeta(modelName, metaName);
            return _serializer.Serialize(meta, context);
        }

        //public string GetMeta(string modelName, string metaName, SerializationContext context)
        //{
        //    var key = JsonCacheKey(context.RenderContext.Culture, modelName, metaName);
        //    if (_jsonCache.TryGetValue(key, out JsonCacheEntry entry))
        //        return entry.Json;

        //    lock (_jsonCacheLock)
        //    {
        //        if (_jsonCache.TryGetValue(key, out entry))
        //            return entry.Json;

        //        entry = new JsonCacheEntry(() =>
        //        {
        //            var meta = GetMeta(modelName, metaName);
        //            return _serializer.Serialize(meta, context);
        //        });
        //        _jsonCache[key] = entry;
        //        return entry.Json;
        //    }
        //}

        public IViewMeta GetMeta(string modelName, string metaName = null)
        {
            EnsureConfigures();

            if (!_typeNameMap.TryGetValue(modelName, out Type modelType))
                throw new KeyNotFoundException($"No type found for model name {modelName}");

            if (metaName == null)
            {
                return GetMeta(modelType);
            }
            else
            {
                if (!_typeNameMap.TryGetValue(MetaTypeName(metaName), out Type metaType))
                    throw new KeyNotFoundException($"No type found for meta name {modelName}");

                return GetMeta(modelType, metaType);
            }
        }

        public IViewMeta GetMeta(Type modelType)
        {
            EnsureConfigures();

            if (!_modelMetaMap.TryGetValue(modelType, out HashSet<Type> metaTypes))
                ThrowNoConfigureForModelType(modelType);

            if (metaTypes.Count > 1)
                ThrowTooManyConfiguresForModelType(modelType, metaTypes);

            var metaType = metaTypes.First();

            if (_metaCache.TryGetValue(MetaCacheKey(modelType, metaType), out MetaCacheEntry entry))
                return entry.Meta;

            entry = TryAddToMetaCache(modelType, metaType);
            return entry.Meta;
        }

        public IViewMeta GetMeta(Type modelType, Type metaType)
        {
            // Try cache
            if (_metaCache.TryGetValue(MetaCacheKey(modelType, metaType), out MetaCacheEntry entry))
                return entry.Meta;

            EnsureConfigures();

            if (!_modelMetaMap.TryGetValue(modelType, out HashSet<Type> metaTypes))
                ThrowNoConfigureForModelType(modelType);

            entry = TryAddToMetaCache(modelType, metaType);
            return entry.Meta;
        }

        private void EnsureConfigures()
        {
            if (!_configures.Initialized)
            {
                lock (_initConfiguresLock)
                {
                    if (!_configures.Initialized)
                    {
                        _configures.Initialize(AddConfigure);
                    }
                }
            }
        }

        private void AddConfigure(Configure.ConfiugreFoundCallback callback)
        {
            var modelType = callback.ModelType;
            var metaType = callback.MetaType;

            // Add short type name map
            if (!_typeNameMap.ContainsKey(modelType.Name))
            {
                _typeNameMap[modelType.Name] = modelType;
            }

            var metaName = MetaTypeName(metaType);
            if (!_typeNameMap.ContainsKey(metaName))
            {
                _typeNameMap[metaName] = metaType;
            }

            // Add model -> meta map
            if (!_modelMetaMap.TryGetValue(modelType, out HashSet<Type> metaTypes))
            {
                metaTypes = new HashSet<Type>();
                _modelMetaMap.Add(modelType, metaTypes);
            }
            metaTypes.Add(metaType);
        }

        private MetaCacheEntry TryAddToMetaCache(Type modelType, Type metaType)
        {
            var key = MetaCacheKey(modelType, metaType);

            lock (_metaCacheLock)
            {
                if (_metaCache.TryGetValue(key, out MetaCacheEntry entry))
                    return entry;

                entry = new MetaCacheEntry(() => _configures.CreateMeta(modelType, metaType));
                _metaCache.Add(key, entry);

                return entry;
            }
        }

        private string JsonCacheKey(string modelName, string metaName, string culture) => culture + "_" + modelName + "_" + metaName;

        private string MetaCacheKey(Type modelType, Type metaType) => modelType.Name + "_" + metaType.Name;

        private string MetaTypeName(Type type) => "__" + type.Name.Substring(0, type.Name.Length - 4);

        private string MetaTypeName(string shortName) => "__" + shortName;

        private void ThrowNoConfigureForModelType(Type modelType) 
            => throw new KeyNotFoundException($"No configure found for model type {modelType.FullName}");

        private void ThrowTooManyConfiguresForModelType(Type modelType, IEnumerable<Type> metaTypes) 
            => throw new InvalidOperationException($"More than one configure found for model type {modelType.FullName} when meta type is not provided");

        class JsonCacheEntry
        {
            private object _lock = new object();
            private Func<string> _getJson;

            public JsonCacheEntry(Func<string> getJson)
            {
                _getJson = getJson;
            }

            private string _json;
            public string Json
            {
                get
                {
                    if (_json != null)
                        return _json;

                    lock (_lock)
                    {
                        if (_json != null)
                            return _json;

                        _json = _getJson();
                    }

                    return _json;
                }
            }
        }

        class MetaCacheEntry
        {
            private object _lock = new object();
            private Func<IViewMeta> _createMeta;

            public MetaCacheEntry(Func<IViewMeta> createMeta)
            {
                _createMeta = createMeta;
            }

            private IViewMeta _meta;
            public IViewMeta Meta
            {
                get
                {
                    if (_meta != null)
                        return _meta;

                    lock (_lock)
                    {
                        if (_meta != null)
                            return _meta;

                        _meta = _createMeta();
                    }

                    return _meta;
                }
            }
        }
    }

    public static class MetaProviderExtensions
    {
        public static bool IsValidPair<TModel, TMeta>(this MetaProvider provider) where TMeta : IViewMeta
            => provider.IsValidPair(typeof(TModel), typeof(TMeta));
    }

    partial class MetaProvider
    {
        static MetaProvider()
        {
            var assemblyProvider = new DependencyAssemblyProvider(Kooboo.Lib.Reflection.AssemblyLoader.AllAssemblies);
            var colletor = new MetaConfigureCollector(assemblyProvider);
            var serializer = new JsonNetSerializer();
            Instance = new MetaProvider(colletor, serializer);
        }

        public static MetaProvider Instance { get; set; }

        public static Api.IApiMetaProvider ApiMetaProvider { get; set; }
    }
}
