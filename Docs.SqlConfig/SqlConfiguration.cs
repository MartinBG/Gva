using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Docs.SqlConfig
{
    public class TableInfo
    {
        public string TableName { get; set; }
        public string IdColumn { get; set; }
        public string[] AllColumns { get; set; }
        public string[] StringColumns { get; set; }
        public string SQLFormat { get; set; }
        public bool HasIdentityInsert { get; set; }
        public bool HasIdValues { get; set; }
        public bool SetEmptyStringWhenValueMissing { get; set; }
    }

    public class ElectronicServiceStageRow
    {
        public string ElectronicServiceProvider { get; set; }
        public string ElectronicServiceTypeApplication { get; set; }
        public string No { get; set; }
        public string IsFirstByDefault { get; set; }
        public string IsLastStage { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Duration { get; set; }
        public string IsDurationReset { get; set; }
        public string ExecutorCode { get; set; }
        public string ExecutorName { get; set; }
        public string ExecutorUser { get; set; }
    }

    public class SqlConfiguration
    {
        private List<TableInfo> TABLES_INFO = new List<TableInfo>
        {
            new TableInfo
            {
                TableName = "RegisterIndexes",
                IdColumn = "RegisterIndexeId",
                AllColumns = new string[] { "Code", "Alias", "IsActive", "NumberFormat", "Name" },
                StringColumns = new string[] { "Code", "Alias", "NumberFormat", "Name" },
                SQLFormat = "INSERT INTO [RegisterIndexes]([RegisterIndexId], [Code], [Alias], [IsActive], [NumberFormat], [Name]) VALUES({0},{1},{2},{3},{4},{5});",
                HasIdentityInsert = true,
                HasIdValues = false
            },
            new TableInfo
            {
                TableName = "DocTypeGroups",
                IdColumn = "DocTypeGroupId",
                AllColumns = new string[] { "DocTypeGroupId", "Name", "IsElectronicService", "IsActive"},
                StringColumns = new string[] { "Name"},
                SQLFormat = "INSERT INTO [DocTypeGroups]([DocTypeGroupId], [Name], [IsElectronicService], [IsActive]) VALUES({0},{1},{2},{3});",
                HasIdentityInsert = true,
                HasIdValues = true
            },
            new TableInfo
            {
                TableName = "DocTypeUnitRoles",
                IdColumn = "DocTypeUnitRoleId",
                AllColumns = new string[] { "DocTypeUnitRoleId", "DocTypeId", "DocDirectionId", "DocUnitRoleId", "UnitId", "IsActive" },
                StringColumns = new string[] { },
                SQLFormat = "INSERT INTO [DocTypeUnitRoles]([DocTypeUnitRoleId], [DocTypeId], [DocDirectionId], [DocUnitRoleId], [UnitId],	[IsActive]) VALUES({0},{1},{2},{3},{4},{5});",
                HasIdentityInsert = true,
                HasIdValues = false
            },
            new TableInfo
            {
                TableName = "DocFileTypes",
                IdColumn = "DocFileTypeId",
                AllColumns = new string[] { "DocFileTypeId", "Name", "Alias", "DocTypeUri", "HasEmbeddedUri",},
                StringColumns = new string[] { "Name", "Alias", "DocTypeUri",},
                SQLFormat = "INSERT INTO [DocFileTypes]([DocFileTypeId], [Name], [Alias], [DocTypeUri], [HasEmbeddedUri], [MimeType], [Extention], [IsEditable], [IsActive]) VALUES({0},{1},{2},{3},{4},'application/xml','.xml',1,1);",
                HasIdentityInsert = true,
                HasIdValues = true
            },
            new TableInfo
            {
                TableName = "ElectronicServiceProviders",
                IdColumn = "ElectronicServiceProviderId",
                AllColumns = new string[] { "ElectronicServiceProviderId", "Code", "Name", "Bulstat", "Alias", "IsActive"},
                StringColumns = new string[] { "Code", "Name", "Bulstat", "Alias" },
                SQLFormat = "INSERT INTO [ElectronicServiceProviders]([ElectronicServiceProviderId], [Code], [Name], [Bulstat], [Alias], [IsActive]) VALUES({0},{1},{2},{3},{4},{5});",
                HasIdentityInsert = true,
                HasIdValues = true,
                SetEmptyStringWhenValueMissing = true
            }
        };

        private Dictionary<string, string> DocDirectionsDictionary = new Dictionary<string, string>()
        {
            { "1", "Входящ" },
            { "2", "Вътрешен" },
            { "3", "Изходящ" },
            { "4", "Циркулярен" },
        };

        private Dictionary<string, string> DocUnitRolesDictionary = new Dictionary<string, string>()
        {
            { "1", "From" },
            { "2", "To" },
            { "3", "ImportedBy" },
            { "4", "MadeBy" },
            { "5", "CCopy" },
            { "6", "InCharge" },
            { "7", "Controlling" },
            { "8", "Readers" },
            { "9", "Editors" },
            { "10", "Registrators" },
            { "11", "PersonInCharge" },
        };

        private Dictionary<string, string> ClassificationPermissionsDictionary = new Dictionary<string, string>()
        {
            { "1", "Read" },
            { "2", "Edit" },
            { "3", "Register" },
            { "4", "Management" },
            { "5", "ESign" },
            { "6", "Finish" },
            { "7", "Reverse" },
            { "8", "SubstituteManagement" },
            { "9", "DeleteManagement" },
            { "10", "EditTech" },
            { "11", "EditTechElectronicServiceStage" },
            { "12", "DocCasePartManagement" }
        };

        private Dictionary<string, Tuple<int, int>> CodeClassificationIdRelations = null;
        private Dictionary<string, Tuple<int, int>> CodeUnitIdRelations = null;
        private Dictionary<string, string> DocTypeGroupsDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> RegisterIndexesDictionary = new Dictionary<string, string>();
        private Dictionary<string, Tuple<string, string>> DocTypeElectronicServiceRelations = new Dictionary<string, Tuple<string, string>>();
        private Dictionary<string, string> UnitsUnitTypeIdDictionary = new Dictionary<string, string>();

        private List<Tuple<string, string>> DocTypesAndUnitsWithRoleTo = new List<Tuple<string, string>>();
        private List<Tuple<string, string>> AllDocTypesAndUnits = new List<Tuple<string, string>>();

        private Dictionary<string, Tuple<string, string>> DocTypeAndClassification2Dictionary = new Dictionary<string, Tuple<string, string>>();
        private Dictionary<string, Tuple<string, string>> DocTypeAndDocTypeGroupsDictionary = new Dictionary<string, Tuple<string, string>>();

        private void AddRow_UnitsAndUnitRelations(List<string> unitsTableRows, List<string> unitRelationsTableRows, List<string> usersTableRows, Dictionary<string, Tuple<int, int>> codeUnitIdRelations, ref int unitsIdentity, ref int unitRelationsIdentity, Dictionary<string, string> row)
        {
            if (row.ContainsKey("LevelKey") && !String.IsNullOrWhiteSpace(row["LevelKey"]))    //row is not empty
            {
                string code = row["LevelKey"].Trim();
                string name = row["Name"].Trim();
                string user = row["User"].Trim();
                string email = row["Email"].Trim();

                int? parentId = null;
                int rootId = unitsIdentity;

                if (code.Length > 2)
                {
                    var pair = codeUnitIdRelations.Where(r => r.Key == code.Substring(0, code.Length - 2)).SingleOrDefault();

                    parentId = pair.Value.Item1;
                    rootId = pair.Value.Item2;
                }

                if (String.IsNullOrWhiteSpace(row["User"]))
                {
                    unitsTableRows.Add(String.Format("INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES({0},N'{1}',{2},{3},{4});", unitsIdentity, name, 1, 0, 1));
                    unitRelationsTableRows.Add(String.Format("INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES({0},{1},{2},{3});", unitRelationsIdentity, unitsIdentity, parentId.HasValue ? parentId.Value.ToString() : "NULL", rootId));

                    codeUnitIdRelations.Add(code, new Tuple<int, int>(unitsIdentity, rootId));
                    UnitsUnitTypeIdDictionary.Add(unitsIdentity.ToString(), "1");

                    unitsIdentity++;
                    unitRelationsIdentity++;
                }
                else
                {
                    unitsTableRows.Add(String.Format("INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES({0},N'{1}',{2},{3},{4});", unitsIdentity, name, 2, 0, 1));
                    unitRelationsTableRows.Add(String.Format("INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES({0},{1},{2},{3});", unitRelationsIdentity, unitsIdentity, parentId.HasValue ? parentId.Value.ToString() : "NULL", rootId));

                    codeUnitIdRelations.Add(code, new Tuple<int, int>(unitsIdentity, rootId));
                    UnitsUnitTypeIdDictionary.Add(unitsIdentity.ToString(), "3");

                    unitsIdentity++;
                    unitRelationsIdentity++;

                    unitsTableRows.Add(String.Format("INSERT INTO [Units]([UnitId],[Name],[UnitTypeId],[InheritParentClassification],[IsActive])VALUES({0},N'{1}',{2},{3},{4});", unitsIdentity, user, 3, 0, 1));
                    unitRelationsTableRows.Add(String.Format("INSERT INTO [UnitRelations]([UnitRelationId],[UnitId],[ParentUnitId],[RootUnitId])VALUES({0},{1},{2},{3});", unitRelationsIdentity, unitsIdentity, unitsIdentity - 1, rootId));

                    codeUnitIdRelations.Add(code + "01", new Tuple<int, int>(unitsIdentity, rootId));
                    UnitsUnitTypeIdDictionary.Add(unitsIdentity.ToString(), "2");

                    unitsIdentity++;
                    unitRelationsIdentity++;
                }

                if (!String.IsNullOrWhiteSpace(email))
                {
                    string username = email.Substring(0, email.IndexOf('@')).Trim();

                    usersTableRows.Add(String.Format(@"Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', 1, N'{6}', {8})
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), {7}, {8}",
                        username,
                        "AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==",
                        "+FoFiIwx7qMV3ROW7PxWgw==",
                        user,
                        "",
                        "",
                        email,
                        unitsIdentity - 1,
                        "1"));
                }
            }
        }

        private void AddRow_ClassificationsAndClassificationRelations(List<string> classificationTableRows, List<string> classificataionRelationsTableRows, Dictionary<string, Tuple<int, int>> codeClassificationIdRelations, ref int classificationsIdentity, ref int classificationRelationsIdentity, Dictionary<string, string> row)
        {
            string levelKey = row["LevelKey"].Trim();
            string name = row["Name"].Trim();
            string alias = row["Alias"].Trim();

            int? parentId = null;
            int rootId = classificationsIdentity;

            if (levelKey.Length > 2)
            {
                var pair = codeClassificationIdRelations.Where(r => r.Key == levelKey.Substring(0, levelKey.Length - 2)).SingleOrDefault();

                parentId = pair.Value.Item1;
                rootId = pair.Value.Item2;
            }

            classificationTableRows.Add(String.Format("INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES({0},N'{1}',N'{2}',{3});", classificationsIdentity, name, alias, 1));
            classificataionRelationsTableRows.Add(String.Format("INSERT INTO [ClassificationRelations]([ClassificationRelationId],[ClassificationId],[ParentClassificationId],[RootClassificationId])VALUES({0},{1},{2},{3});", classificationRelationsIdentity, classificationsIdentity, parentId.HasValue ? parentId.Value.ToString() : "NULL", rootId));

            codeClassificationIdRelations.Add(levelKey, new Tuple<int, int>(classificationsIdentity, rootId));

            classificationsIdentity++;
            classificationRelationsIdentity++;

        }

        private void AddRow_DocTypesAndDocTypeClassifications(List<string> docTypesTableRows, List<string> docTypeClassificationsTableRows, ref int identityValue, ref int docTypeClassificationsIdentity, Dictionary<string, string> row)
        {
            if (row.ContainsKey("DocTypeGroup") && !String.IsNullOrWhiteSpace(row["DocTypeGroup"]))    //row is not empty
            {
                string docTypeGroup = row["DocTypeGroup"].Trim();
                string docDirection = row["DocDirection"].Trim();
                string isElectronicService = row["IsElectronicService"].Trim();
                string electronicServiceProvider = row["ElectronicServiceProvider"].Trim();
                string pRegisterIndex = row["PRegisterIndex"].Trim();
                string sRegisterIndex = row["SRegisterIndex"].Trim();
                string classifications0 = (row.ContainsKey("Classifications0") && !String.IsNullOrWhiteSpace(row["Classifications0"])) ? row["Classifications0"].Trim() : String.Empty;
                string classifications1 = (row.ContainsKey("Classifications1") && !String.IsNullOrWhiteSpace(row["Classifications1"])) ? row["Classifications1"].Trim() : String.Empty;
                string classifications2 = (row.ContainsKey("Classifications2") && !String.IsNullOrWhiteSpace(row["Classifications2"])) ? row["Classifications2"].Trim() : String.Empty;
                string alias = (row.ContainsKey("Alias") && !String.IsNullOrWhiteSpace(row["Alias"])) ? row["Alias"].Trim() : String.Empty;
                string electronicServiceFileTypeUri = (row.ContainsKey("ElectronicServiceFileTypeUri") && !String.IsNullOrWhiteSpace(row["ElectronicServiceFileTypeUri"])) ? row["ElectronicServiceFileTypeUri"].Trim() : String.Empty;
                string electronicServiceTypeApplication = (row.ContainsKey("ElectronicServiceTypeApplication") && !String.IsNullOrWhiteSpace(row["ElectronicServiceTypeApplication"])) ? row["ElectronicServiceTypeApplication"].Trim() : String.Empty;
                string name = row["Name"].Trim();
                string executionDeadline = (row.ContainsKey("ExecutionDeadline") && !String.IsNullOrWhiteSpace(row["ExecutionDeadline"])) ? row["ExecutionDeadline"].Trim() : String.Empty;
                string removeIrregularitiesDeadline = (row.ContainsKey("RemoveIrregularitiesDeadline") && !String.IsNullOrWhiteSpace(row["RemoveIrregularitiesDeadline"])) ? row["RemoveIrregularitiesDeadline"].Trim() : String.Empty;
                string isActive = row["IsActive"].Trim();

                string docTypeGroupId = DocTypeGroupsDictionary.Where(g => g.Key.ToLower() == docTypeGroup.ToLower()).Select(g => g.Value).FirstOrDefault();

                string docDirectionId = DocDirectionsDictionary.Where(d => d.Value.ToLower() == docDirection.ToLower()).Select(g => g.Key).FirstOrDefault();

                string classificationsId0 = CodeClassificationIdRelations.Where(r => r.Key == classifications0).Any() ?
                    CodeClassificationIdRelations.Where(r => r.Key == classifications0).Select(r => r.Value.Item1).FirstOrDefault().ToString() : null;
                string classificationsId1 = CodeClassificationIdRelations.Where(r => r.Key == classifications1).Any() ?
                    CodeClassificationIdRelations.Where(r => r.Key == classifications1).Select(r => r.Value.Item1).FirstOrDefault().ToString() : null;
                string classificationsId2 = CodeClassificationIdRelations.Where(r => r.Key == classifications2).Any() ?
                    CodeClassificationIdRelations.Where(r => r.Key == classifications2).Select(r => r.Value.Item1).FirstOrDefault().ToString() : null;

                string pRegisterIndexId = null;
                if (!String.IsNullOrWhiteSpace(pRegisterIndex))
                {
                    pRegisterIndexId = RegisterIndexesDictionary.Where(i => i.Value == pRegisterIndex).Single().Key;
                }

                string sRegisterIndexId = null;
                if (!String.IsNullOrWhiteSpace(sRegisterIndex))
                {
                    sRegisterIndexId = RegisterIndexesDictionary.Where(i => i.Value == sRegisterIndex).Single().Key;
                }

                docTypesTableRows.Add(String.Format("INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES({0},{1},{2},{3},N'{4}',N'{5}',{6},N'{7}',N'{8}',N'{9}',{10},{11},{12});",
                    identityValue,//[DocTypeId],
                    docTypeGroupId,//[DocTypeGroupId],
                    !String.IsNullOrWhiteSpace(pRegisterIndexId) ? pRegisterIndexId : "NULL",//[PrimaryRegisterIndexId],
                    !String.IsNullOrWhiteSpace(sRegisterIndexId) ? sRegisterIndexId : "NULL",//[SecondaryRegisterIndexId],
                    name,//[Name],
                    alias,//[Alias],
                    isElectronicService,//[IsElectronicService],
                    electronicServiceFileTypeUri,//[ElectronicServiceFileTypeUri],
                    electronicServiceTypeApplication,//[ElectronicServiceTypeApplication],
                    electronicServiceProvider,//[ElectronicServiceProvider],
                    !String.IsNullOrWhiteSpace(executionDeadline) ? executionDeadline : "NULL",//[ExecutionDeadline],
                    !String.IsNullOrWhiteSpace(removeIrregularitiesDeadline) ? removeIrregularitiesDeadline : "NULL",//[RemoveIrregularitiesDeadline],
                    isActive//[IsActive]
                    ));

                if (!String.IsNullOrWhiteSpace(classifications0))
                {
                    docTypeClassificationsTableRows.Add(String.Format("INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES({0},{1},{2},{3},{4});", docTypeClassificationsIdentity, identityValue, docDirectionId, classificationsId0, "1"));
                    docTypeClassificationsIdentity++;
                }

                if (!String.IsNullOrWhiteSpace(classifications1))
                {
                    docTypeClassificationsTableRows.Add(String.Format("INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES({0},{1},{2},{3},{4});", docTypeClassificationsIdentity, identityValue, docDirectionId, classificationsId1, "1"));
                    docTypeClassificationsIdentity++;
                }

                if (!String.IsNullOrWhiteSpace(classifications2))
                {
                    docTypeClassificationsTableRows.Add(String.Format("INSERT INTO [DocTypeClassifications]([DocTypeClassificationId],[DocTypeId],[DocDirectionId],[ClassificationId],[IsActive])VALUES({0},{1},{2},{3},{4});", docTypeClassificationsIdentity, identityValue, docDirectionId, classificationsId2, "1"));
                    docTypeClassificationsIdentity++;
                }


                DocTypeElectronicServiceRelations.Add(identityValue.ToString(), new Tuple<string, string>(electronicServiceTypeApplication, electronicServiceProvider));

                DocTypeAndClassification2Dictionary.Add(identityValue.ToString(), new Tuple<string, string>(classificationsId2, isElectronicService));

                DocTypeAndDocTypeGroupsDictionary.Add(identityValue.ToString(), new Tuple<string, string>(docTypeGroupId, isElectronicService));

                identityValue++;
            }
        }

        private void GetAllElectronicServiceStages(List<ElectronicServiceStageRow> allElectronicServiceStages, Dictionary<string, string> row)
        {
            ElectronicServiceStageRow stageRow = new ElectronicServiceStageRow();

            stageRow.ElectronicServiceProvider = row["ElectronicServiceProvider"].Trim();
            stageRow.ElectronicServiceTypeApplication = row["ElectronicServiceTypeApplication"].Trim();
            stageRow.No = row["No"].Trim();
            stageRow.IsFirstByDefault = row["IsFirstByDefault"].Trim();
            stageRow.IsLastStage = row["IsLastStage"].Trim();
            stageRow.Name = row["Name"].Trim();
            stageRow.Alias = row["Alias"].Trim();
            stageRow.Duration = row["Duration"].Trim();
            stageRow.IsDurationReset = row["IsDurationReset"].Trim();
            stageRow.ExecutorCode = row["ExecutorCode"].Trim();
            stageRow.ExecutorName = row["ExecutorName"].Trim();
            stageRow.ExecutorUser = row["ExecutorUser"].Trim();

            allElectronicServiceStages.Add(stageRow);
        }

        private void AddRow_ElectronicServiceStagesAndExecutors(List<string> electronicServiceStagesTableRows, List<string> electronicServiceStageExecutorsTableRows, List<ElectronicServiceStageRow> allElectronicServiceStages, string docTypeId, ref int electronicServiceStageExecutorsIdentity, ref int identityValue)
        {
            foreach (var stageRow in allElectronicServiceStages)
            {
                electronicServiceStagesTableRows.Add(String.Format("INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES({0},{1},N'{2}',{3},N'{4}',{5},{6},{7},{8},{9});",
                    identityValue,//[ElectronicServiceStageId]
                    docTypeId,//,[DocTypeId]
                    stageRow.Name,//,[Name]
                    "NULL",//,[Description]
                    stageRow.Alias,//,[Alias]
                    stageRow.Duration,//,[Duration]
                    stageRow.IsDurationReset,//,[IsDurationReset]
                    stageRow.IsFirstByDefault,//,[IsFirstByDefault]
                    stageRow.IsLastStage,//,[IsLastStage]
                    "1"//,[IsActive]
                    ));

                string actualUnitCode = "0503";
                string unitId = CodeUnitIdRelations.Where(r => r.Key == actualUnitCode).Select(r => r.Value.Item1).FirstOrDefault().ToString();

                electronicServiceStageExecutorsTableRows.Add(String.Format("INSERT INTO [ElectronicServiceStageExecutors]([ElectronicServiceStageExecutorId],[ElectronicServiceStageId],[UnitId],[IsActive])VALUES({0},{1},{2},{3});", electronicServiceStageExecutorsIdentity, identityValue, unitId, "1"));
                electronicServiceStageExecutorsIdentity++;

                identityValue++;
            }
        }

        private void GetAllIrregularityTypes(List<string> allIrregularityTypes, Dictionary<string, string> row)
        {
            string electronicServiceProvider = row["ElectronicServiceProvider"].Trim();
            string electronicServiceTypeApplication = row["ElectronicServiceTypeApplication"].Trim();
            string name = row["Name"].Trim();

            allIrregularityTypes.Add(name);
        }

        private void AddRow_IrregularityTypes(List<string> irregularityTypesTableRows, List<string> allIrregularityTypes, string docTypeId, ref int identityValue)
        {
            foreach (var irregularityName in allIrregularityTypes)
            {
                irregularityTypesTableRows.Add(String.Format("INSERT INTO [IrregularityTypes]([IrregularityTypeId],[DocTypeId],[Name],[Alias],[Description])VALUES({0},{1},N'{2}',{3},{4});",
                    identityValue, docTypeId, irregularityName, "NULL", "NULL"));

                identityValue++;
            }
        }

        private void AddRow_DocTypeUnitRoles(List<string> docTypeUnitRolesTableRows, Dictionary<string, string> row, ref int identityValue)
        {
            string electronicServiceProvider = row["ElectronicServiceProvider"].Trim();
            string electronicServiceTypeApplication = row["ElectronicServiceTypeApplication"].Trim();
            string docDirection = row["DocDirection"].Trim();
            string docUnitRole = row["DocUnitRole"].Trim();
            string unitCode = row["UnitCode"].Trim();
            string unitName = row["UnitName"].Trim();
            string unitUser = row["UnitUser"].Trim();

            if (!String.IsNullOrWhiteSpace(unitCode))
            {

                string docTypeId = DocTypeElectronicServiceRelations.Where(r => r.Value.Item1 == electronicServiceTypeApplication && r.Value.Item2 == electronicServiceProvider).Select(r => r.Key).SingleOrDefault();

                string docDirectionId = DocDirectionsDictionary.Where(d => d.Value.ToLower() == docDirection.ToLower()).Select(g => g.Key).FirstOrDefault();

                string actualUnitCode = null;
                if (String.IsNullOrWhiteSpace(unitUser))
                {
                    actualUnitCode = unitCode;
                }
                else
                {
                    actualUnitCode = unitCode + "01";
                }

                string unitId = CodeUnitIdRelations.Where(r => r.Key == actualUnitCode).Select(r => r.Value.Item1).FirstOrDefault().ToString();

                string docUnitRoleId = DocUnitRolesDictionary.Where(d => d.Value.ToLower() == docUnitRole.ToLower()).Select(g => g.Key).FirstOrDefault();

                docTypeUnitRolesTableRows.Add(String.Format("INSERT INTO [DocTypeUnitRoles]([DocTypeUnitRoleId],[DocTypeId],[DocDirectionId],[DocUnitRoleId],[UnitId],[IsActive])VALUES({0},{1},{2},{3},{4}, {5});", identityValue, docTypeId, docDirectionId, docUnitRoleId, unitId, "1"));

                identityValue++;

                AllDocTypesAndUnits.Add(new Tuple<string, string>(docTypeId, unitId));
                if (row["DocUnitRole"].Trim().ToLower() == "to")
                {

                    //if (unitId == "453" && docTypeId)
                    //{
                    //}
                    DocTypesAndUnitsWithRoleTo.Add(new Tuple<string, string>(docTypeId, unitId));
                }
            }
        }

        private void AddRow_UnitClassifications(List<string> unitClassificationsTableRows, Dictionary<string, string> row, ref int identityValue)
        {
            if (!row.ContainsKey("ClassificationCode") || String.IsNullOrWhiteSpace(row["ClassificationCode"]))
                return;

            string classificationCode = row["ClassificationCode"].Trim();
            string classificationName = row["ClassificationName"].Trim();
            string classificationPermission = row["ClassificationPermission"].Trim();
            string unitCode = row["UnitCode"].Trim();
            string unitName = row["UnitName"].Trim();
            string unitUser = row["UnitUser"].Trim();

            string classificationsId = CodeClassificationIdRelations.Where(r => r.Key == classificationCode).Select(r => r.Value.Item1).FirstOrDefault().ToString();

            string actualUnitCode = null;
            if (String.IsNullOrWhiteSpace(unitUser))
            {
                actualUnitCode = unitCode;
            }
            else
            {
                actualUnitCode = unitCode + "01";
            }

            string unitId = CodeUnitIdRelations.Where(r => r.Key == unitCode).Select(r => r.Value.Item1).FirstOrDefault().ToString();

            string classificationPermissionId = ClassificationPermissionsDictionary.Where(d => d.Value.ToLower() == classificationPermission.ToLower()).Select(g => g.Key).FirstOrDefault();

            unitClassificationsTableRows.Add(String.Format("INSERT INTO [UnitClassifications]([UnitClassificationId],[UnitId],[ClassificationId],[ClassificationPermissionId])VALUES({0},{1},{2},{3});", identityValue, unitId, classificationsId, classificationPermissionId));

            identityValue++;
        }

        private void AddRow_UnitClassificationsGeneratedFromDocTypeUnits(List<string> unitClassificationsTableRows, ref int identityValue)
        {
            List<Tuple<string, string>> insertRows = new List<Tuple<string, string>>();

            foreach (var item in DocTypesAndUnitsWithRoleTo)
            {
                string classificationId = DocTypeAndClassification2Dictionary.Where(d => d.Key == item.Item1).Select(d => d.Value.Item1).Single();

                if (!String.IsNullOrWhiteSpace(classificationId))
                {
                    if (!insertRows.Where(e => e.Item1 == item.Item2 && e.Item2 == classificationId).Any())
                    {
                        insertRows.Add(new Tuple<string, string>(item.Item2, classificationId));

                        string roleId_Read = ClassificationPermissionsDictionary.Where(d => d.Value.ToLower() == "read").Select(g => g.Key).FirstOrDefault();
                        string roleId_ESign = ClassificationPermissionsDictionary.Where(d => d.Value.ToLower() == "esign").Select(g => g.Key).FirstOrDefault();
                        string roleId_Finish = ClassificationPermissionsDictionary.Where(d => d.Value.ToLower() == "finish").Select(g => g.Key).FirstOrDefault();
                        string roleId_Reverse = ClassificationPermissionsDictionary.Where(d => d.Value.ToLower() == "reverse").Select(g => g.Key).FirstOrDefault();

                        unitClassificationsTableRows.Add(String.Format("INSERT INTO [UnitClassifications]([UnitClassificationId],[UnitId],[ClassificationId],[ClassificationPermissionId])VALUES({0},{1},{2},{3});",
                            identityValue, item.Item2, classificationId, roleId_Read));
                        identityValue++;


                        unitClassificationsTableRows.Add(String.Format("INSERT INTO [UnitClassifications]([UnitClassificationId],[UnitId],[ClassificationId],[ClassificationPermissionId])VALUES({0},{1},{2},{3});",
                            identityValue, item.Item2, classificationId, roleId_ESign));
                        identityValue++;

                        unitClassificationsTableRows.Add(String.Format("INSERT INTO [UnitClassifications]([UnitClassificationId],[UnitId],[ClassificationId],[ClassificationPermissionId])VALUES({0},{1},{2},{3});",
                            identityValue, item.Item2, classificationId, roleId_Finish));
                        identityValue++;

                        unitClassificationsTableRows.Add(String.Format("INSERT INTO [UnitClassifications]([UnitClassificationId],[UnitId],[ClassificationId],[ClassificationPermissionId])VALUES({0},{1},{2},{3});",
                            identityValue, item.Item2, classificationId, roleId_Reverse));
                        identityValue++;
                    }
                }
            }
        }

        private void AddRow_StandartTable(TableInfo tableInfo, List<string> tableRows, Dictionary<string, string> row, ref int identityValue)
        {
            List<string> parameters = new List<string>();

            bool allColumnsAreEmpty = true;
            foreach (var column in tableInfo.AllColumns)
            {
                string columnValue = row.ContainsKey(column) ? row[column] : "";
                bool isEmpty = string.IsNullOrWhiteSpace(columnValue);
                allColumnsAreEmpty &= isEmpty;
            }

            if (!allColumnsAreEmpty)
            {
                if (!tableInfo.HasIdValues)
                {
                    parameters.Add(identityValue.ToString());
                }

                foreach (var column in tableInfo.AllColumns)
                {
                    string columnValue = row.ContainsKey(column) ? row[column] : "";
                    bool isEmpty = string.IsNullOrWhiteSpace(columnValue);
                    if (isEmpty)
                    {
                        if (tableInfo.StringColumns.Contains(column) && tableInfo.SetEmptyStringWhenValueMissing)
                        {
                            columnValue = "''";
                        }
                        else
                        {
                            columnValue = "NULL";
                        }
                    }
                    else if (tableInfo.StringColumns.Contains(column))
                    {
                        columnValue = string.Format("N'{0}'", columnValue);
                    }

                    parameters.Add(columnValue);
                }

                tableRows.Add(string.Format(tableInfo.SQLFormat, parameters.ToArray()));

                if (tableInfo.TableName == "DocTypeGroups")
                {
                    DocTypeGroupsDictionary.Add(parameters[1].Substring(2, parameters[1].Length - 3), parameters[0]);
                }
                else if (tableInfo.TableName == "RegisterIndexes")
                {
                    RegisterIndexesDictionary.Add(parameters[0], parameters[1].Substring(2, parameters[1].Length - 3));
                }

                identityValue++;
            }
        }

        private Tuple<string, string> GenerateStandartTable(TableInfo tableInfo, List<Dictionary<string, string>> rowsWithHeaders)
        {
            List<string> tableRows = new List<string>();

            int identityValue = 1;
            foreach (var row in rowsWithHeaders)
            {
                AddRow_StandartTable(tableInfo, tableRows, row, ref identityValue);
            }

            return Save(tableInfo.TableName, tableRows, null, tableInfo.HasIdentityInsert);
        }

        private Tuple<string, string> Save(string tableName, List<string> tableRows, List<string> additionalScriptRows, bool setIdentityInsert)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("print 'Excel Insert {0}'", tableName));
            sb.AppendLine("GO");
            sb.AppendLine();

            if (setIdentityInsert)
            {
                sb.AppendLine(string.Format("SET IDENTITY_INSERT [{0}] ON", tableName));
            }

            foreach (var row in tableRows)
            {
                sb.AppendLine(row);
            }

            if (setIdentityInsert)
            {
                sb.AppendLine(string.Format("SET IDENTITY_INSERT [{0}] OFF", tableName));
            }

            sb.AppendLine("GO");
            sb.AppendLine();

            if (additionalScriptRows != null && additionalScriptRows.Count > 0)
            {
                foreach (var row in additionalScriptRows)
                {
                    sb.AppendLine(row);
                }

                sb.AppendLine("GO");
                sb.AppendLine();
            }

            return new Tuple<string, string>(tableName + ".sql", sb.ToString());
        }

        private void AddTuple(Dictionary<string, string> dict, Tuple<string, string> t)
        {
            dict.Add(t.Item1, t.Item2);
        }

        public Dictionary<string, string> GenerateSql(string fileName)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            var xmlns = "urn:schemas-microsoft-com:office:spreadsheet";
            XDocument doc = XDocument.Load(fileName);
            var worksheets =
                doc
                .Element(XName.Get("Workbook", xmlns))
                .Elements(XName.Get("Worksheet", xmlns));

            foreach (var worksheet in worksheets)
            {
                string[] headers = null;

                IEnumerable<XElement> allRows = new List<XElement>();

                var rows =
                    worksheet
                    .Element(XName.Get("Table", xmlns))
                    .Elements(XName.Get("Row", xmlns));

                if (headers == null)
                {
                    //only the first sheet's headers are used
                    headers = rows
                        .Select(r =>
                            r.Elements(XName.Get("Cell", xmlns))
                            .Select(c => c.Element(XName.Get("Data", xmlns)).Value)
                            .ToArray())
                            .First();
                }

                allRows = allRows.Concat(rows.Skip(1));


                List<Dictionary<string, string>> rowsWithHeaders = new List<Dictionary<string, string>>();

                foreach (var row in allRows)
                {
                    int cellIndex = -1;
                    var cells = row.Elements(XName.Get("Cell", xmlns));

                    Dictionary<string, string> rowWithHeaders = new Dictionary<string, string>();
                    foreach (var cell in cells)
                    {
                        XAttribute indexAttr = cell.Attribute(XName.Get("Index", xmlns));
                        cellIndex = indexAttr != null ? (int.Parse(indexAttr.Value) - 1) : (cellIndex + 1);

                        var cellData = cell.Element(XName.Get("Data", xmlns));
                        var textValue = cellData != null ? cellData.Value : "";

                        if (cellIndex <= headers.Count() - 1)
                        {
                            rowWithHeaders.Add(headers[cellIndex], textValue);
                        }
                    }

                    rowsWithHeaders.Add(rowWithHeaders);
                }

                string sheetName = worksheet.FirstAttribute.Value;

                if (sheetName == "Units")
                {
                    List<string> unitsTableRows = new List<string>();
                    List<string> unitRelationsTableRows = new List<string>();
                    List<string> usersTableRows = new List<string>();
                    CodeUnitIdRelations = new Dictionary<string, Tuple<int, int>>();

                    int unitsIdentity = 1;
                    int unitRelationsIdentity = 1;

                    foreach (var row in rowsWithHeaders)
                    {
                        AddRow_UnitsAndUnitRelations(unitsTableRows, unitRelationsTableRows, usersTableRows, CodeUnitIdRelations, ref unitsIdentity, ref unitRelationsIdentity, row);
                    }

                    AddTuple(result, Save("Units", unitsTableRows, null, true));
                    AddTuple(result, Save("UnitRelations", unitRelationsTableRows, null, true));
                    AddTuple(result, Save("Users", usersTableRows, null, false));
                }
                else if (sheetName == "Classifications")
                {
                    List<string> classificationsTableRows = new List<string>();
                    List<string> classificationRelationsTableRows = new List<string>();
                    CodeClassificationIdRelations = new Dictionary<string, Tuple<int, int>>();

                    int classificationsIdentity = 1;
                    int classificationRelationsIdentity = 1;

                    foreach (var row in rowsWithHeaders)
                    {
                        AddRow_ClassificationsAndClassificationRelations(classificationsTableRows, classificationRelationsTableRows, CodeClassificationIdRelations, ref classificationsIdentity, ref classificationRelationsIdentity, row);
                    }

                    AddTuple(result, Save("Classifications", classificationsTableRows, null, true));
                    AddTuple(result, Save("ClassificationRelations", classificationRelationsTableRows, null, true));
                }
                else if (sheetName == "DocTypes")
                {
                    List<string> docTypesTableRows = new List<string>();
                    List<string> docTypeClassificationsTableRows = new List<string>();

                    int identityValue = 1;
                    int docTypeClassificationsIdentity = 1;

                    foreach (var row in rowsWithHeaders)
                    {
                        AddRow_DocTypesAndDocTypeClassifications(docTypesTableRows, docTypeClassificationsTableRows, ref identityValue, ref docTypeClassificationsIdentity, row);
                    }

                    AddTuple(result, Save("DocTypes", docTypesTableRows, null, true));
                    AddTuple(result, Save("DocTypeClassifications", docTypeClassificationsTableRows, null, true));
                }
                else if (sheetName == "ElectronicServiceStages")
                {
                    List<ElectronicServiceStageRow> allElectronicServiceStages = new List<ElectronicServiceStageRow>();
                    foreach (var row in rowsWithHeaders)
                    {
                        GetAllElectronicServiceStages(allElectronicServiceStages, row);
                    }

                    List<string> electronicServiceStagesTableRows = new List<string>();
                    List<string> electronicServiceStageExecutorsTableRows = new List<string>();

                    int electronicServiceStageExecutorsIdentity = 1;
                    int identityValue = 1;

                    foreach (var item in DocTypeAndDocTypeGroupsDictionary)
                    {
                        if (item.Value.Item2 == "1") //IsElectronicService
                        {
                            AddRow_ElectronicServiceStagesAndExecutors(electronicServiceStagesTableRows, electronicServiceStageExecutorsTableRows, allElectronicServiceStages, item.Key, ref electronicServiceStageExecutorsIdentity, ref identityValue);
                        }
                    }

                    AddTuple(result, Save("ElectronicServiceStages", electronicServiceStagesTableRows, null, true));
                    AddTuple(result, Save("ElectronicServiceStageExecutors", electronicServiceStageExecutorsTableRows, null, true));
                }
                else if (sheetName == "IrregularityTypes")
                {
                    List<string> allIrregularityTypes = new List<string>();
                    foreach (var row in rowsWithHeaders)
                    {
                        GetAllIrregularityTypes(allIrregularityTypes, row);
                    }


                    List<string> irregularityTypesTableRows = new List<string>();
                    int identityValue = 1;
                    foreach (var item in DocTypeAndClassification2Dictionary)
                    {
                        AddRow_IrregularityTypes(irregularityTypesTableRows, allIrregularityTypes, item.Key, ref identityValue);
                    }

                    AddTuple(result, Save("IrregularityTypes", irregularityTypesTableRows, null, true));
                }
                else if (sheetName == "DocTypeUnitRoles")
                {
                    List<string> docTypeUnitRolesTableRows = new List<string>();

                    int identityValue = 1;
                    foreach (var row in rowsWithHeaders)
                    {
                        AddRow_DocTypeUnitRoles(docTypeUnitRolesTableRows, row, ref identityValue);
                    }

                    AddTuple(result, Save("DocTypeUnitRoles", docTypeUnitRolesTableRows, null, true));
                }
                else if (sheetName == "UnitClassifications")
                {
                    List<string> unitClassificationsTableRows = new List<string>();

                    int identityValue = 1;
                    foreach (var row in rowsWithHeaders)
                    {
                        AddRow_UnitClassifications(unitClassificationsTableRows, row, ref identityValue);
                    }

                    AddRow_UnitClassificationsGeneratedFromDocTypeUnits(unitClassificationsTableRows, ref identityValue);

                    AddTuple(result, Save("UnitClassifications", unitClassificationsTableRows, null, true));
                }
                else
                {
                    var tableInfo = TABLES_INFO.Where(ti => ti.TableName == sheetName).FirstOrDefault();

                    if (tableInfo != null)
                    {
                        AddTuple(result, GenerateStandartTable(tableInfo, rowsWithHeaders));
                    }
                }
            }

            return result;
        }
    }
}
