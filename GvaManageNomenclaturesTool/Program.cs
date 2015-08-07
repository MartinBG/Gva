using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Gva.Api.CommonUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gva.ManageNomenclaturesTool
{
    class Program
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["Gva"].ConnectionString;

        static void Main(string[] args)
        {
           
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                UpdateNomenclature("documentRoles", "caseTypeAlias", "caseTypeAliases", connection);
                UpdateNomenclature("documentTypes", "caseTypeAlias", "caseTypeAliases", connection);
            }
        }

        private static void UpdateNomenclature(string nomenclatureAlias, string srcFieldName, string destFieldName, SqlConnection connection)
        {
            var nomenclaturesToChange = connection.CreateStoreCommand(
                       @"SELECT nv.NomValueId, nv.TextContent FROM nomValues nv
                         JOIN Noms n ON n.NomId = nv.NomId
                         WHERE {0}",
                        new DbClause("n.Alias LIKE {0}", nomenclatureAlias))
                       .Materialize(r =>
                           new
                           {
                               NomValueId = r.Field<int>("NomValueId"),
                               TextContent = r.Field<string>("TextContent")
                           })
                       .ToList();

            
            foreach (var nomenclature in nomenclaturesToChange)
            {
                JObject srcTextContent = JObject.Parse(nomenclature.TextContent);
                JObject newTextContent = new JObject();
                foreach (var oldProp in srcTextContent.Properties().Where(p => p.Name != srcFieldName))
                {
                    newTextContent.Add(new JProperty(oldProp.Name, oldProp.Value));
                }

                var srcPropToChange = srcTextContent.Properties().Where(p => p.Name == srcFieldName).SingleOrDefault();
                if (srcPropToChange != null)
                {
                    var destPropValue = srcPropToChange.Value.ToString() != "" ? new string[] { srcPropToChange.Value.ToString() } : new string[]{};
                    newTextContent.Add(new JProperty(destFieldName, destPropValue));

                    UpdateTextContent(newTextContent, nomenclature.NomValueId, connection);
                }
            }
        }

        private static void UpdateTextContent(JObject newTextContent, int nomValueId, SqlConnection connection)
        {
            using (var transaction = connection.BeginTransaction("transaction"))
            {
                Formatting formatting;
#if DEBUG
                formatting = Formatting.Indented;
#else
                formatting = Formatting.None;
#endif
                string result = JsonConvert.SerializeObject(newTextContent, formatting);

                string cmdText = @"UPDATE NomValues
                                   SET TextContent = @NewTextContent
                                   WHERE NomValueId = @NomValueId";

                SqlCommand command = new SqlCommand(cmdText, connection, transaction);
                command.Parameters.AddWithValue("@NewTextContent", result);
                command.Parameters.AddWithValue("@NomValueId", nomValueId);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Successfully updated textContent of nomenclature with nomValueId {0}\n", nomValueId);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in update of textContent of nomenclature with nomValueId {0}", nomValueId);
                    throw;
                }
            }
        }
    }
}
