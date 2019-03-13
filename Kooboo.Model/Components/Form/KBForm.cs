using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components.Form
{
    public class KBForm:IComponent
    {
        public ComponentType Type => ComponentType.KForm;

        public List<FormField> Fields { get; set; } = new List<FormField>();

        public RenderContext Context { get; set; }

        //"fields:[]"
        public VueField GetField()
        {
            var vueField = new VueField();
            vueField.Name = "fields";

            var list = new List<Dictionary<string,object>>();
            foreach(var field in Fields)
            {
                list.Add(field.GetData(Context));
            }
            vueField.Value = list;
            return vueField;
        }

        public bool IsMatch(Attribute attribute)
        {
            return attribute is FormFieldAttribute;
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
                    var fieldName = keyPair.Key;
                    var list = keyPair.Value;

                    foreach (var item in list)
                    {
                        var formFieldAttr = item as FormFieldAttribute;
                        if (formFieldAttr == null) continue;

                        var component = ComponentManager.Instance.Get(formFieldAttr.ComponentType);
                        if(component!=null)
                        {
                            var formFieldAttrs = GetFormFieldAttrs(fieldName, list);
                            component.SetData(formFieldAttrs);

                            Fields.Add(new FormField()
                            {
                                Name = fieldName,
                                DisplayName = formFieldAttr.DisplayName,
                                Component=component as IBaseComponent
                            });
                        }
                        
                    }

                }
            }
        }

        private List<Dictionary<string,List<Attribute>>> GetFormFieldAttrs(string fieldName, List<Attribute> attributes)
        {
            var formFieldAttrs = new List<Dictionary<string, List<Attribute>>>();

            var dic = new Dictionary<string, List<Attribute>>();
            dic.Add(fieldName, attributes);

            formFieldAttrs.Add(dic);

            return formFieldAttrs;
        }
    }

    public class FormField
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public IBaseComponent Component { get; set; }

        public Dictionary<string,object> GetData(RenderContext context)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("name", Name);
            dic.Add("displayName", ModelHelper.GetMultiLang(DisplayName,context));
            dic.Add("type", Component.Type.ToString());
            //some component needs data. 
            //like select component needs options
            dic.Add("data", Component.GetData());

            return dic;
        }
    }
}
