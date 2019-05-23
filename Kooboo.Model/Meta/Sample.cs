using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Form;
using Kooboo.Model.Meta.Table;

namespace Kooboo.Model.Meta
{
    class Model
    {
        public string Name { get; set; }
    }

    class Sample : IMetaConfigure<Model, Table.TableMeta>, IMetaConfigure<Model, Form.FormMeta>
    {
        public void Configure(TableMeta meta)
        {
        }

        void IMetaConfigure<Model, FormMeta>.Configure(FormMeta meta)
        {
            meta.Builder<Model>()
                .MergeModel();
        }
    }
}
