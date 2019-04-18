using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Kooboo.Model.Meta;
using Kooboo.Model.Meta.Definition;

namespace Kooboo.Model.Meta.Parser
{
    public class KMenuBtnParser :IMetaParser<IKoobooModel>
    {
        public IKMeta Parse(IKoobooModel model)
        {
            var meta = new KMenuMeta();
            var properties = model.GetType().GetProperties().ToList();
           
            meta.MenuBtns = MetaParserHelper.GetMeta(properties, typeof(MenuBtn));

            return meta;
        }
    }
}
