using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;
using System.Reflection;

namespace Kooboo.Model
{
    public class ModelCache
    {
        private static ModelCache _instance;
        private static object _lockObj = new object();
        public static ModelCache Instance
        {
            get
            {

                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                            _instance = new ModelCache();
                    }

                }
                return _instance;
            }
        }

        public Dictionary<string, Type> Models { get; set; } = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
        public ModelCache()
        {
            foreach (var model in Lib.Reflection.AssemblyLoader.LoadTypeByInterface(typeof(IKoobooModel)))
            {
                var modelNameAttr = model.GetCustomAttribute(typeof(ModelNameAttribute)) as ModelNameAttribute;
                if (modelNameAttr != null)
                {
                    Models[modelNameAttr.ModelName] = model;
                }
            }
        }

        public Type Get(string modelName)
        {
            if (Models.ContainsKey(modelName))
            {
                return Models[modelName];
            }
            return null;
        }

    }
}
