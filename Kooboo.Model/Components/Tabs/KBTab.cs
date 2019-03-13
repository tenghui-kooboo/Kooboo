using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Context;
using Kooboo.Model.Attributes;

namespace Kooboo.Model.Components.Tabs
{
    public class KBTab:IComponent
    {
        public ComponentType Type => ComponentType.KTab;

        public List<KTabItem> Tabs { get; set; } = new List<KTabItem>();

        public RenderContext Context { get; set; }

        public VueField GetField()
        {
            var field = new VueField();
            field.Name = "tabs";

            var list = new List<Dictionary<string, object>>();
            foreach(var item in Tabs)
            {
                list.Add(item.GetData(Context));
            }
            field.Value = list;

            return field;
        }

        public bool IsMatch(Attribute attribute)
        {
            return attribute is TabAttribute;
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public void SetData(List<Dictionary<string, List<Attribute>>> attributes)
        {
            foreach (var attr in attributes)
            {
                foreach (var keyPair in attr)
                {
                    var list = keyPair.Value;
                    foreach (var item in list)
                    {

                        var tabAttr = item as TabAttribute;
                        KTabItem tabItem = new KTabItem()
                        {
                            Name = tabAttr.Name,
                            DisplayName = tabAttr.DisplayName,
                        };

                        var component = ComponentManager.Instance.Get(tabAttr.ComponentType);

                        //todo tabs can only put ktable or kform 
                        if (component != null && (
                            tabAttr.ComponentType == ComponentType.KTable|| 
                            tabAttr.ComponentType == ComponentType.KForm
                            ))
                        {
                            tabItem.Component = component;

                            var modelName = tabAttr.ModelName;
                            //need to avoid dead cycle
                            var type = ModelCache.Instance.Get(modelName);

                            var componentModels = ComponentManager.Instance.GetComponentModels(type);

                            if (componentModels.Count > 0)
                            {
                                //todo confirm
                                var componentModel = componentModels[0];
                                component.SetData(componentModel.Attributes);
                            }
                        }
                        Tabs.Add(tabItem);
                    }

                }
            }
        }
    }

    public class KTabItem
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public IComponent Component { get; set; }

        public Dictionary<string,object> GetData(RenderContext context)
        {
            var dic = new Dictionary<string, object>();

            dic.Add("name",Name);
            dic.Add("displayName", ModelHelper.GetMultiLang(DisplayName,context));
            dic.Add("type", Component.Type);

            var field = Component.GetField();
            dic.Add("item", field);

            return dic;
        }
    }

}
