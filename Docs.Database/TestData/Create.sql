---------------------------------------------------------------
-- Noms
---------------------------------------------------------------

:r $(rootPath)\"Noms\registers.sql"
:r $(rootPath)\"Noms\mosv.sql"

---------------------------------------------------------------
--Insert Mosv EXCEL CONFIG
---------------------------------------------------------------
:r $(rootPath)\"ExcelConfig\DocTypeGroups.sql"
:r $(rootPath)\"ExcelConfig\ElectronicServiceProviders.sql"
:r $(rootPath)\"ExcelConfig\Units.sql"
:r $(rootPath)\"ExcelConfig\UnitRelations.sql"
:r $(rootPath)\"ExcelConfig\RegisterIndexes.sql"
:r $(rootPath)\"ExcelConfig\DocTypes.sql"
:r $(rootPath)\"ExcelConfig\DocFileTypes.sql"
:r $(rootPath)\"ExcelConfig\IrregularityTypes.sql"
:r $(rootPath)\"ExcelConfig\Classifications.sql"
:r $(rootPath)\"ExcelConfig\ClassificationRelations.sql"
:r $(rootPath)\"ExcelConfig\UnitClassifications.sql"
:r $(rootPath)\"ExcelConfig\DocTypeUnitRoles.sql"
:r $(rootPath)\"ExcelConfig\DocTypeClassifications.sql"
:r $(rootPath)\"ExcelConfig\ElectronicServiceStages.sql"
:r $(rootPath)\"ExcelConfig\ElectronicServiceStageExecutors.sql"
:r $(rootPath)\"ExcelConfig\Users.sql"

:r $(rootPath)\"ConfigFinalize.sql"

--:r $(rootPath)\"MosvCorrespondents.sql"