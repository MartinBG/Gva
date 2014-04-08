INSERT INTO Noms (NomId, Name, Alias) VALUES (80,N'Типове документи',N'documentParts');
GO

INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personDocumentId'       ,N'Документ за самоличност' ,NULL,1,N'personDocumentId'       ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personEducation'        ,N'Образования'             ,NULL,1,N'personEducation'        ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personEmployment'       ,N'Месторабота'             ,NULL,1,N'personEmployment'       ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personMedical'          ,N'Медицински'              ,NULL,1,N'personMedical'          ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personCheck'            ,N'Проверка'                ,NULL,1,N'personCheck'            ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personTraining'         ,N'Обучение'                ,NULL,1,N'personTraining'         ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personOther'            ,N'Друг документ'           ,NULL,1,N'personOther'            ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personApplication'      ,N'Заявление'               ,NULL,1,N'personApplication'      ,1,NULL);

INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'organizationOther'      ,N'Друг документ'           ,NULL,2,N'organizationOther'      ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'organizationApplication',N'Заявление'               ,NULL,2,N'organizationApplication',1,NULL);

INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'aircraftOwner'          ,N'Свързано лице'           ,NULL,3,N'aircraftOwner'          ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'aircraftDebtFM'         ,N'Залог/запор'             ,NULL,3,N'aircraftDebtFM'         ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'aircraftInspection'     ,N'Инспекция'               ,NULL,3,N'aircraftInspection'     ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'aircraftOccurrence'     ,N'Инцидент'                ,NULL,3,N'aircraftOccurrence'     ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'aircraftApplication'    ,N'Заявление'               ,NULL,3,N'aircraftApplication'    ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'aircraftOther'          ,N'Друг документ'           ,NULL,3,N'aircraftOther'          ,1,NULL);
GO
