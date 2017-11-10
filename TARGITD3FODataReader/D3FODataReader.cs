using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics;
using TARGITD3FODataReader.Models;

namespace TARGITD3FOConnection
{
    [DtsPipelineComponent(DisplayName = "TARGITD3FODataReader",
    ComponentType = ComponentType.SourceAdapter,  Description = "TARGITD3FO Connection Source",
   UITypeName = " TARGITD3FOConnection.TARGITD3FODataReaderInterface, TARGITD3FODataReader, Version = 1.0.0.0, Culture = neutral, PublicKeyToken=71aabddac4fee55e")]
        public class TARGITD3FODataReader : PipelineComponent
    {
        private DbDataReader sqlReader;
        public D3FOConnectionManager cm;
      
        public IDTSRuntimeConnection100 connection;
        public IDTSOutput100 output;
        public DbConnection sqlConn;
        public int[] mapOutputColsToBufferCols;
        private string queueName;
        int ColumnsCount;

        public TARGITD3FODataReader()
        {


            cm = new D3FOConnectionManager();
            cm.AcquireConnection(null);
            sqlConn = cm.GetDbConnection();
            

        }

        public override void ProvideComponentProperties()
        {
            base.RemoveAllInputsOutputsAndCustomProperties();
            ComponentMetaData.RuntimeConnectionCollection.RemoveAll();

            output = ComponentMetaData.OutputCollection.New();
            output.Name = "DataOutput";
            //define custom parameter for query
            IDTSCustomProperty100 queueName = ComponentMetaData.CustomPropertyCollection.New();
            queueName.Name = "QueueName";
            queueName.Description = "The name of the queue to read messages from";
            queueName.Value = String.Empty;
            

            connection = ComponentMetaData.RuntimeConnectionCollection.New();
            connection.Name = "TARGITD3FOConnection";
            connection.ConnectionManagerID = "TARGITDataSource";
       
        }
        public override void AcquireConnections(object transaction)
        {
            if (ComponentMetaData.RuntimeConnectionCollection[0].ConnectionManager != null)
            {
                ConnectionManager connectionManager = Microsoft.SqlServer.Dts.Runtime.DtsConvert.GetWrapper(
                ComponentMetaData.RuntimeConnectionCollection[0].ConnectionManager);
                this.cm = connectionManager.InnerObject as D3FOConnectionManager;
               
                if (this.cm == null)
                    throw new Exception("Couldn't get the cm connection manager, ");
                
                this.queueName = ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString();
                connection = this.cm.AcquireConnection(transaction) as IDTSRuntimeConnection100;
                
            }

        }

        public override void ReleaseConnections()
        {
          //  if (connection.ConnectionManager != null)
          //   {
                //this.connection.ConnectionManager.ReleaseConnection(connection);
                base.ReleaseConnections();
          //   }
        }
        // [CLSCompliant(false)]
        public override DTSValidationStatus Validate()
                {
        /*   bool cancel;
            string qName = ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString();

            if (string.IsNullOrWhiteSpace(qName))
            {
                //Validate that the QueueName property is set
                ComponentMetaData.FireError(0, ComponentMetaData.Name, "The QueueName property must be set", "", 0, out cancel);
                return DTSValidationStatus.VS_ISBROKEN;
            } 
            */
            return base.Validate();
        }

        public override void PreExecute()
        {
            
            try
               {
                sqlConn.Open();
                DbCommand cmd = sqlConn.CreateCommand();

                cmd.CommandText = ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString();
                cmd.CommandType = System.Data.CommandType.Text;
                          
                sqlReader = cmd.ExecuteReader();
                }
               catch (Exception e)
                {
                MessageBox.Show(e.ToString());
                ReleaseConnections();
                throw;
                } 
                        
            base.PreExecute();
           
        }

