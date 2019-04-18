using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Kooboo.Model.Meta.Attributes;
using Kooboo.Model.Meta;
using Kooboo.Model.Meta.Definition;

namespace Kooboo.Model.Meta.Parser
{
    public class KFormParser:IMetaParser<IKoobooModel>
    {

        public IKMeta Parse(IKoobooModel type)
        {
            var form = new KFormMeta();
            var attrs = type.GetType().GetCustomAttributes().ToList()
                .Where(a=>a is IMetaAttribute)
                .Select(a => a as IMetaAttribute).ToList();

            var title = attrs.Find(a => a is TitleAttribute);
            form.Title = title.Value();

            var layout = attrs.Find(a => a is FormLayoutAttribute) as FormLayoutAttribute;
            form.Layout = layout.Layout;

            var properties = type.GetType().GetProperties().ToList();
            form.Items = MetaParserHelper.GetMeta(properties, typeof(FormItem));

            return form;
        }
    }
}
