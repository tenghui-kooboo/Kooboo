using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Dom;
using Kooboo.Model.Helper;

namespace Kooboo.Model
{
    public class ModelFromHtml
    {
        public string El { get; set; }

        public string DataFrom { get; set; }

        public List<VueMethod> Methods { get; set; } = new List<VueMethod>(); 

        private Element VueEl { get; set; }

        public ModelFromHtml(Document document)
        {
            Init(document.childNodes.item);

            InitMethods(VueEl.childNodes.item);
        }

        private void Init(List<Node> items)
        {
            foreach (var item in items)
            {
                if (item is Element)
                {
                    var element = item as Element;
                    var vueContainer = element.attributes.Find(a => a.name.Equals("vue-container", StringComparison.OrdinalIgnoreCase));
                    if (vueContainer != null)
                    {
                        this.El = vueContainer.value;
                        var dataFromAttr = element.attributes.Find(a => a.name.Equals("vue-datafrom", StringComparison.OrdinalIgnoreCase));
                        if (dataFromAttr != null)
                        {
                            this.DataFrom = dataFromAttr.value;
                        }
                        this.VueEl = element;
                        break;
                    }
                    else
                    {
                        Init(element.childNodes.item);
                    }


                }
            }

        }

        private void InitMethods(List<Node> items)
        {
            foreach (var item in items)
            {
                if (item is Element)
                {
                    var element = item as Element;
                    var methodAttr = element.attributes.Find(a => a.name.Equals("vue-api", StringComparison.OrdinalIgnoreCase));
                    if (methodAttr != null)
                    {
                        var api = RuleHelper.GetMethod(methodAttr.value);
                        
                        var method = new VueMethod()
                        {
                            Name = api.Method,
                            Api = methodAttr.value,
                        };
                        var callbackAttr = element.attributes.Find(a => a.name.Equals("vue-callback", StringComparison.OrdinalIgnoreCase));
                        if (callbackAttr != null)
                        {
                            method.CallbackName = callbackAttr.value;
                        }
                        Methods.Add(method);

                    }
                    else
                    {
                        InitMethods(element.childNodes.item);
                    }
                
                }
                
            }
        }
    }
    
}
