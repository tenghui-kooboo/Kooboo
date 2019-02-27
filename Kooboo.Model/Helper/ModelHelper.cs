using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Kooboo.Model.Components.Table.Attributes;
using Kooboo.Model.Components.Table;
using Kooboo.Data.Context;
using Kooboo.Data.Language;
using Kooboo.Api;

namespace Kooboo.Model.Helper
{
    public class ModelHelper
    {
        /// <summary>
        /// get table model by type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static TableModel GetTableSetting(Type type,RenderContext context)
        {
            if (typeof(ITable).IsAssignableFrom(type))
            {
                var tableModel = new TableModel();

                tableModel.ModelName =GetModelName(type);

                var props = type.GetProperties();

                foreach(var prop in props)
                {
                    var attributes = prop.GetCustomAttributes().ToList();
                    foreach(var attr in attributes)
                    {
                        if(attr is TableActionAttribute)
                        {
                            var tableAction = attr as TableActionAttribute;
                            tableModel.Actions.Add(new TableAction()
                            {
                                ActionName=tableAction.DisplayName,
                                DisplayName= Hardcoded.GetValue(tableAction.DisplayName,context),
                                Url=tableAction.Url
                            });

                        }
                        else if(attr is ColumnAttribute)
                        {
                            var columnAttr = attr as ColumnAttribute;
                            tableModel.Columns.Add(new TableColumn()
                            {
                                //todo review
                                FieldName = prop.Name.Substring(0,1).ToLower()+prop.Name.Substring(1),
                                DisplayName= Hardcoded.GetValue(columnAttr.DisplayName,context),
                                CellType=columnAttr.CellType,
                                CellDataType=columnAttr.CellDataType
                            });
                        }
                        else if (attr is RowActionAttribute)
                        {
                            var rowActionAttr = attr as RowActionAttribute;
                            tableModel.RowActions.Add(new TableRowAction()
                            {
                                FieldName=rowActionAttr.FieldName,
                                DisplayName = Hardcoded.GetValue(rowActionAttr.DisplayName,context),
                                CellType=rowActionAttr.CellType
                            });
                        }
                        else if(attr is UrlAttribute)
                        {
                            tableModel.UrlColumns.Add(attr as UrlAttribute);
                        }
                    }

                }
                return tableModel;
            }
            return null;
        }

        private static string GetModelName(Type type)
        {
            var modelName = type.Name;
            var modelAttr = type.GetCustomAttribute(typeof(ModelNameAttribute)) as ModelNameAttribute;
            if(modelAttr!=null)
            {
                modelName = modelAttr.ModelName;
            }
            return modelName;
        }
        /// <summary>
        /// get kooboosetting from api
        /// </summary>
        /// <param name="api"></param>
        /// <param name="apiProvider"></param>
        /// <returns></returns>
        public static KoobooSetting GetSetting(string api, IApiProvider apiProvider)
        {
            var methodInfo =ModelApiHelper.GetMethodInfoByApi(api, apiProvider);
            var setting = methodInfo.GetCustomAttributes(typeof(KoobooSetting), true);
            return setting.ToList().Select(s => s as KoobooSetting).FirstOrDefault();
        }
    }
}
