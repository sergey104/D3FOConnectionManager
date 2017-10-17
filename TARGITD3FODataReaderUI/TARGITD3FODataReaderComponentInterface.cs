using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Dts.Design;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Design;
using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Pipeline.Design;

namespace TARGITD3FOConnection
{
    public class TARGITD3FODataReaderComponentInterface : IDtsComponentUI
    {

        private IDTSComponentMetaData100 _dtsComponentMetadata;
        private IServiceProvider _serviceProvider;
        private IDtsConnectionService _connectionService;

        public TARGITD3FODataReaderComponentInterface()
        {

        }

        public void Initialize(IDTSComponentMetaData100 dtsComponentMetadata, IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._dtsComponentMetadata = dtsComponentMetadata;

            this._connectionService = (IDtsConnectionService)serviceProvider.GetService(typeof(IDtsConnectionService));
        }


        public ContainerControl GetView()
        {
            TARGITD3FODataReaderComponentEditor editor = new TARGITD3FODataReaderComponentEditor();
        //    editor.ConnectionManager = this._connectionManager;
        //    editor.ServiceProvider = this._serviceProvider;
            return editor;
        }

        public void Delete(IWin32Window parentWindow)
        {
        }


        public bool Edit(IWin32Window parentWindow, Microsoft.SqlServer.Dts.Runtime.Variables variables, Microsoft.SqlServer.Dts.Runtime.Connections connections)
        {
            //return ShowForm(parentWindow);
            return true;
        }

        public void New(IWin32Window parentWindow)
        {
            
        }

        public void Help(IWin32Window parentWindow)
        {
            throw new NotImplementedException();
        }
    }
}
