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
        public override void AcquireConnections(object transaction)
        {
            base.AcquireConnections(transaction);
        }
        public override void PrimeOutput(int outputs, int[] outputIDs, PipelineBuffer[] buffers)
        {
            base.PrimeOutput(outputs, outputIDs, buffers);
        }
        public override void PreExecute()
        {
            base.PreExecute();
        }
        public override DTSValidationStatus Validate()
        {
            return base.Validate();
        }
        public override IDTSCustomProperty100 SetComponentProperty(string propertyName, object propertyValue)
        {
            return base.SetComponentProperty(propertyName, propertyValue);
        }
        public override void ProvideComponentProperties()
        {
            base.ProvideComponentProperties();
        }

    }
}