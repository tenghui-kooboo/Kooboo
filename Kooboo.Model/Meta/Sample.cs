using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Kooboo.Model.Meta.Form;
using Kooboo.Model.Meta.Table;

namespace Kooboo.Model.Meta
{
    class Sample : ITableMetaConfigure<Entity>, IFormMetaConfigure<Entity>
    {
        bool IMetaConfigure<Entity, TableMeta>.IsCreator => false;

        void IMetaConfigure<Entity, TableMeta>.Configure(TableMeta meta)
        {
            meta.Builder<ListViewModel>()
                .MergeModel();
        }

        bool IMetaConfigure<Entity, FormMeta>.IsCreator => false;

        void IMetaConfigure<Entity, FormMeta>.Configure(FormMeta meta)
        {
            meta.Builder<DetailsViewModel>()
                .MergeModel();
        }

    }

    class Entity : Kooboo.Data.Interface.ISiteObject
    {
        public string Name { get; set; }
        public byte ConstType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CreationDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime LastModified { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
