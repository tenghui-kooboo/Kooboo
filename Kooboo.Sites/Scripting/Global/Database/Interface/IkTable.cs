using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Data.Context;

namespace Kooboo.Sites.Scripting.Global.Database
{
    public interface IkTable
    {
        RenderContext context { get; set; }

        TableContext TableContext { get; set; }

        object add(object value);
        
        object append(object value);
        
        void update(object id, object newvalue);
        
        void update(object newvalue);
        
        void delete(object id);

        DynamicTableObject find(string searchCondition);
        
        DynamicTableObject find(string field, object value);
        
        DynamicTableObject[] findAll(string field, object value);
        
        DynamicTableObject[] findAll(string condition);
        
        DynamicTableObject get(object id);

        TableQuery Query();

        TableQuery Query(string searchCondition);
    
        void createIndex(string fieldname);
       
        DynamicTableObject[] all();

    }
}
