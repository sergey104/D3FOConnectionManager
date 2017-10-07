﻿using System;

using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Reflection;

using System.Data.Common;

using Microsoft.SqlServer.Dts.Runtime;
using System.Data;

namespace D3FOConnectionManager
{
    [DtsConnection(ConnectionType = "TARGITD3FO", DisplayName = "TARGITD3FOConnection",
                  Description = "TARGITD3FO Connection Manager",
        UITypeName = "D3FOConnectionManager.myConnectionManagerUI,Version=1.0.0.0,Culture=neutral,PublicKeyToken=80664248b6de6485")]
    public class D3FOConnectionManager : ConnectionManagerBase, IDTSComponentPersist
    {
        #region Variables for internal use
        // The template for the connectionstring, but without the sensitive password property
        private const string CONNECTIONSTRING_TEMPLATE = "DATASOURCE <Name> = DOTNET CONNECTION '<Connetion_Method>' 'aosUri=<AOS_Uri>;activeDirectoryResource=<AD_Resource>;activeDirectoryTenant=<AD_Tenant>;activeDirectoryClientAppId=<AD_Client_App_ID>;activeDirectoryClientAppSecret=<AD_Client_App_Secret>'";
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

        private string _assemblyp = String.Empty;
        [CategoryAttribute("D3FO connection manager")]
        [Description("Some Assemby")]
        public string Assemblyp
        {
            get { return this._assemblyp; }
            set { this._assemblyp = value; }
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
            Assembly asm = Assembly.LoadFrom(@"D:\Weather\TARGIT.WeatherService\TARGIT.WeatherService.dll");

            var FuncConnection = asm.DefinedTypes.First(c => c.Name == "WeatherConnection");

            string errorstr = "";

            //           DbConnection dbConnection = DatabaseConn.CreateDbConnection(FuncConnection.FullName, @"ignoreemptyvalues=false;datasource=C:\demodata\PopulationCBSA.xlsx;detectionrowscount=100", out errorstr);
            //       DbConnection dbConnection = DatabaseConn.CreateDbConnection(FuncConnection.FullName, @"DATASOURCE [D365 Cloud] = DOTNET CONNECTION 'TARGIT.AX7.AX7Connection' 'aosUri=https://envdemo-10a825f4ddcbe392865aos.cloudax.dynamics.com;activeDirectoryResource=https://envdemo-10a825f4ddcbe392865aos.cloudax.dynamics.com;activeDirectoryTenant=https://login.windows.net/9d2a793f-db8c-4949-820c-34e31d66b3cd;activeDirectoryClientAppId=90ad8d0d-99c4-4b66-a349-f7017f49cab2;activeDirectoryClientAppSecret=RAYMdhcvkfMl6JGMuARB7PEgJjdMNc2u4BeC2XKr7Ac=", out errorstr);
            DbConnection dbConnection = DatabaseConn.CreateDbConnection(FuncConnection.FullName, @"DOTNET CONNECTION 'TARGIT.WeatherService.WeatherConnection' 'url=https://www.illo.com/Weather/2.0/WeatherService.svc;IsCacheEnabled=true'", out errorstr);
            return dbConnection;
           // dbConnection.Open();
        }

        public override void ReleaseConnection(object connection)
        {
            if (connection != null)
            {
                ((IDbConnection)connection).Close();
            }
        }
#endregion
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
            

            if (string.IsNullOrEmpty(Assemblyp))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            
            if (string.IsNullOrEmpty(Company))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            
            if (string.IsNullOrEmpty(AOS_Uri))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            
            if (string.IsNullOrEmpty(AD_Resource))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            
            if (string.IsNullOrEmpty(AD_Tenant))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
            }
            
            if (string.IsNullOrEmpty(AD_Client_App_ID))
            {
                infoEvents.FireError(0, "D3FO Connection Manager", "Field is mandatory.", string.Empty, 0);
                return DTSExecResult.Failure;
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

            connectionString = connectionString.Replace("<Name>", Name);
            connectionString = connectionString.Replace("<Assembly>", Assemblyp);
            connectionString = connectionString.Replace("<Connection_Method>", Connection_Method);
            connectionString = connectionString.Replace("<Company>", Company);
            connectionString = connectionString.Replace("<AOS_Uri>", AOS_Uri);
            connectionString = connectionString.Replace("<AD_Resource>", AD_Resource);
            connectionString = connectionString.Replace("<AD_Tenant>", AD_Tenant);
            connectionString = connectionString.Replace("<AD_Client_App_ID>", _ad_Client_App_ID);
            connectionString = connectionString.Replace("<AD_Client_App_Secret>", _ad_Client_App_Secret);

            _connectionString = connectionString;
        }
        #endregion

