DECLARE @nomId INT = (select nomId from Noms where alias like 'documentRoles')
INSERT INTO [NomValues]
    ([NomId], [Code], [Name]                   , [NameAlt]     ,[ParentValueId], [Alias]             , [IsActive], [TextContent])
VALUES
    (@nomId , NULL , N'Образование'            , N'education'  , NULL          , N'personEducation'  , 1         , '{"caseTypeAlias": null,"isPersonsOnly": true,"categoryAlias": "system"}'),
    (@nomId , NULL , N'Месторабота'            , N'employment' , NULL          , N'personEmployment' , 1         , '{"caseTypeAlias": null,"isPersonsOnly": true,"categoryAlias": "system"}'),
    (@nomId , NULL , N'Документ за самоличност', N'documentId' , NULL          , N'personDocumentId' , 1         , '{"caseTypeAlias": null,"isPersonsOnly": true,"categoryAlias": "system"}'),
    (@nomId , NULL , N'Лиценз'                 , N'licence'    , NULL          , N'personLicence'    , 1         , '{"caseTypeAlias": null,"isPersonsOnly": true,"categoryAlias": "system"}'),
    (@nomId , NULL , N'Медицинско свидетелство', N'medical'    , NULL          , N'personMedical'    , 1         , '{"caseTypeAlias": null,"isPersonsOnly": true,"categoryAlias": "system"}'),
    (@nomId , NULL , N'Заявление              ', N'application', NULL          , N'personApplication', 1         , '{"caseTypeAlias": null,"isPersonsOnly": true,"categoryAlias": "system"}'),
    (@nomId , NULL , N'Отчет'                  , N'report'     , NULL          , N'personReport'     , 1         , '{"caseTypeAlias": null,"isPersonsOnly": true,"categoryAlias": "system"}'),
    (@nomId , NULL , N'Състояние'              , N'status'     , NULL          , N'personStatus'     , 1         , '{"caseTypeAlias": null,"isPersonsOnly": true,"categoryAlias": "system"}')
GO
