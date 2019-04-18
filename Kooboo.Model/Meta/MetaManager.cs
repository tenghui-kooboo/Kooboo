using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Parser;

namespace Kooboo.Model.Meta
{
    public static class MetaManager
    {
        private static Dictionary<ModelType, IMetaParser<IKoobooModel>> Parsers=new Dictionary<ModelType, IMetaParser<IKoobooModel>>();

        static MetaManager()
        {
            Parsers.Add(ModelType.KForm,new KFormParser());
            Parsers.Add(ModelType.KTable,new KTableParser());

        }
        public static IMetaParser<IKoobooModel> GetParser(ModelType type)
        {
            if (Parsers.ContainsKey(type))
                return Parsers[type];
            return null;
        }
        public static IKMeta GetMeta(IKoobooModel model)
        {
            var parser = GetParser(model.ModelType);
            return parser.Parse(model);
        }

        public static string GetMetaString(IKoobooModel model)
        {
            var meta = GetMeta(model);
            return Kooboo.Lib.Helper.JsonHelper.Serialize(meta);
        }

    }

}