        #region Methods for IDTSComponentPersist
        // These two methods are for saving the data in the package XML without showing sensitive data
        void IDTSComponentPersist.LoadFromXML(System.Xml.XmlElement node, IDTSInfoEvents infoEvents)
        {
            //	Checking if XML is correct. This might occur if the connection manager XML has been modified outside BIDS/SSDT
            if (node.Name != "D3FOCONNECTIONMANAGER")
            {
                throw new Exception(string.Format("Unexpected connectionmanager element when loading task - {0}.", "D3FOCONNECTIONMANAGER"));
            }
            else
            {
                // Fill properties with values from package XML
                this._name = node.Attributes.GetNamedItem("Name").Value;
                this._assemblyp = node.Attributes.GetNamedItem("Assemblyp").Value;
                this._company = node.Attributes.GetNamedItem("Company").Value;
                this._connection_Method = node.Attributes.GetNamedItem("Connection_Method").Value;
                this._aos_Uri = node.Attributes.GetNamedItem("AOS_Uri").Value;
                this._ad_Resource = node.Attributes.GetNamedItem("AD_Resource").Value;
                this._ad_Tenant = node.Attributes.GetNamedItem("AD_Tenant").Value;
                this._ad_Client_App_ID = node.Attributes.GetNamedItem("AD_Client_App_ID").Value;
                this._ad_Client_App_Secret = node.Attributes.GetNamedItem("AD_Client_App_Secret").Value;


                
                this._connectionString = node.Attributes.GetNamedItem("ConnectionString").Value;
            }
        }

        void IDTSComponentPersist.SaveToXML(System.Xml.XmlDocument doc, IDTSInfoEvents infoEvents)
        {
            XmlElement rootElement = doc.CreateElement("D3FOCONNECTIONMANAGER");
            doc.AppendChild(rootElement);

            XmlAttribute connectionStringAttr = doc.CreateAttribute("ConnectionString");
            connectionStringAttr.Value = _connectionString;
            rootElement.Attributes.Append(connectionStringAttr);

            XmlAttribute nameStringAttr = doc.CreateAttribute("Name");
            nameStringAttr.Value = _name;
            rootElement.Attributes.Append(nameStringAttr);
            XmlAttribute assemblyStringAttr = doc.CreateAttribute("Assembly");
            assemblyStringAttr.Value = _assemblyp;
            rootElement.Attributes.Append(assemblyStringAttr);

            XmlAttribute connectionmethodStringAttr = doc.CreateAttribute("ConnectionMethod");
            connectionmethodStringAttr.Value = _connection_Method;
            rootElement.Attributes.Append(connectionmethodStringAttr);

            XmlAttribute companyStringAttr = doc.CreateAttribute("Company");
            companyStringAttr.Value = _company;
            rootElement.Attributes.Append(companyStringAttr);

            XmlAttribute aosuriStringAttr = doc.CreateAttribute("AOS_Uri");
            aosuriStringAttr.Value = _aos_Uri;
            rootElement.Attributes.Append(aosuriStringAttr);

            XmlAttribute adresourceStringAttr = doc.CreateAttribute("AD_Resource");
            adresourceStringAttr.Value = _ad_Resource;
            rootElement.Attributes.Append(adresourceStringAttr);

            XmlAttribute adtenantStringAttr = doc.CreateAttribute("AD_Tenant");
            adtenantStringAttr.Value = _ad_Tenant;
            rootElement.Attributes.Append(adtenantStringAttr);

            XmlAttribute adclientappidStringAttr = doc.CreateAttribute("AD_Client_App_ID");
            adclientappidStringAttr.Value = _ad_Client_App_ID;
            rootElement.Attributes.Append(adclientappidStringAttr);

            XmlAttribute adclientappsecretStringAttr = doc.CreateAttribute("AD_Client_App_Secret");
            adclientappsecretStringAttr.Value = _ad_Client_App_Secret;
            rootElement.Attributes.Append(adclientappsecretStringAttr);


        }
        #endregion



    }
}
