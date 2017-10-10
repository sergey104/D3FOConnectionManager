using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Dts.Design;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Design;


namespace TARGITD3FOConnection
{
    public class D3FOConnectionManagerInterface : IDtsConnectionManagerUI
    {
        private ConnectionManager _connectionManager;
        private IServiceProvider _serviceProvider;

        public D3FOConnectionManagerInterface()
        {

        }

        public void Initialize(ConnectionManager connectionManager, IServiceProvider serviceProvider)
        {
            this._connectionManager = connectionManager;
            this._serviceProvider = serviceProvider;
        }


        public ContainerControl GetView()
        {
            D3FOConnectionManagerEditor editor = new D3FOConnectionManagerEditor();
            editor.ConnectionManager = this._connectionManager;
            editor.ServiceProvider = this._serviceProvider;
            return editor;
        }

        public void Delete(IWin32Window parentWindow)
        {
        }

        public void New(IWin32Window parentWindow)
       {

        }

        public bool Edit(System.Windows.Forms.IWin32Window parentWindow, Microsoft.SqlServer.Dts.Runtime.Connections connections, Microsoft.SqlServer.Dts.Runtime.Design.ConnectionManagerUIArgs connectionUIArg)
        {
            D3FOConnectionManagerEditor editor = new D3FOConnectionManagerEditor();

            editor.Initialize(_connectionManager, this._serviceProvider);
            if (editor.ShowDialog(parentWindow) == DialogResult.OK)
            {
                editor.Dispose();
                return true;
            }
            else
            {
                editor.Dispose();
                return false;
            }
        }

        public bool New(System.Windows.Forms.IWin32Window parentWindow, Microsoft.SqlServer.Dts.Runtime.Connections connections, Microsoft.SqlServer.Dts.Runtime.Design.ConnectionManagerUIArgs connectionUIArg)
        {
            D3FOConnectionManagerEditor editor = new D3FOConnectionManagerEditor();

            editor.Initialize(_connectionManager, this._serviceProvider);
            if (editor.ShowDialog(parentWindow) == DialogResult.OK)
            {
                editor.Dispose();
                return true;
            }
            else
            {
                editor.Dispose();
                return false;
            }
        }
    }
}
