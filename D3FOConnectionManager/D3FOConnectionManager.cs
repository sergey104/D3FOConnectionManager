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
    public class D3FOConnectionManager : ConnectionManagerBase
    {

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





    }
}
