using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Data.Context;

namespace Kooboo.Sites.Scripting.Global.Database
{
    public interface IkTable
    {
        Database Database { get; set; }

        string Name { get; set; }

        RenderContext context { get; set; }

        object add(object value);

        object append(object value);

        void update(object id, object newvalue);

        void update(object newvalue);

        void delete(object id);

        object find(string searchCondition);

        object[] findAll(string condition);

        object get(object id);
    }
}