        private void AddOutputColumns(String propertyValue)
        {
           
            output.OutputColumnCollection.RemoveAll();
            output.ExternalMetadataColumnCollection.RemoveAll();

            sqlConn.Open();
            DbCommand cmd = sqlConn.CreateCommand();

            //    cmd.CommandText = "SELECT * FROM CountryCodes";
            cmd.CommandText = ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString();
            cmd.CommandType = System.Data.CommandType.Text;
           

            sqlReader = cmd.ExecuteReader();
         
            if (sqlReader != null)
            {

                DataTable dt = new DataTable();
                DataTable dtSchema = sqlReader.GetSchemaTable();
                this.ColumnsCount = dtSchema.Rows.Count;
                foreach (DataRow row in dtSchema.Rows)
                {
                    IDTSOutputColumn100 outputCol = ComponentMetaData.OutputCollection[0].OutputColumnCollection.New();
                   
                   // MessageBox.Show("1111111111111111111111");
                    bool isLong = false;
                    DataType dType = DataType.DT_WSTR;
                     dType = DataRecordTypeToBufferType(row["DataType"].GetType());
                    dType = ConvertBufferDataTypeToFitManaged(dType, ref isLong);
                    int length = 0;// ((int)row["ColumnSize"]) == -1 ? 1000 : (int)row["ColumnSize"];
                    int precision = 0; //row["NumericPrecision"] is System.DBNull ? 0 : (short)row["NumericPrecision"];
                    int scale = 0; // row["NumericScale"] is System.DBNull ? 0 : (short)row["NumericScale"];
                    int codePage = 0; // dtSchema.Locale.TextInfo.ANSICodePage;
                    
                    switch (dType)
                    {
                        case DataType.DT_STR:
                        case DataType.DT_TEXT:
                            precision = 0;
                            scale = 0;
                            length = 256;
                            break;
                        case DataType.DT_NUMERIC:
                            length = 0;
                            codePage = 0;
                            if (precision > 38)
                                precision = 38;
                            if (scale > precision)
                                scale = precision;
                            break;
                        case DataType.DT_DECIMAL:
                            length = 0;
                            precision = 0;
                            codePage = 0;
                            if (scale > 28)
                                scale = 28;
                            break;
                        case DataType.DT_WSTR:
                            precision = 0;
                            scale = 0;
                            codePage = 0;
                            length = 256;                         break;
                        default:
                            
                            precision = 0;
                            scale = 0;
                            
                            
                            break;
                    }
                 //   MessageBox.Show("2222222222222222222222222222"); 
                      outputCol.Name = row["ColumnName"].ToString();
                  //   outputCol.SetDataTypeProperties(DataType.DT_WSTR, 4000, 0, 0, 0);

                    outputCol.SetDataTypeProperties(dType, length, precision, scale, codePage);
                }

            } 
                   
        }
        public override IDTSCustomProperty100 SetComponentProperty(string propertyName, object propertyValue)
        {
            base.SetComponentProperty(propertyName, propertyValue);
            if (propertyName == "QueueName" &&
        ComponentMetaData.OutputCollection[0].OutputColumnCollection.Count == 0)
            {
                AddOutputColumns(propertyValue.ToString());
            }

             return base.SetComponentProperty(propertyName, propertyValue);
            
        }

        public override void SetOutputColumnDataTypeProperties(int outputID, int outputColumnID, Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType dataType, int length, int precision, int scale, int codePage)

        {

            IDTSOutputCollection100 outputColl = this.ComponentMetaData.OutputCollection;

            IDTSOutput100 output = outputColl.GetObjectByID(outputID);

            IDTSOutputColumnCollection100 columnColl = output.OutputColumnCollection;

            IDTSOutputColumn100 column = columnColl.GetObjectByID(outputColumnID);

            column.SetDataTypeProperties(dataType, length, precision, scale, codePage);

        }

        public override void PrimeOutput(int outputs, int[] outputIDs, PipelineBuffer[] buffers)
        {
            base.PrimeOutput(outputs, outputIDs, buffers);
            IDTSOutput100 output = ComponentMetaData.OutputCollection.FindObjectByID(outputIDs[0]);
            PipelineBuffer buffer = buffers[0];
            sqlConn.Open();
            DbCommand cmd = sqlConn.CreateCommand();

            //    cmd.CommandText = "SELECT * FROM CountryCodes";
            cmd.CommandText = ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString();
            cmd.CommandType = System.Data.CommandType.Text;
          //  MessageBox.Show("PrimeOutput" + ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString());
            DbDataReader sqlReader1;
            sqlReader1 = cmd.ExecuteReader();
//#if DEBUG
            //Debugger.Launch();
//#endif
            try
            {

                string sep = "";
                var dt = new DataTable();
                DataReaderParser dp = new DataReaderParser(sqlReader1);
                dt = dp.ConvertDataReaderToTableManually();

                foreach (DataRow row in dt.Rows)
                {
                    buffer.AddRow();

                    for (int x = 0; x < row.ItemArray.Length; x++)
                    {
                        if (row.IsNull(x))
                            buffer.SetNull(mapOutputColsToBufferCols[x]);
                        else
                            buffer[x] = (String.Format("{0}", row[x]));
                    }
                }

                buffer.SetEndOfRowset();
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred:\n" +ex.Message+"\n"+ex.StackTrace);
            }
            buffer.SetEndOfRowset();
        }

        public override void Cleanup()
        {
            
            base.Cleanup();
        }

    }
}