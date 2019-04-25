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

        public TableMetaBuilder<T> Before<TCell>(string before, string name, string header, Action<TCell> configureCell)
                where TCell : Cell
        {
            var index = Meta.Columns.FindIndex(before);
            if (index < 0)
                ThrowNotFoundException(before);

            if (!Meta.Columns.TryInsert(index, name, out Column column))
                ThrowDuplicatedException(name);

            ConfigureColumn(column, header, configureCell);
            return this;
        }

        public TableMetaBuilder<T> Before<TCell>(Expression<Func<T, object>> before, Expression<Func<T, object>> name, string header, Action<TCell> configureCell)
            where TCell : Cell
        {
            return Before(before.PropertyName(), name.PropertyName(), header, configureCell);
        }

        public TableMetaBuilder<T> After<TCell>(string after, string name, string header, Action<TCell> configureCell)
            where TCell : Cell
        {
            var index = Meta.Columns.FindIndex(after);
            if (index < 0)
                ThrowNotFoundException(after);

            var added = index == Meta.Columns.Count - 1 ?
                Meta.Columns.TryAdd(name, out Column column) :
                Meta.Columns.TryInsert(index + 1, name, out column);

            if (!added)
                ThrowDuplicatedException(name);

            ConfigureColumn(column, header, configureCell);
            return this;
        }

        public TableMetaBuilder<T> After<TCell>(Expression<Func<T, object>> after, Expression<Func<T, object>> name, string header, Action<TCell> configureCell)
            where TCell : Cell
        {
            return After(after.PropertyName(), name.PropertyName(), header, configureCell);
        }

        public TableMetaBuilder<T> Column<TCell>(string name, string header, Action<TCell> configureCell)
           where TCell : Cell
        {
            if (!Meta.Columns.TryAdd(name, out Column column))
                ThrowDuplicatedException(name);

            ConfigureColumn(column, header, configureCell);
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
            if (!Meta.Columns.TryGet(name, out Column column))
                ThrowNotFoundException(name);

            configureCell?.Invoke(column.Cell as TCell);
            return this;
        }

        public TableMetaBuilder<T> Column<TCell>(Expression<Func<T, object>> getName, Action<TCell> configureCell)
            where TCell : Cell
        {
            return Column(getName.PropertyName(), configureCell);
        }

        private void ConfigureColumn<TCell>(Column column, string header, Action<TCell> configureCell)
            where TCell : Cell
        {
            var cell = Activator.CreateInstance<TCell>();
            configureCell?.Invoke(cell);

            column.Header.Text = header;
            column.Cell = cell;
        }

        private void ThrowNotFoundException(string name) => throw new KeyNotFoundException($"{name} not found in {typeof(T).Name} collection");

        private void ThrowDuplicatedException(string name) => throw new DuplicateWaitObjectException($"{name} already exist in {typeof(T).Name} collection");
    }

    public static class TableMetaBuilderExtensions
    {
        public static TableMetaBuilder<T> Builder<T>(this TableMeta meta)
        {
            return new TableMetaBuilder<T>(meta);
        }
    }
}
