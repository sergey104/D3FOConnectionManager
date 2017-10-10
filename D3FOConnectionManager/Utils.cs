using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TARGITD3FOConnection
{

    static class Util
    {

        /// <summary>
        /// Helper routine that looks up a type name and tries to retrieve the
        /// full type reference in the actively executing assemblies.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Type GetTypeFromName(string typeName)
        {
            Type type = null;

            // Let default name binding find it
            type = Type.GetType(typeName, false);
            if (type != null)
                return type;

            // look through assembly list
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // try to find manually
            foreach (Assembly asm in assemblies)
            {
                //Console.WriteLine("Scanning Assembly " + asm.GetName());
                type = asm.GetType(typeName, false);


                if (type != null)
                    break;
            }

            /*if ( type==null ) {
                Console.WriteLine ( "Debug: Type not found ");
            }
            else {
                Console.WriteLine("Debug: Type found ");
            }*/

            return type;
        }


    }
    public class DatabaseConn
    {

        // Given a class name and connection string,  
        // create the DbConnection. 
        // Returns a DbConnection on success; null on failure. 
        public static DbConnection CreateDbConnection(
            string className, string connectionString, out String error)
        {
            // Assume failure.
            DbConnection connection = null;
            error = "";

            // Create the DbProviderFactory and DbConnection. 
            if (connectionString != null)
            {
                try
                {
                    Type type = Util.GetTypeFromName(className);

                    if (type != null)
                    {
                        //Console.WriteLine("Start Reflection");
                        ConstructorInfo constructorInfo = type.GetConstructor(System.Type.EmptyTypes);
                        Object instance = constructorInfo.Invoke(null);

                        //Console.WriteLine("Instance Created");
                        connection = (DbConnection)instance;
                        //Console.WriteLine("DBConnection converted");
                        connection.ConnectionString = connectionString;
                        //Console.WriteLine("Connection string assigned");
                    }
                    else
                    {
                        error = "Could not find Class " + className;
                    }


                }
                catch (Exception ex)
                {
                    // Set the connection to null if it was created. 
                    if (connection != null)
                    {
                        connection = null;
                    }
                    error = ex.Message;
                    //Console.WriteLine(ex.Message);
                }
            }
            // Return the connection. 
            return connection;
        }


    }
}
