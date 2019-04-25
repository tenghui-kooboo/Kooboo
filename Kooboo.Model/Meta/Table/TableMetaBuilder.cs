using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Kooboo.Model.Meta.Table
{
    public class TableMetaBuilder<T>
    {
        public TableMetaBuilder(TableMeta meta)
        {
            Meta = meta;
        }

        public TableMeta Meta { get; }

        public TableMetaBuilder<T> Column<TCell>(string name, string header, Action<TCell> configureCell)
           where TCell : Cell
        {
            var column = Meta.Columns.EnsureItem(name);

            var cell = Activator.CreateInstance<TCell>();
            configureCell?.Invoke(cell);

            column.Header.Text = header;
            column.Cell = cell;

            return this;
        }

        public TableMetaBuilder<T> Column<TCell>(Expression<Func<T, object>> getName, string header, Action<TCell> configureCell)
            where TCell : Cell
        {
            return Column(getName.PropertyName(), header, configureCell);
        }

        public TableMetaBuilder<T> Column<TCell>(string name, Action<TCell> configureCell)
            where TCell : Cell
        {
            var column = Meta.Columns.EnsureItem(name);

            configureCell?.Invoke(column.Cell as TCell);

            return this;
        }

        public TableMetaBuilder<T> Column<TCell>(Expression<Func<T, object>> getName, Action<TCell> configureCell)
            where TCell : Cell
        {
            return Column(getName.PropertyName(), configureCell);
        }
    }

    public static class TableMetaBuilderExtensions
    {
        public static TableMetaBuilder<T> Builder<T>(this TableMeta meta)
        {
            return new TableMetaBuilder<T>(meta);
        }
    }
}
