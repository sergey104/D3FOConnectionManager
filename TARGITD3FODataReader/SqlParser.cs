using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.TransactSql.ScriptDom;



namespace TARGITD3FOConnection
{
    public class SqlParser
    {
        public List<string> Parse(string sql)
        {
            TSql100Parser parser = new TSql100Parser(false);
            TSqlFragment fragment;
            
            IList<ParseError> errors;
            fragment = parser.Parse(new System.IO.StringReader(sql), out errors);
            if (errors != null && errors.Count > 0)
            {
                List<string> errorList = new List<string>();
                foreach (var error in errors)
                {
                    errorList.Add(error.Message);
                }
                return errorList;
            }
            return null;
        }
    }
}
