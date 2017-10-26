using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARGITD3FOConnection;
using System.Collections.Generic;

namespace TARGITD3FODataReader.Models
{
    public class DataStructure
    {
    }
    ////
    public class FieldItem
    {
        private string _name; // String.Empty;
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        private string _type; // String.Empty;
        public string Type
        {
            get { return this._type; }
            set { this._type = value; }
        }
        public FieldItem()
        {

        }
        public FieldItem(string n, string t)
        {
            _name = n;
            _type = t;
        }
    }
    ///
    public class TableItem
    {
        private string _name; // String.Empty;
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        public List<FieldItem> Fields;
    }

}
