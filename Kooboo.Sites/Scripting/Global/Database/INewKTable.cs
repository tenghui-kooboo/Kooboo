using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Data.Context;
using Kooboo.IndexedDB.Dynamic;

namespace Kooboo.Sites.Scripting.Global.Database
{
    public interface INewKTable
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

        //DynamicTableObject
        DynamicTableObject find(string searchCondition);
        //DynamicTableObject
        DynamicTableObject find(string field, object value);
        //DynamicTableObject
        DynamicTableObject[] findAll(string field, object value);
        //DynamicTableObject
        DynamicTableObject[] findAll(string condition);
        //DynamicTableObject
        DynamicTableObject get(object id);
        //todo extend/rewrite
        TableQuery Query();
        //todo extend/rewrite
        TableQuery Query(string searchCondition);
        //create index
        void createIndex(string fieldname);
        //DynamicTableObject
        DynamicTableObject[] all();

    }
}
