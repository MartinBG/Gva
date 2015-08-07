using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Gva.Api.CommonUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using Common.Json;
using System.Reflection;
using System.Linq.Expressions;
using Common.Api.Models;
using System.Collections;
using Gva.ManageLotsTool.OldDOs;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Organizations;
using System.Configuration;

namespace Gva.ManageLotsTool
{
    class Program
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["Gva"].ConnectionString;

        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                ConvertModel<PersonRatingDO_Old, PersonRatingDO>("ratings", connection);
                ConvertModel<PersonRatingEditionDO_Old, PersonRatingEditionDO>("ratingEditions", connection);
                ConvertModel<PersonLicenceEditionDO_Old, PersonLicenceEditionDO>("licenceEditions", connection);
                ConvertModel<PersonLicenceDO_Old, PersonLicenceDO>("licences", connection);
                ConvertModel<PersonAddressDO_Old, PersonAddressDO>("personAddresses", connection);
                ConvertModel<PersonLangCertDO_Old, PersonLangCertDO>("personDocumentLangCertificates", connection);
                ConvertModel<PersonCheckDO_Old, PersonCheckDO>("personDocumentChecks", connection);
                ConvertModel<PersonTrainingDO_Old, PersonTrainingDO>("personDocumentTrainings", connection);
                ConvertModel<PersonDocumentIdDO_Old, PersonDocumentIdDO>("personDocumentIds", connection);
                ConvertModel<PersonEducationDO_Old, PersonEducationDO>("personDocumentEducations", connection);
                ConvertModel<PersonEmploymentDO_Old, PersonEmploymentDO>("personDocumentEmployments", connection);
                ConvertModel<PersonDocumentOtherDO_Old, PersonDocumentOtherDO>("personDocumentOthers", connection);
                ConvertModel<PersonStatusDO_Old, PersonStatusDO>("personStatuses", connection);
                ConvertModel<PersonFlyingExperienceDO_Old, PersonFlyingExperienceDO>("personFlyingExperiences", connection);
                ConvertModel<PersonMedicalDO_Old, PersonMedicalDO>("personDocumentMedicals", connection);
                ConvertModel<AircraftDocumentOtherDO_Old, AircraftDocumentOtherDO>("aircraftDocumentOthers", connection);
                ConvertModel<OrganizationDocumentOtherDO_Old, OrganizationDocumentOtherDO>("organizationDocumentOthers", connection);
            }
        }

        private static void ConvertModel<TSrc, TDest>(string path, SqlConnection connection)
            where TDest : class, new()
            where TSrc : class
        {
            var partVersionsToChange = connection.CreateStoreCommand(
                       @"SELECT lpv.LotPartVersionId, lpv.TextContent 
                         FROM LotPartVersions lpv
                         JOIN LotParts lp ON lp.LotPartId = lpv.LotPartId
                         JOIN LotSetParts lsp ON lp.LotSetPartId = lsp.LotSetPartId
                         WHERE {0}",
                        new DbClause("lp.Path LIKE {0} + '/%'", path))
                       .Materialize(r =>
                           new
                           {
                               LotPartVersionId = r.Field<int>("LotPartVersionId"),
                               TextContent = r.Field<string>("TextContent")
                           })
                       .ToList();

            foreach (var partVersion in partVersionsToChange)
            {
                string textContent = partVersion.TextContent;
                TSrc srcModel = JsonConvert.DeserializeObject<TSrc>(textContent);
                TDest destModel = new TDest();

                CopyModel<TSrc, TDest>(srcModel, destModel);

                JObject newTextContent = JObject.FromObject(destModel);
                foreach (var oldProp in JObject.Parse(textContent).Properties().Where(p => p.Name.StartsWith("__")))
                {
                    newTextContent.AddFirst(new JProperty(oldProp.Name, oldProp.Value));
                }

                UpdateTextContent(newTextContent, partVersion.LotPartVersionId, connection);
            }
        }

        public static void CopyModel<TSrc, TDest>(TSrc srcModel, TDest destModel)
            where TDest : class, new()
        {
            foreach (PropertyInfo srcProp in srcModel.GetType().GetProperties())
            {
                if (srcProp.GetValue(srcModel) != null)
                {
                    CopyProperty<TSrc, TDest>(srcProp, srcModel, destModel);
                }
            }
        }

        private static void UpdateTextContent(JObject textContent, int lotPartVersionId, SqlConnection connection)
        {
            using (var transaction = connection.BeginTransaction("transaction"))
            {
                Formatting formatting;
#if DEBUG
                formatting = Formatting.Indented;
#else
                formatting = Formatting.None;
#endif
                string result = JsonConvert.SerializeObject(textContent, formatting);

                string cmdText = @"UPDATE LotPartVersions
                                   SET TextContent = @NewTextContent
                                   WHERE LotPartVersionId = @LotPartVersionId";

                SqlCommand command = new SqlCommand(cmdText, connection, transaction);
                command.Parameters.AddWithValue("@NewTextContent", result);
                command.Parameters.AddWithValue("@LotPartVersionId", lotPartVersionId);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Successfully updated textContent of lotPartVersionId {0}\n", lotPartVersionId);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in update of textContent of LotPartId {0}", lotPartVersionId);
                    throw;
                }
            }
        }

        public static void TrySetPropertyValue<TValue, TDest>(Type destType, string propName, TDest destModel, TValue value)
            where TDest : class
            where TValue : class
        {
            try
            {
                PropertyInfo targetProperty = destType.GetProperty(propName);
                if (targetProperty != null)
                {
                    targetProperty.SetValue(destModel, value);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The requested targetProperty with name {0} doesn't exist in destModel ", propName);
                throw;
            }
        }

        public static void CopyProperty<TSrc, TDest>(PropertyInfo srcProp, TSrc srcModel, TDest destModel)
            where TDest : class, new()
        {
            var srcPropValue = srcProp.GetValue(srcModel);
            Type srcType = srcProp.PropertyType;
            Type destType = destModel.GetType();

            try
            {
                string propNamespace = srcProp.PropertyType.Namespace == "System.Collections.Generic" ?
                    srcProp.PropertyType.GenericTypeArguments[0].Namespace :
                    propNamespace = srcProp.PropertyType.Namespace;

                if (propNamespace.StartsWith("Gva") || propNamespace.StartsWith("Common"))
                {
                    if (srcType.Name == "NomValue")
                    {
                        string destPropName = string.Format("{0}Id", srcProp.Name);
                        var nomValueId = srcPropValue != null ? srcType.GetProperty("NomValueId").GetValue(srcPropValue) : null;
                        destType.GetProperty(destPropName).SetValue(destModel, nomValueId);
                    }
                    else if (srcProp.PropertyType.GenericTypeArguments.Count() > 0 && srcPropValue != null) //process nested collection of gva class type
                    {
                        Type itemType = srcProp.PropertyType.GenericTypeArguments[0];
                        if (srcPropValue is IList<NomValue>)
                        {
                            List<int> destList = new List<int>();
                            foreach (NomValue nestedSrcNomValueProp in (IList<NomValue>)srcPropValue)
                            {
                                destList.Add(nestedSrcNomValueProp.NomValueId);
                            }
                            TrySetPropertyValue(destType, srcProp.Name, destModel, destList);
                        }
                        else
                        {
                            var destPropType = destType.GetProperty(srcProp.Name).PropertyType;
                            var nestedDestListResult = destPropType.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
                            foreach (var nestedSrcProp in (IList)srcPropValue)
                            {
                                Type nestedDestType = destPropType.GenericTypeArguments[0];
                                var nestDestProp = nestedDestType.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
                                CopyModel(nestedSrcProp, nestDestProp);
                                ((IList)nestedDestListResult).Add(nestDestProp);
                            }

                            TrySetPropertyValue(destType, srcProp.Name, destModel, nestedDestListResult);
                        }
                    }
                    else if (srcType.IsClass)//process gva class type
                    {
                        var destPropType = destType.GetProperty(srcProp.Name).PropertyType;
                        var emptyNestedDestModel = destPropType.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);

                        foreach (var nestedSrcProp in srcType.GetProperties())
                        {
                            if (srcPropValue != null)
                            {
                                CopyProperty(nestedSrcProp, srcPropValue, emptyNestedDestModel);
                            }
                            TrySetPropertyValue(destType, srcProp.Name, destModel, emptyNestedDestModel);
                        }
                    }
                }
                else
                {
                    destType.GetProperty(srcProp.Name).SetValue(destModel, srcPropValue);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
        }
    }
}
