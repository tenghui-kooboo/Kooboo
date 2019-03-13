using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components
{
    ////todo rewrite
    public class ComponentManager
    {
        private static ComponentManager _instance;
        private static object _lockObj=new object();
        public static ComponentManager Instance
        {
            get
            {
                
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if(_instance==null)
                            _instance = new ComponentManager();
                    }
                    
                }
                return _instance;
            }
        }
        public List<IComponent> Components { get; set; } = new List<IComponent>();
        //todo to be optimized
        //can use IL emit,it will not create instance.
        public ComponentManager()
        {
            foreach (var type in Lib.Reflection.AssemblyLoader.LoadTypeByInterface(typeof(IComponent)))
            {
                var instance = Activator.CreateInstance(type) as IComponent;
                Components.Add(instance);
            }
        }

        public IComponent Get(ComponentType type)
        {
            var component = Components.Find(c => c.Type == type);

            if (component != null)
            {
                var instance = Activator.CreateInstance(component.GetType()) as IComponent;

                return instance;
            }

            return null;
        }

        public List<ComponentModel> GetComponentModels(Type type)
        {
            var props = type.GetProperties();

            List<ComponentModel> componentModels = new List<ComponentModel>();

            foreach (var prop in props)
            {
                var attributes = prop.GetCustomAttributes().ToList();

                ComponentModel model = null;
                var dic = new Dictionary<string, List<Attribute>>();
                foreach (var attr in attributes)
                {
                    try
                    {
                        var matchComponents = Instance.Components.Find(c => c.IsMatch(attr));
                        if (matchComponents != null)
                        {
                            model = componentModels.Find(c => c.Type == matchComponents.GetType());
                            if (model == null)
                            {
                                model = new ComponentModel()
                                {
                                    Type = matchComponents.GetType(),
                                    Attributes = new List<Dictionary<string, List<Attribute>>>()
                                };
                                componentModels.Add(model);
                            }
                            break;
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                    

                }

                if (model != null)
                {
                    dic[prop.Name] = attributes;
                    model.Attributes.Add(dic);
                }

            }
            return componentModels;
        }

        public List<IComponent> GetComponents(Type type,RenderContext context)
        {
            var props = type.GetProperties();

            List<ComponentModel> componentModels = GetComponentModels(type);

            var components = new List<IComponent>();
            foreach (var model in componentModels)
            {
                try
                {
                    var instance = Activator.CreateInstance(model.Type) as IComponent;
                    instance.Context = context;
                    instance.SetData(model.Attributes);
                    components.Add(instance);
                }
                catch(Exception ex)
                {

                }

                
            }
            return components;
        }

    }
    public class ComponentModel
    {
        public Type Type { get; set; }

        public List<Dictionary<string, List<Attribute>>> Attributes { get; set; }
    }
}
