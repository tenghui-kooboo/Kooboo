using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Kooboo.Model.Meta.Form;
using Kooboo.Model.Meta.Table;

namespace Kooboo.Model.Meta
{
    class Sample : IMetaCreator<Entity, Table.TableMeta>, IMetaCreator<Entity, Form.FormMeta>
    {
        void IMetaConfigure<Entity, TableMeta>.Configure(TableMeta meta)
        {
            meta.Builder<ListViewModel>()
                .MergeModel();
        }

        void IMetaConfigure<Entity, FormMeta>.Configure(FormMeta meta)
        {
            meta.Builder<DetailsViewModel>()
                .MergeModel();
        }


    }

    class Entity
    {
        public string Name { get; set; }
    }

    class ListViewModel
    {
        [LinkColumn(ActionType.Redirect, "/_admin/test/list")]
        public string Name { get; set; }
    }

    class DetailsViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    class EntityApi
    {
        void Delete()
        {
        }
    }
}
