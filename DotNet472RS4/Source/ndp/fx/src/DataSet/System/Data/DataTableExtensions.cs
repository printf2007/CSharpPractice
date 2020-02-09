//------------------------------------------------------------------------------
// <copyright file="DataTableExtenstions.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <owner current="true" primary="true">Microsoft</owner>
// <owner current="true" primary="false">Microsoft</owner>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Linq.Expressions;
using System.Globalization;
using System.Diagnostics;
using System.Data.DataSetExtensions;

namespace System.Data
{

    /// <summary>
    /// This static class defines the DataTable extension methods.
    /// </summary>
    public static class DataTableExtensions
    {

        /// <summary>
        ///   This method returns a IEnumerable of Datarows.
        /// </summary>
        /// <param name="source">
        ///   The source DataTable to make enumerable.
        /// </param>
        /// <returns>
        ///   IEnumerable of datarows.
        /// </returns>
        public static EnumerableRowCollection<DataRow> AsEnumerable(this DataTable source)
        {
            DataSetUtil.CheckArgumentNull(source, "source");
            return new EnumerableRowCollection<DataRow>(source);
        }

        /// <summary>
        ///   This method takes an input sequence of DataRows and produces a DataTable object
        ///   with copies of the source rows.
        ///   Also note that this will cause the rest of the query to execute at this point in time
        ///   (e.g. there is no more delayed execution after this sequence operator).
        /// </summary>
        /// <param name="source">
        ///   The input sequence of DataRows
        /// </param>
        /// <returns>
        ///   DataTable containing copies of the source DataRows.
        ///   Properties for the DataTable table will be taken from first DataRow in the source.
        /// </returns>
        /// <exception cref="ArgumentNullException">if source is null</exception>
        /// <exception cref="InvalidOperationException">if source is empty</exception>
        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source)
            where T : DataRow
        {
            DataSetUtil.CheckArgumentNull(source, "source");
            return LoadTableFromEnumerable(source, null, null, null);
        }

        /// <summary>
        ///   delegates to other CopyToDataTable overload with a null FillErrorEventHandler.
        /// </summary>
        public static void CopyToDataTable<T>(this IEnumerable<T> source, DataTable table, LoadOption options)
            where T : DataRow
        {
            DataSetUtil.CheckArgumentNull(source, "source");
            DataSetUtil.CheckArgumentNull(table, "table");
            LoadTableFromEnumerable(source, table, options, null);
        }


        /// <summary>
        ///   This method takes an input sequence of DataRows and produces a DataTable object
        ///   with copies of the source rows.
        ///   Also note that this will cause the rest of the query to execute at this point in time
        ///   (e.g. there is no more delayed execution after this sequence operator).
        /// </summary>
        /// <param name="source">
        ///   The input sequence of DataRows
        ///
        ///   CopyToDataTable uses DataRowVersion.Default when retrieving values from source DataRow
        ///   which will include proposed values for DataRow being edited.
        ///
        ///   Null DataRow in the sequence are skipped.
        /// </param>
        /// <param name="table">
        ///   The target DataTable to load.
        /// </param>
        /// <param name="options">
        ///   The target DataTable to load.
        /// </param>
        /// <param name="errorHandler">
        /// Error handler for recoverable errors.
        /// Recoverable errors include:
        ///     A source DataRow is in the deleted or detached state state.
        ///     DataTable.LoadDataRow threw an exception, i.e. wrong # of columns in source row
        /// Unrecoverable errors include:
        ///     exceptions from IEnumerator, DataTable.BeginLoadData or DataTable.EndLoadData
        /// </param>
        /// <returns>
        ///   DataTable containing copies of the source DataRows.
        /// </returns>
        /// <exception cref="ArgumentNullException">if source is null</exception>
        /// <exception cref="ArgumentNullException">if table is null</exception>
        /// <exception cref="InvalidOperationException">if source DataRow is in Deleted or Detached state</exception>
        public static void CopyToDataTable<T>(this IEnumerable<T> source, DataTable table, LoadOption options, FillErrorEventHandler errorHandler)
            where T : DataRow
        {
            DataSetUtil.CheckArgumentNull(source, "source");
            DataSetUtil.CheckArgumentNull(table, "table");
            LoadTableFromEnumerable(source, table, options, errorHandler);
        }

