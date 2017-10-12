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



namespace TARGITD3FOConnection
{
    [DtsPipelineComponent(DisplayName = "TARGITD3FODataReader", ComponentType = ComponentType.SourceAdapter)]
    public class TARGITD3FODataReader : PipelineComponent
    {
       D3FOConnectionManager D3FOconnectionManager;
        IDTSOutput100 output;
        IDTSCustomProperty100 queueName;
        IDTSRuntimeConnection100 connection;

        public override void ProvideComponentProperties()
        {
            base.RemoveAllInputsOutputsAndCustomProperties();
            ComponentMetaData.RuntimeConnectionCollection.RemoveAll();

            output = ComponentMetaData.OutputCollection.New();
            output.Name = "Output";

            queueName = ComponentMetaData.CustomPropertyCollection.New();
            queueName.Name = "QueueName";
            queueName.Description = "The name of the RabbitMQ queue to read messages from";

            connection = ComponentMetaData.RuntimeConnectionCollection.New();
            connection.Name = "TARGITD3FOConnection";
            connection.ConnectionManagerID = "TARGITD3FOConnection";

            CreateColumns();
        }

        private void CreateColumns()
        {
            IDTSOutput100 output = ComponentMetaData.OutputCollection[0];

            output.OutputColumnCollection.RemoveAll();
            output.ExternalMetadataColumnCollection.RemoveAll();

            IDTSOutputColumn100 column1 = output.OutputColumnCollection.New();
            IDTSExternalMetadataColumn100 exColumn1 = output.ExternalMetadataColumnCollection.New();

            IDTSOutputColumn100 column2 = output.OutputColumnCollection.New();
            IDTSExternalMetadataColumn100 exColumn2 = output.ExternalMetadataColumnCollection.New();

            column1.Name = "MessageContents";
            column1.SetDataTypeProperties(DataType.DT_WSTR, 4000, 0, 0, 0);

            column2.Name = "RoutingKey";
            column2.SetDataTypeProperties(DataType.DT_WSTR, 100, 0, 0, 0);
        }

        public override void AcquireConnections(object transaction)
        {
            /*  if (ComponentMetaData.RuntimeConnectionCollection[0].ConnectionManager != null)
              {
                  ConnectionManager connectionManager = Microsoft.SqlServer.Dts.Runtime.DtsConvert.GetWrapper(
                    ComponentMetaData.RuntimeConnectionCollection[0].ConnectionManager);

                  this.D3FOconnectionManager = connectionManager.InnerObject as TARGITD3FOConnection.D3FOConnectionManager;

                  if (this.D3FOconnectionManager == null)
                      throw new Exception("Couldn't get the D3FO connection manager, ");

                  this.queueName = ComponentMetaData.CustomPropertyCollection["QueueName"].Value;
                 D3FOconnectionManager = this.D3FOconnectionManager.AcquireConnection(transaction) as IConnection;
              } */
            base.AcquireConnections(transaction);
        }

        public override void ReleaseConnections()
        {
            if (D3FOconnectionManager != null)
            {
                this.D3FOconnectionManager.ReleaseConnection(D3FOconnectionManager);
            }
        }
        public override void PrimeOutput(int outputs, int[] outputIDs, PipelineBuffer[] buffers)
        {
            IDTSOutput100 output = ComponentMetaData.OutputCollection[0];
            PipelineBuffer buffer = buffers[0];

            object message;
            bool success;

            while (queueConsumer.IsRunning)
            {
                try
                {
                    success = queueConsumer.Queue.Dequeue(100, out message);
                }
                catch (Exception)
                {
                    break;
                }

                if (success)
                {
                    BasicDeliverEventArgs e = (BasicDeliverEventArgs)message;

                    var messageContent = System.Text.Encoding.UTF8.GetString(e.Body);

                    buffer.AddRow();
                    buffer[0] = messageContent;
                    buffer[1] = e.RoutingKey;
                }
                else
                {
                    break;
                }
            }

            buffer.SetEndOfRowset();
        }
        public override void PreExecute()
        {
            base.PreExecute();
        }
        public override DTSValidationStatus Validate()
        {
            bool cancel;
            string qName = ComponentMetaData.CustomPropertyCollection["QueueName"].Value;

            if (string.IsNullOrWhiteSpace(qName))
            {
                //Validate that the QueueName property is set
                ComponentMetaData.FireError(0, ComponentMetaData.Name, "The QueueName property must be set", "", 0, out cancel);
                return DTSValidationStatus.VS_ISBROKEN;
            }

            return base.Validate();
        }
        public override IDTSCustomProperty100 SetComponentProperty(string propertyName, object propertyValue)
        {
            return base.SetComponentProperty(propertyName, propertyValue);
        }
       

    }
}