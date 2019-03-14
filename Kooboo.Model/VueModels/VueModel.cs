using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.ValidateRules;
using Kooboo.Model.Setting;
using System.Reflection;

namespace Kooboo.Model
{
    public class VueModel
    {
        public string El { get; set; }

        public List<VueField> Data { get; set; }

        public List<VueMethod> Methods { get; set; }

        public List<string> Computed { get; set; }

        public VueCreated VueCreated { get; set; }

        public string GetJs()
        {
            var sb = new StringBuilder();
            int tabCount = 0;

            var body = GetJsBody(tabCount);

           
            sb.AppendTabs(tabCount, "var vm=new Vue({");
            
            sb.Append(body);
            sb.AppendTabs(tabCount, "})");
            return sb.ToString();
        }

        public string GetJsBody(int tabCount)
        {
            tabCount ++;
            var sb = new StringBuilder();
            sb.AppendTabs(tabCount, GetEl());
            sb.Append(GetData(tabCount));
            sb.Append(GetMethod(tabCount));
            sb.Append(GetCreated(tabCount));
            //sb.Append(GetComputed());
            return sb.ToString();
        }
        public string GetEl()
        {
            return string.Format("el:'{0}',", El);
        }

        public string GetData(int tabCount)
        {
            var sb = new StringBuilder();

            sb.AppendTabs(tabCount, "data:{");

            int newTabCount = tabCount + 1;
            for (var i=0;i<Data.Count;i++)
            {
                var field = Data[i];
                
                sb.AppendTabs(newTabCount, field.Name + ":");
                //todo review
                field.TabCount = newTabCount + 1;
                sb.Append(field.GetDataValue());
            }
            sb.AppendTabs(tabCount,"},");
            return sb.ToString();
        }

        public string GetMethod(int tabCount)
        {
            
            var sb = new StringBuilder();
            sb.AppendTabs(tabCount,"methods:{");

            var newTabCount = tabCount + 1;
            foreach (var method in Methods)
            {
                method.TabCount = newTabCount;
                sb.Append(method.GetJs());
            }

            sb.AppendTabs(tabCount,"},");
            return sb.ToString();
        }

        public string GetComputed()
        {
            var sb = new StringBuilder();
            sb.Append("computed:{");
            foreach(var compute in Computed)
            {
                //todo implement
            }
            sb.Append("},");
            return sb.ToString();
        }

        public string GetCreated(int tabCount)
        {
            //VueCreated data can be removed.
            if (VueCreated == null || string.IsNullOrEmpty(VueCreated.API))
                return string.Empty;

            var sb = new StringBuilder();
            sb.Append("created:function(){");
            sb.AppendLine();
            //sb.AppendTabs(tabCount, string.Format("Kooboo.Table.getList(this,'{0}','{1}')", VueCreated.API, VueCreated.ModelType)); 
            sb.AppendTabs(tabCount, string.Format("Kooboo.Table.getList(this.tableData,this,'{0}')",VueCreated.API));
            sb.Append("}");
            return sb.ToString();
        }
    }


    


}
