using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Runtime.Design;
using System;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.Common;
using TARGITD3FODataReader.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace TARGITD3FOConnection
{
    public partial class TARGITD3FODataReaderEditor: Form
    {
       private ListBox listBox1;
        private TabControl tabControl1;
        private TabPage tabConnectionManagerPage;
        private Button button1;
        private ComboBox comboConnection;
        private Label label2;
        private Label label1;
        private TabPage tabColumnsPage;
        private ComboBox comboMode;
        private Label label3;
        private TabControl tabAccessMode;
        private TabPage tabTables;
        private Label label4;
        private TabPage tabSQL;
        private ComboBox comboTablesList;
        private Button buttonPreview;
        private RichTextBox richTextBox1;
        private Label label5;
        private Button button5;
        private Button button4;
        private Button button2;
        private Panel panel2;
        private Panel panel1;
        private Panel panel3;
        private Button buttonOK;
        private Button buttonCancel;
        private Button buttonHelp;
        private TabPage tabErrorOutputPage;

        private Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100 metaData;
        private IServiceProvider serviceProvider;
        private IDtsConnectionService connectionService;
        private CManagedComponentWrapper designTimeInstance;

        private DbDataReader sqlReader;
        public D3FOConnectionManager cm;
        public IDTSCustomProperty100 queueName;
        public IDTSRuntimeConnection100 connection;
        public IDTSOutput100 output;
        public DbConnection sqlConn;
        private ConnectionManagerItem currentManager = new ConnectionManagerItem();
        private Label label6;

        public string SQLString { get; set; }

        private class ConnectionManagerItem
        {
            public string ID;
            public string Name { get; set; }
           
            public TARGITD3FOConnection.D3FOConnectionManager ConnManager { get; set; }

            public DataStructure ds = new DataStructure();

            public override string ToString()
            {
                return Name;
            }
        }
        List<ConnectionManagerItem> connectionlist = new List<ConnectionManagerItem>();
        public TARGITD3FODataReaderEditor(Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100 metaData, IServiceProvider serviceProvider)
    {
            InitializeComponent();
            this.metaData = metaData;
            this.serviceProvider = serviceProvider;
            this.connectionService = (IDtsConnectionService)serviceProvider.GetService(typeof(IDtsConnectionService));
            this.designTimeInstance = metaData.Instantiate();
           
        }

        public TARGITD3FODataReaderEditor()
        {
            


        }

        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConnectionManagerPage = new System.Windows.Forms.TabPage();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.tabAccessMode = new System.Windows.Forms.TabControl();
            this.tabTables = new System.Windows.Forms.TabPage();
            this.comboTablesList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabSQL = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboConnection = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabColumnsPage = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.tabErrorOutputPage = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabConnectionManagerPage.SuspendLayout();
            this.tabAccessMode.SuspendLayout();
            this.tabTables.SuspendLayout();
            this.tabSQL.SuspendLayout();
            this.tabColumnsPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabErrorOutputPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Items.AddRange(new object[] {
            "Connection Manager",
            "Columns",
            "Error Output"});
            this.listBox1.Location = new System.Drawing.Point(23, 25);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(150, 454);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabConnectionManagerPage);
            this.tabControl1.Controls.Add(this.tabColumnsPage);
            this.tabControl1.Controls.Add(this.tabErrorOutputPage);
            this.tabControl1.Location = new System.Drawing.Point(208, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(658, 467);
            this.tabControl1.TabIndex = 1;
            // 
            // tabConnectionManagerPage
            // 
            this.tabConnectionManagerPage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabConnectionManagerPage.Controls.Add(this.buttonPreview);
            this.tabConnectionManagerPage.Controls.Add(this.tabAccessMode);
            this.tabConnectionManagerPage.Controls.Add(this.comboMode);
            this.tabConnectionManagerPage.Controls.Add(this.label3);
            this.tabConnectionManagerPage.Controls.Add(this.button1);
            this.tabConnectionManagerPage.Controls.Add(this.comboConnection);
            this.tabConnectionManagerPage.Controls.Add(this.label2);
            this.tabConnectionManagerPage.Controls.Add(this.label1);
            this.tabConnectionManagerPage.Location = new System.Drawing.Point(4, 22);
            this.tabConnectionManagerPage.Name = "tabConnectionManagerPage";
            this.tabConnectionManagerPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnectionManagerPage.Size = new System.Drawing.Size(650, 441);
            this.tabConnectionManagerPage.TabIndex = 0;
            this.tabConnectionManagerPage.Text = "ConnectionManager";
            // 
            // buttonPreview
            // 
            this.buttonPreview.Location = new System.Drawing.Point(29, 410);
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(109, 23);
            this.buttonPreview.TabIndex = 7;
            this.buttonPreview.Text = "Preview";
            this.buttonPreview.UseVisualStyleBackColor = true;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // tabAccessMode
            // 
            this.tabAccessMode.Controls.Add(this.tabTables);
            this.tabAccessMode.Controls.Add(this.tabSQL);
            this.tabAccessMode.Location = new System.Drawing.Point(25, 177);
            this.tabAccessMode.Name = "tabAccessMode";
            this.tabAccessMode.SelectedIndex = 0;
            this.tabAccessMode.Size = new System.Drawing.Size(619, 230);
            this.tabAccessMode.TabIndex = 6;
            // 
            // tabTables
            // 
            this.tabTables.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabTables.Controls.Add(this.comboTablesList);
            this.tabTables.Controls.Add(this.label4);
            this.tabTables.Location = new System.Drawing.Point(4, 22);
            this.tabTables.Name = "tabTables";
            this.tabTables.Padding = new System.Windows.Forms.Padding(3);
            this.tabTables.Size = new System.Drawing.Size(611, 204);
            this.tabTables.TabIndex = 0;
            this.tabTables.Text = "Table";
            // 
            // comboTablesList
            // 
            this.comboTablesList.FormattingEnabled = true;
            this.comboTablesList.Location = new System.Drawing.Point(41, 37);
            this.comboTablesList.Name = "comboTablesList";
            this.comboTablesList.Size = new System.Drawing.Size(403, 21);
            this.comboTablesList.TabIndex = 1;
            this.comboTablesList.SelectedIndexChanged += new System.EventHandler(this.comboTablesList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Name of the table or a view";
            // 
            // tabSQL
            // 
            this.tabSQL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabSQL.Controls.Add(this.button5);
            this.tabSQL.Controls.Add(this.button4);
            this.tabSQL.Controls.Add(this.button2);
            this.tabSQL.Controls.Add(this.richTextBox1);
            this.tabSQL.Controls.Add(this.label5);
            this.tabSQL.Location = new System.Drawing.Point(4, 22);
            this.tabSQL.Name = "tabSQL";
            this.tabSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tabSQL.Size = new System.Drawing.Size(611, 204);
            this.tabSQL.TabIndex = 1;
            this.tabSQL.Text = "SQL";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(483, 112);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(104, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Parse Query";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(483, 72);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Browse...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(483, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Parameters";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Location = new System.Drawing.Point(10, 33);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(435, 147);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "SQL command text";
            // 
            // comboMode
            // 
            this.comboMode.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.comboMode.FormattingEnabled = true;
            this.comboMode.Items.AddRange(new object[] {
            "Table or view",
            "SQL command"});
            this.comboMode.Location = new System.Drawing.Point(22, 138);
            this.comboMode.Name = "comboMode";
            this.comboMode.Size = new System.Drawing.Size(452, 21);
            this.comboMode.TabIndex = 5;
            this.comboMode.SelectedIndexChanged += new System.EventHandler(this.comboMode_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data access mode";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(512, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "New ...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboConnection
            // 
            this.comboConnection.FormattingEnabled = true;
            this.comboConnection.Location = new System.Drawing.Point(22, 65);
            this.comboConnection.Name = "comboConnection";
            this.comboConnection.Size = new System.Drawing.Size(452, 21);
            this.comboConnection.TabIndex = 2;
            this.comboConnection.SelectedIndexChanged += new System.EventHandler(this.comboConnection_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Connection manager";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(532, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Specify TARGIT D3FO connection manager , a data source and select data access mod" +
    "e. Edit SQL command.";
            // 
            // tabColumnsPage
            // 
            this.tabColumnsPage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabColumnsPage.Controls.Add(this.panel2);
            this.tabColumnsPage.Controls.Add(this.panel1);
            this.tabColumnsPage.Location = new System.Drawing.Point(4, 22);
            this.tabColumnsPage.Name = "tabColumnsPage";
            this.tabColumnsPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabColumnsPage.Size = new System.Drawing.Size(650, 441);
            this.tabColumnsPage.TabIndex = 1;
            this.tabColumnsPage.Text = "Columns";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(7, 329);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(627, 96);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(7, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 294);
            this.panel1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(134, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Under construction";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // tabErrorOutputPage
            // 
            this.tabErrorOutputPage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabErrorOutputPage.Controls.Add(this.panel3);
            this.tabErrorOutputPage.Location = new System.Drawing.Point(4, 22);
            this.tabErrorOutputPage.Name = "tabErrorOutputPage";
            this.tabErrorOutputPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabErrorOutputPage.Size = new System.Drawing.Size(650, 441);
            this.tabErrorOutputPage.TabIndex = 2;
            this.tabErrorOutputPage.Text = "ErrorOutput";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(6, 22);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(638, 386);
            this.panel3.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(530, 485);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(95, 36);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(652, 485);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(82, 35);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Location = new System.Drawing.Point(777, 485);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(85, 36);
            this.buttonHelp.TabIndex = 4;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            // 
            // TARGITD3FODataReaderEditor
            // 
            this.ClientSize = new System.Drawing.Size(923, 536);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.listBox1);
            this.Name = "TARGITD3FODataReaderEditor";
            this.Load += new System.EventHandler(this.TARGITD3FODataReaderEditor_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabConnectionManagerPage.ResumeLayout(false);
            this.tabConnectionManagerPage.PerformLayout();
            this.tabAccessMode.ResumeLayout(false);
            this.tabTables.ResumeLayout(false);
            this.tabTables.PerformLayout();
            this.tabSQL.ResumeLayout(false);
            this.tabSQL.PerformLayout();
            this.tabColumnsPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabErrorOutputPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void TARGITD3FODataReaderEditor_Load(object sender, EventArgs e)
        {
            var connections = connectionService.GetConnections();

            string connectionManagerId = string.Empty;

            var currentConnectionManager = this.metaData.RuntimeConnectionCollection[0];
            if (currentConnectionManager != null)
            {
                connectionManagerId = currentConnectionManager.ConnectionManagerID;
                
           }

            for (int i = 0; i < connections.Count; i++)
            {
                var conn = connections[i].InnerObject as TARGITD3FOConnection.D3FOConnectionManager;

                if (conn != null)
                {
                    var item = new ConnectionManagerItem()
                    {
                        Name = connections[i].Name,
                        ConnManager = conn,
                        ID = connections[i].ID
                    };
                    currentManager = item;
                    connectionlist.Add(item);
                    comboConnection.Items.Add(item);
                    comboConnection.SelectedIndex = 0;
                    cm = conn;
                    if (connections[i].ID.Equals(connectionManagerId))
                    {
                        comboConnection.SelectedIndex = i;
                        cm = conn;
                        currentManager = item;
                    }
                    
                }
            }
            if(connectionlist.Count > 0)
            {
                foreach(var item in connectionlist)
                {
                    ExecuteSQL(item.ConnManager);
                    if (sqlReader != null)
                    {
                        int k = 0;
                        while (sqlReader.Read())
                        {
                            if (sqlReader[2] == null) continue;
                            string tname = (String.Format("{0}", sqlReader[0]));
                            string fname = (String.Format("{0}", sqlReader[1]));
                            string type = (String.Format("{0}", sqlReader[2]));

                            item.ds.AddTableField(tname, fname, type);
                            // comboTablesList.Items.Add(item);
                           

                        }

                    }
                  

                }
            }
            if (currentManager != null)
            {
                foreach (var v in currentManager.ds.Tables)
                {
                    comboTablesList.Items.Add(v);

                }
                comboTablesList.SelectedIndex = 0;
                comboMode.SelectedIndex = 0;
            }

           
        }
        private void UpdateConnectionTab()
        {
            if (currentManager != null)
            {
                comboTablesList.Items.Clear();
                foreach (var v in currentManager.ds.Tables)
                {
                    
                    comboTablesList.Items.Add(v);

                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            System.Collections.ArrayList created = connectionService.CreateConnection("TARGITD3FOConnection");

            foreach (ConnectionManager cm in created)
            {
                var item = new ConnectionManagerItem()
                {
                    Name = cm.Name,
                    ConnManager = cm.InnerObject as TARGITD3FOConnection.D3FOConnectionManager,
                    ID = cm.ID
                };

                comboConnection.Items.Insert(0, item);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(listBox1.SelectedIndex)
            {
                case 0:
                    {

                        tabControl1.SelectedIndex = 0;
                    }
                    break;
                case 1:
                    {
                        tabControl1.SelectedIndex = 1;
                    }
                    break;
                case 2:
                    {
                        tabControl1.SelectedIndex = 2;
                    }
                    break;
                default: tabControl1.SelectedIndex = 0;
                    break;


            }
        }

        private void comboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboMode.SelectedIndex)
            {
                case 0:
                    {
                        comboTablesList.Items.Clear();
                        this.SQLString = String.Empty;
                        foreach (var v in currentManager.ds.Tables)
                        {
                            comboTablesList.Items.Add(v);

                        }
                        comboTablesList.SelectedIndex = 0;
                        comboMode.SelectedIndex = 0;
                        tabAccessMode.SelectedIndex = 0;
                        break;
                    }
                case 1:
                    {
                        comboTablesList.Items.Clear();
                        this.SQLString = String.Empty;
                        foreach (var v in currentManager.ds.Tables)
                        {
                            comboTablesList.Items.Add(v);

                        }
                        comboTablesList.SelectedIndex = 0;
                        comboMode.SelectedIndex = 1;
                        tabAccessMode.SelectedIndex = 1;
                        break;
                    }
                default:
                    comboTablesList.Items.Clear();
                    foreach (var v in currentManager.ds.Tables)
                    {
                        comboTablesList.Items.Add(v);

                    }
                    comboTablesList.SelectedIndex = 0;
                    comboMode.SelectedIndex = 0;
                    tabAccessMode.SelectedIndex = 0;
                    break;


            }
        }

        public  void ExecuteSQL(D3FOConnectionManager cm)
        {

            try
            {
               

              //  cm = new D3FOConnectionManager();
                cm.AcquireConnection(null);
                sqlConn = cm.GetDbConnection();
                //  DbConnection sqlConn = (DbConnection)connection;
                sqlConn.Open();
                DbCommand cmd = sqlConn.CreateCommand();
                
                cmd.CommandText = "SELECT * FROM [sys].columns";
                cmd.CommandType = System.Data.CommandType.Text;
              
                sqlReader = cmd.ExecuteReader();  
                


            }
            catch (Exception)
            {
               
                throw;
            }
           
        }
        //////
        public void ExecuteSQL1()
        {

            try
            {
                //  cm = new D3FOConnectionManager();
                cm.AcquireConnection(null);
                sqlConn = cm.GetDbConnection();
                //  DbConnection sqlConn = (DbConnection)connection;
                sqlConn.Open();
                DbCommand cmd = sqlConn.CreateCommand();
                if (comboMode.SelectedIndex == 0)
                {
                    string s = this.comboTablesList.SelectedItem.ToString();
                    cmd.CommandText = "SELECT * FROM " + s;
                    cmd.CommandType = System.Data.CommandType.Text;
                }
                else
                {
                    string s = richTextBox1.Text;
                    cmd.CommandText = s;
                    cmd.CommandType = System.Data.CommandType.Text;
                }
                var sqlReader1 = cmd.ExecuteReader();
                
                               
                                var dt = new DataTable();
                DataReaderParser dp = new DataReaderParser(sqlReader1);
                dt = dp.ConvertDataReaderToTableManually();
                               TableContent t = new TableContent();
                t.BuildContent(dt);
                t.Show();
                
               

               

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void comboConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            int j = comboConnection.SelectedIndex;
            currentManager = connectionlist[j];
            
            UpdateConnectionTab();
            this.SQLString = String.Empty;
        }



               private void button5_Click(object sender, EventArgs e)
        {
         

                try
                {
                    //  cm = new D3FOConnectionManager();
                    cm.AcquireConnection(null);
                    sqlConn = cm.GetDbConnection();
                    //  DbConnection sqlConn = (DbConnection)connection;
                    sqlConn.Open();
                    DbCommand cmd = sqlConn.CreateCommand();

                string s = richTextBox1.Text;

                cmd.CommandText =  s;
                    cmd.CommandType = System.Data.CommandType.Text;

                    var sqlReader1 = cmd.ExecuteReader();
                    MessageBox.Show("SQL query statment OK!");
                

                }
                catch (Exception exp)
                {

                MessageBox.Show("Error!" + exp.ToString());
               
                }

           
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {

            ExecuteSQL1();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                richTextBox1.Clear();
                richTextBox1.Text = sr.ReadToEnd();
                //MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboConnection.SelectedItem != null)
            {
                var item = (ConnectionManagerItem)comboConnection.SelectedItem;
                this.metaData.RuntimeConnectionCollection[0].ConnectionManagerID = item.ID;
            }
            if(String.IsNullOrEmpty(this.SQLString))
            designTimeInstance.SetComponentProperty("QueueName", "Place for SQL query");
            else designTimeInstance.SetComponentProperty("QueueName", this.SQLString);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void comboTablesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = this.comboTablesList.SelectedItem.ToString();
            this.SQLString = "SELECT * FROM " + s;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string s = this.comboTablesList.SelectedItem.ToString();
            this.SQLString = richTextBox1.Text.ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        ///////////////

    }


    }


