using System;
using System.Collections.Generic;
using System.Reflection;
using Kooboo.Model.Meta.Attributes;
using System.Linq;
using Kooboo.Model.Meta.Definition;
using Kooboo.Model.Meta;

namespace Kooboo.Model.Meta.Parser
{
    public class KTableParser : IMetaParser<IKoobooModel>
    {
        public IKMeta Parse(IKoobooModel model)
        {
            var type = model.GetType();
            var props = type.GetProperties();

            var meta = new KTableMeta();
            var table = new KTable();
            foreach(var prop in props)
            {
                var column = GetColumn(prop);
                if(column!=null)
                    table.Columns.Add(column);
            }

            meta.Table = table;

            var relation = type.GetCustomAttributes(typeof(RelationAttribute)).ToList()
                            .Select(a=>a as RelationAttribute).FirstOrDefault();
            if (relation != null)
            {
                var menuModel = relation.MenuModel;
                var instance = Activator.CreateInstance(menuModel) as IKoobooModel;
                meta.Menus = MetaParserHelper.GetMenu(instance);
            }

            return meta;

        }

        public Column GetColumn(PropertyInfo propertyInfo)
        {
            var attributes = propertyInfo.GetCustomAttributes().ToList()
               .Where(a => a is IMetaAttribute)
               .Select(m => m as IMetaAttribute).ToList();
            if (attributes.Count == 0)
                return null;
            var column = new Column()
            {
                Name = ToJsName(propertyInfo.Name)
            };
           

            var headers = attributes.FindAll(a => a.IsHeader);
            var cells = attributes.FindAll(a => !a.IsHeader);

            column.Header = GetHeader(headers);
            column.Cell = GetCell(cells);
            return column;
        }

        private Dictionary<string,string> GetHeader(List<IMetaAttribute> attributes)
        {
            var dic = new Dictionary<string, string>();
            var properties = typeof(Header).GetProperties();
            foreach(var property in properties)
            {
                var attr = attributes.Find(a => a.PropertyName.Equals(property.Name, StringComparison.OrdinalIgnoreCase));
                if (attr!=null)
                {
                    dic.Add(attr.PropertyName, attr.Value());
                }
            }
            return dic;
        }

        private Dictionary<string,string> GetCell(List<IMetaAttribute> attributes)
        {
            var dic = new Dictionary<string, string>();
            var properties = typeof(Cell).GetProperties();
            foreach (var property in properties)
            {
                var attr = attributes.Find(a => a.PropertyName.Equals(property.Name, StringComparison.OrdinalIgnoreCase));
                if (attr != null)
                {
                    dic.Add(attr.PropertyName, attr.Value());
                }
            }
            return dic;
        }

        private string ToJsName(string name)
        {
            if (name.Length == 1)
                return name.ToLower();

            return Char.ToLower(name[0]) + name.Substring(1);
        }


    }
}
