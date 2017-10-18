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

namespace TARGITD3FOConnection
{
    [DtsPipelineComponent(DisplayName = "TARGITD3FOComponent", ComponentType = ComponentType.SourceAdapter, IconResource = "TARGITD3FODataReader.Resources.Icon1.ico",
      UITypeName = "TARGITD3FOConnection.TARGITD3FODataReaderComponentInterface, TARGITD3FODataReaderComponentUI, Version = 1.0.0.0, Culture = neutral, PublicKeyToken =71aabddac4fee55e"
        )]

    public class TARGITD3FODataReaderComponent : PipelineComponent
    {
        private DbDataReader sqlReader;
        public D3FOConnectionManager cm;
        public IDTSCustomProperty100 queueName;
        public IDTSRuntimeConnection100 connection;
        public IDTSOutput100 output;
        public DbConnection sqlConn;

        public TARGITD3FODataReaderComponent()
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

            queueName = ComponentMetaData.CustomPropertyCollection.New();
            queueName.Name = "QueueName";
            queueName.Description = "The name of the queue to read messages from";

            

            connection = ComponentMetaData.RuntimeConnectionCollection.New();
            connection.Name = "sdatasource";
            connection.ConnectionManagerID = "sdatasource";

            


            CreateColumns();
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

          //      this.queueName = ComponentMetaData.CustomPropertyCollection["QueueName"].Value;
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
                MessageBox.Show("DBBBBBBB111111111111111111!");



                //  DbConnection sqlConn = (DbConnection)connection;
                sqlConn.Open();
                DbCommand cmd = sqlConn.CreateCommand();
                MessageBox.Show("DBBBBBBB!");
                cmd.CommandText = "SELECT * FROM CountryCodes";
                cmd.CommandType = System.Data.CommandType.Text;
                MessageBox.Show("DBBBBBBB2222!");
                sqlReader = cmd.ExecuteReader();
            }
                catch (Exception)
                {
                    ReleaseConnections();
                    throw;
                } 
            

            
            base.PreExecute();
        }

        private void CreateColumns()
        {
             

             output.OutputColumnCollection.RemoveAll();
             output.ExternalMetadataColumnCollection.RemoveAll();

             IDTSOutputColumn100 column1 = output.OutputColumnCollection.New();
             IDTSExternalMetadataColumn100 exColumn1 = output.ExternalMetadataColumnCollection.New();
             IDTSOutputColumn100 column2 = output.OutputColumnCollection.New();
             IDTSExternalMetadataColumn100 exColumn2 = output.ExternalMetadataColumnCollection.New();



             column1.Name = "Country";
             column1.SetDataTypeProperties(DataType.DT_WSTR, 4000, 0, 0, 0);
             exColumn1.Name = "Country";

            column2.Name = "Code";
            column2.SetDataTypeProperties(DataType.DT_WSTR, 4000, 0, 0, 0);
            exColumn2.Name = "Code";

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
            try
            {

                string sep = "";

                while (sqlReader.Read())
                {

                    string s = (String.Format("{0}",sqlReader[0]));
                    MessageBox.Show(s);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred:\n{ex.Message}\n{ex.StackTrace}");
            }
            buffer.SetEndOfRowset();
        }

    }
}