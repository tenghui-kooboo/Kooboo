//Copyright (c) 2018 Yardi Technology Limited. Http://www.kooboo.com 
//All rights reserved.
using Kooboo.Data.Context;
using Kooboo.Sites.Scripting.Global.Database;
 
namespace Kooboo.Sites.Scripting.Global
{ 
    public class kDatabase
    {
        private RenderContext context { get; set; }
        public kDatabase(RenderContext context)
        {
            this.context = context;
        }
         
        public KTable GetTable(string Name)
        {
            var db = Kooboo.Data.DB.GetKDatabase(this.context.WebSite);
            var tb =  db.GetOrCreateTable(Name);
            return new KTable(tb, this.context); 
        }

        public KTable Table(string Name)
        {
            return GetTable(Name); 
        }

        public KMySQL Mysql
        {
            get
            {
                return new KMySQL(context);
            }
        }


        public KTable this[string key]
        {
            get
            {
                return GetTable(key);  
            }
        }


    }  
} 