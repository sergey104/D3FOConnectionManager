using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Windows.Forms;
using Microsoft.SqlServer.Dts.Design;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Design;

namespace TARGITD3FOConnection
{
    

     public partial class D3FOConnectionManagerEditor : Form
        {
            #region General Connection Manager Methods
            // Setting and getting ConnectionManager
        private ConnectionManager _connectionManager;
        private TextBox txtName;
        private TextBox txtAssembly;
        private TextBox txtConnectionMethod;
        private TextBox txtCompany;
        private TextBox txtAOS_Uri;
        private TextBox txtAD_Resource;
        private TextBox txtAD_Tenant;
        private TextBox txtAD_Client_App_ID;
        private TextBox txtAD_Client_App_Secret;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Button btnTest;
        private Button btnOK;
        private Button btnCancel;
        private RadioButton radioLocal;
        private RadioButton radioServer;
        private GroupBox groupBox1;

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
                InitializeComponent();
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

            
            this.txtName.Text = this._connectionManager.Properties["Name"].GetValue(_connectionManager).ToString();
            this.txtAssembly.Text = this._connectionManager.Properties["Assemblyp"].GetValue(_connectionManager).ToString();
            this.txtConnectionMethod.Text = this._connectionManager.Properties["Connection_Method"].GetValue(_connectionManager).ToString();
            this.txtCompany.Text = this._connectionManager.Properties["Company"].GetValue(_connectionManager).ToString();
            this.txtAOS_Uri.Text = this._connectionManager.Properties["AOS_Uri"].GetValue(_connectionManager).ToString();
            this.txtAD_Resource.Text = this._connectionManager.Properties["AD_Resource"].GetValue(_connectionManager).ToString();
            this.txtAD_Tenant.Text = this._connectionManager.Properties["AD_Tenant"].GetValue(_connectionManager).ToString();
            this.txtAD_Client_App_ID.Text = this._connectionManager.Properties["AD_Client_App_ID"].GetValue(_connectionManager).ToString();
            this.txtAD_Client_App_Secret.Text = this._connectionManager.Properties["AD_Client_App_Secret"].GetValue(_connectionManager).ToString();
            if (this._connectionManager.Properties["Assembly_location"].GetValue(_connectionManager).ToString() == "Server") this.radioServer.Checked = true;
               else this.radioServer.Checked = false;


        }
            #endregion

