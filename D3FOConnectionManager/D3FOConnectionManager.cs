using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.SqlServer.Dts.Runtime;

namespace D3FOConnectionManager
{
    [DtsConnection(ConnectionType = "D3FO", DisplayName = "D3FOConnection",
                  Description = "Connection Manager for D3FO")]
    public class D3FOConnectionManager : ConnectionManagerBase, IDTSComponentPersist
    {
        #region Variables for internal use
        // The template for the connectionstring, but without the sensitive password property
        private const string CONNECTIONSTRING_TEMPLATE = "URL=<URL>;UserName=<UserName>;";
        #endregion
        #region Get Set Properties
        /*
         * The properties of connection manager that
         * will be saved in the XML of the SSIS package.
         * You can add a Category and Description
         * for each property making it clearer.
         */
        private string _connectionString = String.Empty;
        public override string ConnectionString
        {
            get
            {
                UpdateConnectionString();
                return _connectionString;
            }
            //connectionstring is now readonly
            //set
            //{
            //    _connectionString = value;
            //}
        }


        private string _name = String.Empty;
        [CategoryAttribute("D3FO connection manager")]
        [Description("Some URL to do something with in an other task or transformation")]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        private string _assembly = String.Empty;
        [CategoryAttribute("D3FO connection manager")]
        [Description("Some Assemby")]
        public string Assembly
        {
            get { return this._assembly; }
            set { this._assembly = value; }
        }

        private string _connection_Method = String.Empty;
        [CategoryAttribute("D3FO connection manager")]
        [Description("SomeConnection_Method")]
        public string Connection_Method
        {
            get { return this._connection_Method; }
            set { this._connection_Method = value; }
        }

        private string _company = String.Empty;
        [CategoryAttribute("D3FO connection manager")]
        [Description("Some Company")]
        public string Company
        {
            get { return this._company; }
            set { this._company = value; }
        }

        private string _aos_Uri = String.Empty;
        [CategoryAttribute("D3FO connection manager")]
        [Description("Some AOS_Uri")]
        public string AOS_Uri
        {
            get { return this._aos_Uri; }
            set { this._aos_Uri = value; }
        }

        private string _ad_Resource = String.Empty;
        [CategoryAttribute("D3FO connection manager")]
        [Description("Some AD_Resource")]
        public string AD_Resource
        {
            get { return this._ad_Resource; }
            set { this._ad_Resource = value; }
        }

        private string _ad_Tenant = String.Empty;
        [CategoryAttribute("D3FO connection manager")]
        [Description("Some AD_Tenant")]
        public string AD_Tenant
        {
            get { return this._ad_Tenant; }
            set { this._ad_Tenant = value; }
        }

        private string _ad_Client_App_ID = String.Empty;
        [CategoryAttribute("D3FO connection manager")]
        [Description("Some AD_Client_App_ID")]
        public string AD_Client_App_ID
        {
            get { return this._ad_Client_App_ID; }
            set { this._ad_Client_App_ID = value; }
        }

        private string _ad_Client_App_Secret = String.Empty;
        [CategoryAttribute("D3FO connection manager")]
        [Description("Some AD_Client_App_Secret")]
        public string AD_Client_App_Secret
        {
            get { return this._ad_Client_App_Secret; }
            set { this._ad_Client_App_Secret = value; }
        }

        private bool _local = true;
        [CategoryAttribute("D3FO connection manager")]
        [Description("Some local")]
        public bool Local
        {
            get { return this._local; }
            set { this._local = value; }
        }

        private bool _server = true;
        [CategoryAttribute("D3FO connection manager")]
        [Description("Some Server")]
        public bool Server
        {
            get { return this._server; }
            set { this._server = value; }
        }

        #region Overriden methods
        public override object AcquireConnection(object txn)
        {
            // Set the connectionstring
            UpdateConnectionString();
            return base.AcquireConnection(txn);
        }

