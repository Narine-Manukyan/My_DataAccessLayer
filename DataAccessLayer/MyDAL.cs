using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace DataAccessLayer
{
    /// <summary>
    /// For accessing database data.
    /// </summary>
    public class MyDAL: IDataAccessor
    {
        // Provides a simple way to create and manage the contents of connection strings used by the SqlConnection class.
        private string connectionString;
        // Represents the sql query's file path.
        private string filePath;

        /// <summary>
        /// The parameterFull constructor.
        /// </summary>
        /// <param name="filePath">Represents the sql query's file path.</param>
        public MyDAL(string filePath)
        {
            this.connectionString = " Data Source = NAR-ПК\\TESTINSTANCE;" +
                "Initial Catalog = AdventureWorks2;" +
                "Integrated Security = false;" +
                "User ID = sa; Password = ******;";
            this.filePath = filePath;
        }

        /// <summary>
        /// For executing and retrieving data for a given operation and input parameters.
        /// </summary>
        /// <param name="code">A code which specifies mapped name of the operation to be executed.</param>
        /// <param name="parameters">
        /// Key is the parameter’s name...value is the parameter’s value.
        /// </param>
        /// <returns>IEnumerable<T></returns>
        public IEnumerable<object> GetData(string code, List<KeyValuePair<string, object>> parameters)
        {
            if (File.Exists(this.filePath))
            {
                // Open the file, read from it and close it.
                string fullText = File.ReadAllText(filePath);
                int index = fullText.IndexOf("name:" + code);
                if (fullText.Contains(code))
                {
                    string tempText = fullText.Substring(index + ("name:" + code).Length);

                    string cmd = "";
                    if (tempText.IndexOf("name:") != -1)
                    {
                        for (int i = 0; i != tempText.IndexOf("name:"); i++)
                        {
                            cmd += tempText[i];
                        }
                    }
                    else
                        cmd = tempText;


                    using (SqlConnection connection = new SqlConnection(this.connectionString))
                    {
                        // Create a command and sets it's properties.
                        SqlCommand command = new SqlCommand(cmd, connection);

                        //command.Connection = connection;
                        //command.CommandType = CommandType.StoredProcedure;
                        //command.CommandText = "";

                        // Open the connection and execute the reader.
                        connection.Open();

                        for (int i = 0; i < parameters.Count; i++)
                        {
                            command.Parameters.Add(new SqlParameter(parameters[i].Key, parameters[i].Value));
                        }

                        SqlDataReader reader = command.ExecuteReader();
                        var result = new List<object>[reader.FieldCount];
                        if (reader.HasRows)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                result[i] = new List<Object>(); 
                            }
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    result[i].Add(reader[i]);
                                    Console.WriteLine(result[i][0]);
                                }
                            }
                        }
                        reader.Close();
                        return result;
                    }
                }
                return null;
            }
            else
                throw new FileNotFoundException("There wasn't found file with this path.");
        }
    }
}
