namespace CosmosWizard.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;

    public static class DataTableUtil
    {
        public static DataTable CreateDataTable<T>(IEnumerable<T> list, string sheetName)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            var dataTable = new DataTable(sheetName);
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                var values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}