using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Kooboo.Model.Meta.Attributes;
using Kooboo.Model.Meta;
using Kooboo.Model.Meta.Definition;
using Kooboo.Model.Setting;

namespace Kooboo.Model.Meta.Parser
{
    public class KFormParser:IMetaParser<IKoobooModel>
    {

        public IKMeta Parse(IKoobooModel type)
        {
            var meta = new KFormMeta();
            var attrs = type.GetType().GetCustomAttributes().ToList()
                .Where(a=>a is Attribute)
                .Select(a => a as Attribute).ToList();

            var form = new KForm();
            var layout = attrs.Find(a => a is FormLayoutAttribute) as FormLayoutAttribute;
            if (layout != null)
                form.Layout = layout.Layout;

            var properties = type.GetType().GetProperties().ToList();
            form.Items = MetaParserHelper.GetMeta(properties, typeof(FormItem));

            var relation = attrs.Find(a => a is RelationAttribute) as RelationAttribute;
            if (relation != null)
            {
                meta.Popup = Activator.CreateInstance(relation.Model) as IPopup;
                form.SubmitApi = relation.SubmitApi;
                form.LoadApi = relation.LoadDataApi;

                meta.Form = form;
            }

            return meta;
        }
    }
}
