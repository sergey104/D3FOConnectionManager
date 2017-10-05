using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3FOConnectionManager
{
    // C# code
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.SqlServer.Dts.Design;
    using Microsoft.SqlServer.Dts.Runtime;
    using Microsoft.SqlServer.Dts.Runtime.Design;


    namespace SSISJoost
    {
        public partial class D3FOConnectionManagerEditor : Form
        {
            #region General Connection Manager Methods
            // Setting and getting ConnectionManager
            private ConnectionManager _connectionManager;
            public ConnectionManager ConnectionManager
            {
                get { return _connectionManager; }
                set { _connectionManager = value; }
            }

            // Setting and getting ServiceProvider
            private IServiceProvider _serviceProvider = null;
            public IServiceProvider ServiceProvider
            {
                get { return _serviceProvider; }
                set { _serviceProvider = value; }
            }

            // Default constructor
            public D3FOConnectionManagerEditor()
            {
               // InitializeComponent();
            }

            public void Initialize(ConnectionManager connectionManager, IServiceProvider serviceProvider)
            {
                this._connectionManager = connectionManager;
                this._serviceProvider = serviceProvider;
            }
            #endregion

            #region Page Load
            // Fill the fields of the form. Get data from connectionManager object
            private void SMTP2Editor_Load(object sender, EventArgs e)
            {
              //  this.txtName.Text = this._connectionManager.Name;
             //   this.txtDescription.Text = this._connectionManager.Description;
              //  this.txtURL.Text = this._connectionManager.Properties["URL"].GetValue(_connectionManager).ToString();
              //  this.txtUserName.Text = this._connectionManager.Properties["UserName"].GetValue(_connectionManager).ToString();
              //  this.txtPassword.Text = this._connectionManager.Properties["Password"].GetValue(_connectionManager).ToString();
            }
            #endregion

            #region Buttons
            // Save value from fields in connectionManager object
            private void btnOK_Click(object sender, EventArgs e)
            {
              //  this._connectionManager.Name = this.txtName.Text;
              //  this._connectionManager.Description = this.txtDescription.Text;
              //  this._connectionManager.Properties["URL"].SetValue(this._connectionManager, this.txtURL.Text);
              //  this._connectionManager.Properties["UserName"].SetValue(this._connectionManager, this.txtUserName.Text);
              //  this._connectionManager.Properties["Password"].SetValue(this._connectionManager, this.txtPassword.Text);
              //  this.DialogResult = DialogResult.OK;
            }

            // Cancel diolog
            private void btnCancel_Click(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.Cancel;
            }

            // Show some helpful information
            private void btnHelp_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Help");
            }
            #endregion
        }
    }
}
