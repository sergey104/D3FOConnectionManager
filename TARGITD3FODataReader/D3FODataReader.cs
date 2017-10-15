using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
using System.ServiceModel;
using System.Data;
using System.Xml;
using System.IO;
using System.Data.SqlClient;

namespace TARGITD3FOConnection
{
    [DtsPipelineComponent(DisplayName = "TARGITD3FOComponent", ComponentType = ComponentType.SourceAdapter, IconResource = "TARGITD3FODataReader.Resources.Icon1.ico")]
    public class TARGITD3FODataReaderComponent : PipelineComponent
    {
        private SqlDataReader sqlReader;
       
        public override void ProvideComponentProperties()
        {
            ComponentMetaData.RuntimeConnectionCollection.RemoveAll();
            RemoveAllInputsOutputsAndCustomProperties();
            ComponentMetaData.Name = "TARGITD3FODataReaderComponent";
            ComponentMetaData.Description = "TARGITD3FODataReaderComponent";
            IDTSRuntimeConnection100 rtc = ComponentMetaData.RuntimeConnectionCollection.New();
            rtc.Name = "TARGITD3FOConnection";
            rtc.Description = "TARGITD3FOConnection";
            IDTSOutput100 output = ComponentMetaData.OutputCollection.New();
            output.Name = "Component Output";
            output.Description = "Output";
            output.ExternalMetadataColumnCollection.IsUsed = true;
            IDTSCustomProperty100 queueName = ComponentMetaData.CustomPropertyCollection.New();
            queueName.Name = "QueueName";
            queueName.Description = "The name of the D3FO  queue to read from";
            CreateColumns();
        }
        public override void AcquireConnections(object transaction)
        {

            if (ComponentMetaData.RuntimeConnectionCollection[0].ConnectionManager != null)
            {
                String _filename;
                    ConnectionManager cm = Microsoft.SqlServer.Dts.Runtime.DtsConvert.GetWrapper(ComponentMetaData.RuntimeConnectionCollection["TARGITD3FOConnection"].ConnectionManager);

                   D3FOConnectionManager connectionManager = cm.InnerObject as D3FOConnectionManager;


                    if (connectionManager == null)   throw new Exception("Couldn't get D3FO connection manager, ");

                    IDTSCustomProperty100 queueName = ComponentMetaData.CustomPropertyCollection["QueueName"];
                    //  _filename = ComponentMetaData.RuntimeConnectionCollection["FilePipeline"].ConnectionManager.AcquireConnection(transaction).ToString();
                    _filename = connectionManager.AcquireConnection(transaction).ToString();
                    if (_filename == null || _filename.Length == 0)
                    {
                        throw new Exception("Nothing returned when openning connection");
                    }  
            }



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
            } */

            return base.Validate();
        }

        public override void PreExecute()
        {
            try
            {
                SqlConnection sqlConn = (SqlConnection)Microsoft.SqlServer.Dts.Runtime.DtsConvert.GetWrapper(ComponentMetaData.RuntimeConnectionCollection["TARGITD3FOConnection"].ConnectionManager).AcquireConnection(null);

                SqlCommand cmd = new SqlCommand("SELECT * FROM system.tables", sqlConn);
                sqlReader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                ReleaseConnections();
                throw;
            }
        }

        private void CreateColumns()
        {
            IDTSOutput100 output = ComponentMetaData.OutputCollection["Component Output"];

            output.OutputColumnCollection.RemoveAll();
            output.ExternalMetadataColumnCollection.RemoveAll();

            IDTSOutputColumn100 column1 = output.OutputColumnCollection.New();
            IDTSExternalMetadataColumn100 exColumn1 = output.ExternalMetadataColumnCollection.New();

            

            column1.Name = "Table";
            column1.SetDataTypeProperties(DataType.DT_WSTR, 4000, 0, 0, 0);

            
        }

    }
}