using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Sites.Scripting.Global.Db
{
    public class TableColumn
    {
        public TableColumn()
        {

        }
        public TableColumn(KeyValuePair<string, object> dic)
        {
            Name = dic.Key;
            DataType = TypeConverter.GetDataType(dic.Value.GetType());
        }

        public TableColumn(IDictionary<string, object> dic)
        {
            this.Name = dic["Field"] as string;
            this.DataType = dic["Type"] as string;
            Null = dic["Null"] as string;
            Key = dic["Key"] as string;//PRI
            IsPrimary = !string.IsNullOrEmpty(Key) && "PRI".Equals(Key, StringComparison.OrdinalIgnoreCase);
            Default = dic["Default"] as string;
            Extra = dic["Extra"] as string;//auto_increment
        }

        public string Name { get; set; }

        public string DataType { get; set; }

        public bool IsPrimary { get; set; }

        private bool _isNew;
        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                IsChange = true;
                _isNew = true;
            }
        }

        public bool IsChange { get; set; }

        #region extra schema message
        public string Null { get; set; }

        public string Key { get; set; }

        public string Default { get; set; }

        public string Extra { get; set; }
        #endregion

    }
}
