using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Configuration;

namespace Gva.FileMakerConverter
{
    class FileMakerConverter
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GvaAircraft"].ConnectionString;
            string fileMakerNamespace = ConfigurationManager.AppSettings["FileMakerNamespace"];

            XName FMPXMLRESULT = XName.Get("FMPXMLRESULT", fileMakerNamespace);
            XName METADATA = XName.Get("METADATA", fileMakerNamespace);
            XName FIELD = XName.Get("FIELD", fileMakerNamespace);
            XName RESULTSET = XName.Get("RESULTSET", fileMakerNamespace);
            XName ROW = XName.Get("ROW", fileMakerNamespace);
            XName COL = XName.Get("COL", fileMakerNamespace);
            XName DATA = XName.Get("DATA", fileMakerNamespace);

            string[] filePaths = Directory.GetFiles(@"C:\Users\Panka\Desktop\fileMakerExportedFiles\", "*.xml");

            foreach (string filePath in filePaths)
            {
                string tableName = Path.GetFileNameWithoutExtension(filePath);

                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    XDocument d = XDocument.Load(fs, LoadOptions.None);
                    connection.Open();

                    var Fields = d
                        .Element(FMPXMLRESULT)
                        .Element(METADATA)
                        .Elements(FIELD)
                        .Select(e => new
                        {
                            Name = e.Attribute("NAME").Value,
                            Type = e.Attribute("TYPE").Value
                        }).ToList();

                    var Rows = d
                            .Element(FMPXMLRESULT)
                            .Element(RESULTSET)
                            .Elements(ROW);

                    StringBuilder sb;

                    #region drop table

                    SqlCommand droptable = connection.CreateCommand();
                    sb = new StringBuilder();
                    sb.AppendFormat("IF OBJECT_ID('[{0}]','U') IS NOT NULL", tableName); sb.AppendLine();
                    sb.AppendFormat("drop table [{0}]", tableName); sb.AppendLine();
                    droptable.CommandText = sb.ToString();
                    droptable.ExecuteNonQuery();

                    #endregion

                    #region create table
                    SqlCommand createtable = connection.CreateCommand();
                    sb = new StringBuilder();
                    sb.AppendFormat("CREATE TABLE [{0}] (", tableName); sb.AppendLine();
                    for (int i = 0; i < Fields.Count; i++)
                    {
                        if (Fields[i].Type == "TEXT")
                        sb.AppendFormat("[{0}] nvarchar(max)", Fields[i].Name);
                        //TODO: Add types
                        else
                        sb.AppendFormat("[{0}] nvarchar(max)", Fields[i].Name);

                        if (i < Fields.Count - 1)
                        sb.AppendLine(",");
                        else
                        sb.AppendLine("");
                    }
                    sb.AppendLine(")");
                    createtable.CommandText = sb.ToString();
                    createtable.ExecuteNonQuery();
                    #endregion

                    #region insert data
                    SqlCommand inserttable = connection.CreateCommand();
                    sb = new StringBuilder();
                    sb.AppendFormat("INSERT INTO [{0}] (", tableName); sb.AppendLine();
                    for (int i = 0; i < Fields.Count; i++)
                    {
                        sb.AppendFormat("[{0}]", Fields[i].Name);

                        if (i < Fields.Count - 1)
                        sb.AppendLine(",");
                        else
                        sb.AppendLine("");
                    }
                    sb.AppendLine(")");

                    sb.AppendLine("VALUES (");
                    for (int i = 0; i < Fields.Count; i++)
                    {
                        string parname = "@par" + i.ToString();

                        if (Fields[i].Type == "TEXT")
                        inserttable.Parameters.Add(parname, SqlDbType.NVarChar);
                        //TODO: Add types
                        else
                        inserttable.Parameters.Add(parname, SqlDbType.NVarChar);

                        sb.AppendFormat("{0}", parname);

                        if (i < Fields.Count - 1)
                        sb.AppendLine(",");
                        else
                        sb.AppendLine("");
                    }
                    sb.AppendLine(")");
                    inserttable.CommandText = sb.ToString();


                    foreach (var row in Rows)
                    {
                        var cols = row.Elements(COL).Select(e => e.Element(DATA)).ToList();
                        for (int i = 0; i < Fields.Count; i++)
                        {
                        string parname = "@par" + i.ToString();
                        if (cols[i] != null)
                            inserttable.Parameters[parname].Value = cols[i].Value;
                        else
                            inserttable.Parameters[parname].Value = DBNull.Value;
                        }
                        inserttable.ExecuteNonQuery();
                    }
                    #endregion

                    connection.Close();
              }
            }
        }
    }
}