            #region Buttons
            // Save value from fields in connectionManager object
            private void btnOK_Click(object sender, EventArgs e)
            {
            //  this._connectionManager.Name = this.txtName.Text;
            this._connectionManager.Properties["Name"].SetValue(this._connectionManager, this.txtName.Text);
            this._connectionManager.Properties["Assemblyp"].SetValue(this._connectionManager, this.txtAssembly.Text);
            this._connectionManager.Properties["Connection_Method"].SetValue(this._connectionManager, this.txtConnectionMethod.Text);
            this._connectionManager.Properties["Company"].SetValue(this._connectionManager, this.txtCompany.Text);
            this._connectionManager.Properties["AOS_Uri"].SetValue(this._connectionManager, this.txtAOS_Uri.Text);
            this._connectionManager.Properties["AD_Resource"].SetValue(this._connectionManager, this.txtAD_Resource.Text);
            this._connectionManager.Properties["AD_Tenant"].SetValue(this._connectionManager, this.txtAD_Tenant.Text);
            this._connectionManager.Properties["AD_Client_App_ID"].SetValue(this._connectionManager, this.txtAD_Client_App_ID.Text);
            this._connectionManager.Properties["AD_Client_App_Secret"].SetValue(this._connectionManager, this.txtAD_Client_App_Secret.Text);
            if(this.radioServer.Checked == true) this._connectionManager.Properties["Assembly_location"].SetValue(this._connectionManager, "Server");
            else this._connectionManager.Properties["Assembly_location"].SetValue(this._connectionManager, "Local");
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

            // Cancel diolog
            private void btnCancel_Click(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

            // Show some helpful information
            private void btnHelp_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Help");
            }
        #endregion

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtAssembly = new System.Windows.Forms.TextBox();
            this.txtConnectionMethod = new System.Windows.Forms.TextBox();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.txtAOS_Uri = new System.Windows.Forms.TextBox();
            this.txtAD_Resource = new System.Windows.Forms.TextBox();
            this.txtAD_Tenant = new System.Windows.Forms.TextBox();
            this.txtAD_Client_App_ID = new System.Windows.Forms.TextBox();
            this.txtAD_Client_App_Secret = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.radioLocal = new System.Windows.Forms.RadioButton();
            this.radioServer = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(148, 33);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(484, 20);
            this.txtName.TabIndex = 0;
            // 
            // txtAssembly
            // 
            this.txtAssembly.Location = new System.Drawing.Point(148, 59);
            this.txtAssembly.Name = "txtAssembly";
            this.txtAssembly.Size = new System.Drawing.Size(484, 20);
            this.txtAssembly.TabIndex = 3;
            // 
            // txtConnectionMethod
            // 
            this.txtConnectionMethod.Location = new System.Drawing.Point(148, 138);
            this.txtConnectionMethod.Name = "txtConnectionMethod";
            this.txtConnectionMethod.Size = new System.Drawing.Size(484, 20);
            this.txtConnectionMethod.TabIndex = 4;
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(148, 164);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(484, 20);
            this.txtCompany.TabIndex = 5;
            // 
            // txtAOS_Uri
            // 
            this.txtAOS_Uri.Location = new System.Drawing.Point(148, 190);
            this.txtAOS_Uri.Name = "txtAOS_Uri";
            this.txtAOS_Uri.Size = new System.Drawing.Size(484, 20);
            this.txtAOS_Uri.TabIndex = 6;
            // 
            // txtAD_Resource
            // 
            this.txtAD_Resource.Location = new System.Drawing.Point(148, 217);
            this.txtAD_Resource.Name = "txtAD_Resource";
            this.txtAD_Resource.Size = new System.Drawing.Size(484, 20);
            this.txtAD_Resource.TabIndex = 7;
            // 
            // txtAD_Tenant
            // 
            this.txtAD_Tenant.Location = new System.Drawing.Point(148, 244);
            this.txtAD_Tenant.Name = "txtAD_Tenant";
            this.txtAD_Tenant.Size = new System.Drawing.Size(484, 20);
            this.txtAD_Tenant.TabIndex = 8;
            // 
            // txtAD_Client_App_ID
            // 
            this.txtAD_Client_App_ID.Location = new System.Drawing.Point(148, 271);
            this.txtAD_Client_App_ID.Name = "txtAD_Client_App_ID";
            this.txtAD_Client_App_ID.Size = new System.Drawing.Size(484, 20);
            this.txtAD_Client_App_ID.TabIndex = 9;
            // 
            // txtAD_Client_App_Secret
            // 
            this.txtAD_Client_App_Secret.Location = new System.Drawing.Point(148, 298);
            this.txtAD_Client_App_Secret.Name = "txtAD_Client_App_Secret";
            this.txtAD_Client_App_Secret.Size = new System.Drawing.Size(484, 20);
            this.txtAD_Client_App_Secret.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Assembly";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Assembly location";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Connection Method";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Company";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "AOS_Uri";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "AD_Resource";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 251);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "AD_Tenant";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 278);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "AD Client App ID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 304);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "AD Client App Secret";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(27, 361);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(164, 23);
            this.btnTest.TabIndex = 21;
            this.btnTest.Text = "Test Connection";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(439, 361);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(557, 361);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // radioLocal
            // 
            this.radioLocal.AutoSize = true;
            this.radioLocal.Location = new System.Drawing.Point(6, 10);
            this.radioLocal.Name = "radioLocal";
            this.radioLocal.Size = new System.Drawing.Size(51, 17);
            this.radioLocal.TabIndex = 24;
            this.radioLocal.TabStop = true;
            this.radioLocal.Text = "Local";
            this.radioLocal.UseVisualStyleBackColor = true;
            this.radioLocal.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioServer
            // 
            this.radioServer.AutoSize = true;
            this.radioServer.Location = new System.Drawing.Point(89, 10);
            this.radioServer.Name = "radioServer";
            this.radioServer.Size = new System.Drawing.Size(56, 17);
            this.radioServer.TabIndex = 25;
            this.radioServer.TabStop = true;
            this.radioServer.Text = "Server";
            this.radioServer.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioServer);
            this.groupBox1.Controls.Add(this.radioLocal);
            this.groupBox1.Location = new System.Drawing.Point(148, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 33);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            // 
            // D3FOConnectionManagerEditor
            // 
            this.ClientSize = new System.Drawing.Size(655, 420);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAD_Client_App_Secret);
            this.Controls.Add(this.txtAD_Client_App_ID);
            this.Controls.Add(this.txtAD_Tenant);
            this.Controls.Add(this.txtAD_Resource);
            this.Controls.Add(this.txtAOS_Uri);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.txtConnectionMethod);
            this.Controls.Add(this.txtAssembly);
            this.Controls.Add(this.txtName);
            this.Name = "D3FOConnectionManagerEditor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Text = "D3FOConnectionManager Editor";
            this.Load += new EventHandler(this.SMTP2Editor_Load);
           
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
    }