        private static DataTable LoadTableFromEnumerable<T>(IEnumerable<T> source, DataTable table, LoadOption? options, FillErrorEventHandler errorHandler)
            where T : DataRow
        {
            if (options.HasValue) {
                switch(options.Value) {
                    case LoadOption.OverwriteChanges:
                    case LoadOption.PreserveChanges:
                    case LoadOption.Upsert:
                        break;
                    default:
                        throw DataSetUtil.InvalidLoadOption(options.Value);
                }
            }


            using (IEnumerator<T> rows = source.GetEnumerator())
            {
                // need to get first row to create table
                if (!rows.MoveNext())
                {
                    if (table == null)
                    {
                        throw DataSetUtil.InvalidOperation(Strings.DataSetLinq_EmptyDataRowSource);
                    }
                    else
                    {
                        return table;
                    }
                }

                DataRow current;
                if (table == null)
                {
                    current = rows.Current;
                    if (current == null)
                    {
                        throw DataSetUtil.InvalidOperation(Strings.DataSetLinq_NullDataRow);
                    }

                    table = new DataTable();
                    table.Locale = CultureInfo.CurrentCulture;
                    // We do not copy the same properties that DataView.ToTable does.
                    // If user needs that functionality, use other CopyToDataTable overloads.
                    // The reasoning being, the IEnumerator<DataRow> can be sourced from
                    // different DataTable, so we just use the "Default" instead of resolving the difference.

                    foreach (DataColumn column in current.Table.Columns)
                    {
                        table.Columns.Add(column.ColumnName, column.DataType);
                    }
                }

                table.BeginLoadData();
                try
                {
                    do
                    {
                        current = rows.Current;
                        if (current == null)
                        {
                            continue;
                        }

                        object[] values = null;
                        try
                        {   // 'recoverable' error block
                            switch(current.RowState)
                            {
                                case DataRowState.Detached:
                                    if (!current.HasVersion(DataRowVersion.Proposed))
                                    {
                                        throw DataSetUtil.InvalidOperation(Strings.DataSetLinq_CannotLoadDetachedRow);
                                    }
                                    goto case DataRowState.Added;
                                case DataRowState.Unchanged:
                                case DataRowState.Added:
                                case DataRowState.Modified:
                                    values = current.ItemArray;
                                    if (options.HasValue)
                                    {
                                        table.LoadDataRow(values, options.Value);
                                    }
                                    else
                                    {
                                        table.LoadDataRow(values, true);
                                    }
                                    break;
                                case DataRowState.Deleted:
                                    throw DataSetUtil.InvalidOperation(Strings.DataSetLinq_CannotLoadDeletedRow);
                                default:
                                    throw DataSetUtil.InvalidDataRowState(current.RowState);
                            }
                        }
                        catch (Exception e)
                        {
                            if (!DataSetUtil.IsCatchableExceptionType(e))
                            {
                                throw;
                            }

                            FillErrorEventArgs fillError = null;
                            if (null != errorHandler)
                            {
                                fillError = new FillErrorEventArgs(table, values);
                                fillError.Errors = e;
                                errorHandler.Invoke(rows, fillError);
                            }
                            if (null == fillError) {
                                throw;
                            }
                            else if (!fillError.Continue)
                            {
                                if (Object.ReferenceEquals(fillError.Errors ?? e, e))
                                {   // if user didn't change exception to throw (or set it to null)
                                    throw;
                                }
                                else
                                {   // user may have changed exception to throw in handler
                                    throw fillError.Errors;
                                }
                            }
                        }
                    } while (rows.MoveNext());
                }
                finally
                {
                    table.EndLoadData();
                }
            }
            Debug.Assert(null != table, "null DataTable");
            return table;
        }

        #region Methods to Create LinqDataView

        /// <summary>
        /// Creates a LinkDataView of DataRow over the input table.
        /// </summary>
        /// <param name="table">DataTable that the view is over.</param>
        /// <returns>An instance of LinkDataView.</returns>
        public static DataView AsDataView(this DataTable table)
        {
            DataSetUtil.CheckArgumentNull<DataTable>(table, "table");
            return new LinqDataView(table, null);
        }


        /// <summary>
        /// Creates a LinqDataView from EnumerableDataTable
        /// </summary>
        /// <typeparam name="T">Type of the row in the table. Must inherit from DataRow</typeparam>
        /// <param name="source">The enumerable-datatable over which view must be created.</param>
        /// <returns>Generated LinkDataView of type T</returns>
        public static DataView AsDataView<T>(this EnumerableRowCollection<T> source) where T : DataRow
        {
            DataSetUtil.CheckArgumentNull<EnumerableRowCollection<T>>(source, "source");
            return source.GetLinqDataView();
        }

        #endregion LinqDataView

    }
}
