using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using System.Windows.Forms;

namespace TARGITD3FOConnection
{
    public class DataReaderParser
    {
        private DbDataReader _dr; // String.Empty;
       
        public DbDataReader Dr
        {
            get { return this._dr; }
            set { this._dr = value; }
        }

        public DataReaderParser()
        {
            _dr = null;
        }
        public DataReaderParser(DbDataReader d)
        {
            this.Dr = d;
        }
        /////
        public  DataTable ConvertDataReaderToTableManually()
        {
            DataTable dt = new DataTable();
            try
            {

                DataTable dtSchema = _dr.GetSchemaTable();

                // You can also use an ArrayList instead of List<>
                List<DataColumn> listCols = new List<DataColumn>();

                if (dtSchema != null)
                {
                    foreach (DataRow drow in dtSchema.Rows)
                    {
                        string columnName = System.Convert.ToString(drow["ColumnName"]);
                        //  DataColumn column = new DataColumn(columnName, (Type)(drow["DataType"]));
                        DataColumn column = new DataColumn(columnName);
                        column.Unique = false; // (bool)drow["IsUnique"];
                        column.AllowDBNull = true; // (bool)drow["AllowDBNull"];
                        column.AutoIncrement = false; // (bool)drow["IsAutoIncrement"];
                        listCols.Add(column);
                        dt.Columns.Add(column);
                       
                    }
                }

                // Read rows from DataReader and populate the DataTable
                while (_dr.Read())
                {
                    DataRow dataRow = dt.NewRow();
                    for (int i = 0; i < listCols.Count; i++)
                    {
                        dataRow[((DataColumn)listCols[i])] = _dr[i];
                    }
                    dt.Rows.Add(dataRow);
                }
               
                return dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error" + ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ex Error" + ex.ToString());
                return null;
            }
            finally
            {
               //

            }
           
        }
        //
        public string DumpDataTable(DataTable table)
        {
            string data = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (null != table && null != table.Rows)
            {
                foreach (DataRow dataRow in table.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        sb.Append(item);
                        sb.Append(',');
                    }
                    sb.AppendLine();
                }

                data = sb.ToString();
            }
            return data;
        }
    }
}
