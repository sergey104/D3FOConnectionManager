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


namespace TARGITD3FOConnection
{
    [DtsPipelineComponent(DisplayName = "TARGITD3FOComponent", ComponentType = ComponentType.SourceAdapter, IconResource = "TARGITD3FODataReader.Resources.Icon1.ico")]
    public class TARGITD3FODataReaderComponent : PipelineComponent
    {
        private StreamReader textReader;
        private string exportedAddressFile;
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
            /*    public override DTSValidationStatus Validate()
                {
                    bool pbCancel = false;
                    IDTSOutput100 output = ComponentMetaData.OutputCollection["Component Output"];
                    if (ComponentMetaData.InputCollection.Count != 0)
                    {
                        ComponentMetaData.FireError(0, ComponentMetaData.Name, "Unexpected input found", "", 0, out pbCancel);
                        return DTSValidationStatus.VS_ISCORRUPT;

                    }
                    if (ComponentMetaData.RuntimeConnectionCollection["FilePipeline"].ConnectionManager == null)
                    {
                        ComponentMetaData.FireError(0, "Validate", "No connection manager specified", "", 0, out pbCancel);
                        return DTSValidationStatus.VS_ISBROKEN;

                    }
                    //I Check for Output Columns, if not then force ReinitializeMetaData 
                    if (ComponentMetaData.OutputCollection["Component Output"].OutputColumnCollection.Count == 0)

                    {

                        ComponentMetaData.FireError(0, "Validate", "No output columns specified.Making call to ReinitializeMetaData.", "", 0, out pbCancel);
                        return DTSValidationStatus.VS_NEEDSNEWMETADATA;

                    }
                    //What about if we have output columns but we have no ExternalMetaData II columns? Maybe somebody removed them through code, 
                    if (DoesEachOutputColumnHaveAMetaDataColumnAndDoDatatypesMatch(output.ID) == false)
                    {

                        ComponentMetaData.FireError(0, "Validate", "Output columns and metadata columns are out of sync.Making call to ReinitializeMetaData.", "", 0, out pbCancel);

                        return DTSValidationStatus.VS_NEEDSNEWMETADATA;

                    }

                    return base.Validate();
                }

                private bool DoesEachOutputColumnHaveAMetaDataColumnAndDoDatatypesMatch(int outputID)
                {

                    IDTSOutput100 output = ComponentMetaData.OutputCollection.GetObjectByID(outputID);
                    IDTSExternalMetadataColumn100 mdc;
                    bool rtnVal = true;

                    foreach (IDTSOutputColumn100 col in output.OutputColumnCollection)
                    {

                        if (col.ExternalMetadataColumnID == 0)
                        {
                            rtnVal = false;
                        }
                        else
                        {
                            mdc = output.ExternalMetadataColumnCollection.GetObjectByID(col.ExternalMetadataColumnID);

                            if (mdc.DataType != col.DataType || mdc.Length != col.Length || mdc.Precision != col.Precision || mdc.Scale != col.Scale || mdc.CodePage != col.CodePage)

                            {

                                rtnVal = false;

                            }

                        }
                    }
                    return rtnVal;



                }
                public override void ReinitializeMetaData()
                {

                    IDTSOutput100 profoutput =

                    ComponentMetaData.OutputCollection["Component Output"];

                    if (profoutput.ExternalMetadataColumnCollection.Count > 0)
                    {

                        profoutput.ExternalMetadataColumnCollection.RemoveAll();
                    }

                    if (profoutput.OutputColumnCollection.Count > 0)
                    {

                        profoutput.OutputColumnCollection.RemoveAll();

                    }

                    CreateOutputAndMetaDataColumns(profoutput);

                }
                public void CreateOutputAndMetaDataColumns(IDTSOutput100 profoutput)
                {
                    IDTSOutputColumn100 outName = profoutput.OutputColumnCollection.New();
                    outName.Name = "Name";
                    outName.Description = "The Name value retieved from File";
                    outName.SetDataTypeProperties(DataType.DT_STR, 50, 0, 0, 1252);
                    //    CreateExternalMetaDataColumn(profoutput.ExternalMetadataColumnCollection, outName);
                    IDTSOutputColumn100 outTxt = profoutput.OutputColumnCollection.New();
                    outTxt.Name = "Line";
                    outTxt.Description = "The Age value retieved from File";
                    outTxt.SetDataTypeProperties(DataType.DT_I4, 0, 0, 0, 0);
                    //Create an external metadata column to go alongside with it
                    //   CreateExternalMetaDataColumn(profoutput.ExternalMetadataColumnCollection, outTxt);


                }

                public override void PrimeOutput(int outputs, int[] outputIDs, PipelineBuffer[] buffers)
                {
                    // ParseTheFileAndAddToBuffer(_filename, buffers[0]);
                    string nextLine;
                    string[] columns;
                    PipelineBuffer MyAddressOutputBuffer = buffers[0];
                    char[] delimiters;
                    delimiters = ",".ToCharArray();

                    nextLine = textReader.ReadLine();
                    while (nextLine != null)
                    {
                        columns = nextLine.Split(delimiters);
                        {
                            MyAddressOutputBuffer.AddRow();

                        }
                        nextLine = textReader.ReadLine();
                    }
                    buffers[0].SetEndOfRowset();
                }

            */

        }
}