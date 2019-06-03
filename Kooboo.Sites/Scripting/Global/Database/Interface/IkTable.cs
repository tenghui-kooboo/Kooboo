using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Data.Context;

namespace Kooboo.Sites.Scripting.Global.Database
{
    public interface IkTable
    {
        RenderContext context { get; set; }
        //return primary key
        object add(object value);
        //other database doesn't need implement
        object append(object value);
        //update data,id will be primary key.
        //indexdb's id is primary key value
        void update(object id, object newvalue);
        //db need primary key
        void update(object newvalue);
        //db need primary key
        void delete(object id);

        DynamicTableObject find(string searchCondition);
        
        DynamicTableObject find(string field, object value);
        
        DynamicTableObject[] findAll(string field, object value);
        
        DynamicTableObject[] findAll(string condition);
        
        DynamicTableObject get(object id);

        ITableQuery Query();
   
        ITableQuery Query(string searchCondition);
    
        void createIndex(string fieldname);
       
        DynamicTableObject[] all();

    }
}
