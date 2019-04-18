using System;
using System.Collections.Generic;
using Kooboo.Data.Context;
using Kooboo.Model.Render;
using Kooboo.Lib.Reflection;
using System.Reflection.Emit;
using Kooboo.Model.Meta;

namespace Kooboo.Model
{
    public static class KoobooModelManager
    {
        //private static Dictionary<string,Type> Models = new Dictionary<string,Type>(StringComparer.OrdinalIgnoreCase);
        private static Dictionary<string, IKoobooModel> Models = new Dictionary<string, IKoobooModel>(StringComparer.OrdinalIgnoreCase);

        private static Dictionary<string, IKMeta> Metas = new Dictionary<string, IKMeta>();
        static KoobooModelManager()
        {
            foreach (var item in AssemblyLoader.LoadTypeByInterface(typeof(IKoobooModel)))
            {
                var instance = Activator.CreateInstance(item) as IKoobooModel;
                if (instance != null)
                {
                    Models[instance.ModelName] = instance;
                }
                
            }
        }
        public static string Render(string html, ModelRenderContext context)
        {
            if (!IsNeedRender(context.HttpContext))
                return html;
            html = new ViewParser(ViewParseOptions.Instance).RenderRootView(html, context);
            
            return html;
        }
        private static bool IsNeedRender(RenderContext context)
        {
            var url = context.Request.Path;
            return url.StartsWith("/_Admin/Vue", StringComparison.OrdinalIgnoreCase) ||
                url.StartsWith("/_Admin/account/Vue", StringComparison.OrdinalIgnoreCase);
           
        }

        public static IKoobooModel GetMetaModel(string modelName)
        {
            return Models[modelName];
        }
        public static IKMeta GetMeta(string modelName)
        {
            if (Metas.ContainsKey(modelName))
            {
                return Metas[modelName];
            }
            else
            {
                if (Models.ContainsKey(modelName))
                {
                    var model = Models[modelName];
                    var meta = MetaManager.GetMeta(model);
                    Metas[modelName] = meta;
                    return meta;
                }
            }

            return null;
        }
        //public static string GetMeta(string modelName)
        //{
        //    if (Metas.ContainsKey(modelName))
        //    {
        //        return Metas[modelName];
        //    }
        //    else
        //    {
        //        if (Models.ContainsKey(modelName))
        //        {
        //            var model = Models[modelName];
        //            var meta= MetaManager.GetMetaString(model);
        //            Metas[modelName] = meta;
        //            return meta;
        //        }
        //    }
            
        //    return "";
        //}

        //private static string GetModelName(Type type)
        //{
        //    var method = type.GetProperty("ModelName").GetGetMethod();
        //    var dynamicMethod = new DynamicMethod("model", typeof(string), Type.EmptyTypes);
        //    var generator = dynamicMethod.GetILGenerator();
        //    generator.Emit(OpCodes.Ldnull);
        //    generator.Emit(OpCodes.Call, method);
        //    generator.Emit(OpCodes.Ret);

        //    var model= (Func<string>)dynamicMethod.CreateDelegate(
        //               typeof(Func<string>));
        //    return model();
        //}
    }
}
