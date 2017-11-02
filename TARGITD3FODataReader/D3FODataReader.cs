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
        
        private string queueName;

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

            IDTSCustomProperty100 queueName = ComponentMetaData.CustomPropertyCollection.New();
            queueName.Name = "QueueName";
            queueName.Description = "The name of the queue to read messages from";
            queueName.Value = String.Empty;
            

            connection = ComponentMetaData.RuntimeConnectionCollection.New();
            connection.Name = "TARGITD3FOConnection";
            connection.ConnectionManagerID = "TARGITDataSource";


            MessageBox.Show("prop");

            //CreateColumns();
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

                //    cmd.CommandText = "SELECT * FROM CountryCodes";
                cmd.CommandText = ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString();
                    cmd.CommandType = System.Data.CommandType.Text;
                MessageBox.Show("PreExecute" + ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString());
               
                sqlReader = cmd.ExecuteReader();
           }
               catch (Exception)
                {
                    ReleaseConnections();
                    throw;
                } 
            

            
            base.PreExecute();
            
        }

        private void AddOutputColumns(String propertyValue)
        {
            MessageBox.Show("CreateColumns");

            output.OutputColumnCollection.RemoveAll();
            output.ExternalMetadataColumnCollection.RemoveAll();

            /*
                          IDTSOutputColumn100 column1 = output.OutputColumnCollection.New();
                          IDTSExternalMetadataColumn100 exColumn1 = output.ExternalMetadataColumnCollection.New();
                          IDTSOutputColumn100 column2 = output.OutputColumnCollection.New();
                          IDTSExternalMetadataColumn100 exColumn2 = output.ExternalMetadataColumnCollection.New();

                          column1.Name = "Country";
                          column1.SetDataTypeProperties(DataType.DT_WSTR, 4000, 0, 0, 0);
                          exColumn1.Name = "Country";
                          column2.Name = "Code";
                          column2.SetDataTypeProperties(DataType.DT_WSTR, 4000, 0, 0, 0);
             exColumn1.Name = "Code"; */
            sqlConn.Open();
            DbCommand cmd = sqlConn.CreateCommand();

            //    cmd.CommandText = "SELECT * FROM CountryCodes";
            cmd.CommandText = ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString();
            cmd.CommandType = System.Data.CommandType.Text;
            MessageBox.Show("Execute in Col" + ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString());

            sqlReader = cmd.ExecuteReader();
            MessageBox.Show("Executed in Col Done" );
            if (sqlReader != null)
            {
#if DEBUG
                Debugger.Launch();
#endif

                DataTable dt = new DataTable();
                DataTable dtSchema = sqlReader.GetSchemaTable();
                foreach (DataRow row in dtSchema.Rows)
                {
                    IDTSOutputColumn100 outputCol = ComponentMetaData.OutputCollection[0].OutputColumnCollection.New();
                    MessageBox.Show("Parsing");
                    bool isLong = false;
        /*            DataType dType = DataRecordTypeToBufferType((Type)row["DataType"]);
                    dType = ConvertBufferDataTypeToFitManaged(dType, ref isLong);
                    int length = ((int)row["ColumnSize"]) == -1 ? 1000 : (int)row["ColumnSize"];
                    int precision = row["NumericPrecision"] is System.DBNull ? 0 : (short)row["NumericPrecision"];
                    int scale = row["NumericScale"] is System.DBNull ? 0 : (short)row["NumericScale"];
                    int codePage = dtSchema.Locale.TextInfo.ANSICodePage;

                    switch (dType)
                    {
                        case DataType.DT_STR:
                        case DataType.DT_TEXT:
                            precision = 0;
                            scale = 0;
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
                            break;
                        default:
                            length = 0;
                            precision = 0;
                            scale = 0;
                            codePage = 0;
                            break;
                    }
*/
                    outputCol.Name = row["ColumnName"].ToString();
                    outputCol.SetDataTypeProperties(DataType.DT_WSTR, 4000, 0, 0, 0);
                    MessageBox.Show("Parsing"+ outputCol.Name);
                //    outputCol.SetDataTypeProperties(dType, length, precision, scale, codePage);
                }

            } 
                ///////////////////////////////////////

            //////////////////////////////
           
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
            PipelineBuffer buffer = buffers[0];
            sqlConn.Open();
            DbCommand cmd = sqlConn.CreateCommand();

            //    cmd.CommandText = "SELECT * FROM CountryCodes";
            cmd.CommandText = ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString();
            cmd.CommandType = System.Data.CommandType.Text;
            MessageBox.Show("PrimeOutput" + ComponentMetaData.CustomPropertyCollection["QueueName"].Value.ToString());
            DbDataReader sqlReader1;
            sqlReader1 = cmd.ExecuteReader();
            try
            {

                string sep = "";

                while (sqlReader1.Read())
                {

                    //            string s0 = (String.Format("{0}",sqlReader[0]));
                    for (int i = 0; i < sqlReader1.Depth; i++)
                    {
                        string s1 = (String.Format("{0}", sqlReader1[i]));

                        //    MessageBox.Show(s);
                        buffer.AddRow();
                        buffer[i] = s1;
                      //  buffer[1] = s1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred:\n{ex.Message}\n{ex.StackTrace}");
            }
            buffer.SetEndOfRowset();
        }

        public override void Cleanup()
        {
            
            base.Cleanup();
        }

    }
}