        public override void ReleaseConnection(object connection)
        {
            base.ReleaseConnection(connection);
        }

        public override DTSExecResult Validate(IDTSInfoEvents infoEvents)
        {
            // Very basic validation
            // Check if the URL field is filled.
            // Note: this is a runtime validation
            // In the form you can add some more
            // designtime validation.
            if (string.IsNullOrEmpty(Name))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            else
            {
                return DTSExecResult.Success;
            }

            if (string.IsNullOrEmpty(Assembly))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            else
            {
                return DTSExecResult.Success;
            }
            if (string.IsNullOrEmpty(Company))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            else
            {
                return DTSExecResult.Success;
            }
            if (string.IsNullOrEmpty(AOS_Uri))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            else
            {
                return DTSExecResult.Success;
            }
            if (string.IsNullOrEmpty(AD_Resource))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            else
            {
                return DTSExecResult.Success;
            }
            if (string.IsNullOrEmpty(AD_Tenant))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            else
            {
                return DTSExecResult.Success;
            }
            if (string.IsNullOrEmpty(AD_Client_App_ID))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            else
            {
                return DTSExecResult.Success;
            }
            if (string.IsNullOrEmpty(AD_Client_App_Secret))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            else
            {
                return DTSExecResult.Success;
            }
        }
        #endregion
        #region Update ConnectionString
        private void UpdateConnectionString()
        {
            // Create a connectionstring, but without sensitive properties like the password
            String connectionString = CONNECTIONSTRING_TEMPLATE;

            connectionString = connectionString.Replace("<URL>", URL);
            connectionString = connectionString.Replace("<UserName>", UserName);

            _connectionString = connectionString;
        }
        #endregion

        #region Methods for IDTSComponentPersist
        // These two methods are for saving the data in the package XML without showing sensitive data
        void IDTSComponentPersist.LoadFromXML(System.Xml.XmlElement node, IDTSInfoEvents infoEvents)
        {
            //	Checking if XML is correct. This might occur if the connection manager XML has been modified outside BIDS/SSDT
            if (node.Name != "MYCONNECTIONMANAGER")
            {
                throw new Exception(string.Format("Unexpected connectionmanager element when loading task - {0}.", "MYCONNECTIONMANAGER"));
            }
            else
            {
                // Fill properties with values from package XML
                this._userName = node.Attributes.GetNamedItem("UserName").Value;
                this._url = node.Attributes.GetNamedItem("URL").Value;


                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "Password")
                    {
                        this._password = childNode.InnerText;
                    }
                }
                this._connectionString = node.Attributes.GetNamedItem("ConnectionString").Value;
            }
        }

        void IDTSComponentPersist.SaveToXML(System.Xml.XmlDocument doc, IDTSInfoEvents infoEvents)
        {
            XmlElement rootElement = doc.CreateElement("MYCONNECTIONMANAGER");
            doc.AppendChild(rootElement);

            XmlAttribute connectionStringAttr = doc.CreateAttribute("ConnectionString");
            connectionStringAttr.Value = _connectionString;
            rootElement.Attributes.Append(connectionStringAttr);

            XmlAttribute userNameStringAttr = doc.CreateAttribute("UserName");
            userNameStringAttr.Value = _userName;
            rootElement.Attributes.Append(userNameStringAttr);

            XmlAttribute urlStringAttr = doc.CreateAttribute("URL");
            urlStringAttr.Value = _url;
            rootElement.Attributes.Append(urlStringAttr);

            if (!string.IsNullOrEmpty(_password))
            {
                XmlElement passwordElement = doc.CreateElement("Password");
                rootElement.AppendChild(passwordElement);
                passwordElement.InnerText = _password;

                // This will make the password property sensitive
                XmlAttribute passwordAttr = doc.CreateAttribute("Sensitive");
                passwordAttr.Value = "1";
                passwordElement.Attributes.Append(passwordAttr);
            }
        }
        #endregion



    }
}
