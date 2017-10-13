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
        public override void ProvideComponentProperties()
        {
            ComponentMetaData.RuntimeConnectionCollection.RemoveAll();
            RemoveAllInputsOutputsAndCustomProperties();
            ComponentMetaData.Name = "TARGITD3FO Connection Manager";
            ComponentMetaData.Description = "TARGITD3FO Connection Manager";
            IDTSRuntimeConnection100 rtc = ComponentMetaData.RuntimeConnectionCollection.New();
            rtc.Name = "TARGITD3FO Connection Manager";

            rtc.Description = "TARGITD3FO Connection Manager";
            IDTSOutput100 output = ComponentMetaData.OutputCollection.New();
            output.Name = "Component Output";
            output.Description = "Output";
            output.ExternalMetadataColumnCollection.IsUsed = true;
            IDTSCustomProperty100 queueName = ComponentMetaData.CustomPropertyCollection.New();
            queueName.Name = "QueueName";
            queueName.Description = "The name of the D3FO  queue to read from";
        }
        public override void AcquireConnections(object transaction)
        {
            String _filename;

            if (ComponentMetaData.RuntimeConnectionCollection["FilePipeline"].ConnectionManager != null)
            {
                ConnectionManager cm = Microsoft.SqlServer.Dts.Runtime.DtsConvert.GetWrapper(ComponentMetaData.RuntimeConnectionCollection["File to read"].ConnectionManager);
                if (cm.CreationName != "FILE")
                {
                    throw new Exception("Connection Manager is not File connection manager");
                }
                else
                {
                    Microsoft.SqlServer.Dts.Runtime.DTSFileConnectionUsageType _fil;
                    _fil = (Microsoft.SqlServer.Dts.Runtime.DTSFileConnectionUsageType)cm.Properties["FileUsageType"].GetValue(cm);
                    if (_fil != Microsoft.SqlServer.Dts.Runtime.DTSFileConnectionUsageType.FileExists)
                    {
                        ;
                        throw new Exception("File type must be existing file");
                    }
                    else
                    {
                        _filename = ComponentMetaData.RuntimeConnectionCollection["FilePipeline"].ConnectionManager.AcquireConnection(transaction).ToString();
                    }
                    if (_filename == null || _filename.Length == 0)
                    {
                        throw new Exception("Nothing returned when grabbing filename");
                    }
                }
            }
        }
    }
}