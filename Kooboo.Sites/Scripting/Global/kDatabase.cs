//Copyright (c) 2018 Yardi Technology Limited. Http://www.kooboo.com 
//All rights reserved.
using System;
using Kooboo.Data.Context;
using Kooboo.Sites.Scripting.Global.Db;
using System.Collections.Generic;
 
namespace Kooboo.Sites.Scripting.Global
{ 
    public class kDatabase
    {
        private RenderContext context { get; set; }
        public kDatabase(RenderContext context)
        {
            this.context = context;
        }
         
        public IkTable GetTable(string Name)
        {
            //use indexdb by default
            var db = new IndexDB(context);
            return db.GetTable(Name);
        }

        public IkTable Table(string Name)
        {
            return GetTable(Name); 
        }

        //return database/Iktable
        public object this[string key]
        {
            get
            {
                var db = GetDatabase(key);
                if (db!=null)
                {
                    return db;
                }

                return GetTable(key);
            }
        }

        private Database GetDatabase(string name)
        {
            if (DataBases.ContainsKey(name))
            {
                var type = DataBases[name];
                return Activator.CreateInstance(type, context) as Database;
            }
            return null;
        }

        private static object _lockObj = new object();

        private static Dictionary<string, Type> dataBases;

        private static Dictionary<string,Type> DataBases
        {
            get
            {
                if (dataBases == null)
                {
                    lock (_lockObj)
                    {
                        if (dataBases == null)
                        {
                            dataBases = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
                            var types= Kooboo.Lib.Reflection.AssemblyLoader.LoadTypeByInterface(typeof(Database));

                            var rendercontext = new RenderContext();
                            foreach(var type in types)
                            {
                                try
                                {
                                    var instance = Activator.CreateInstance(type, rendercontext) as Database;
                                    dataBases[instance.Name] = type;
                                }
                                catch
                                {

                                }
                                
                            }
                        }
                    }
                }
                return dataBases;
            }
        }

    }  
} 