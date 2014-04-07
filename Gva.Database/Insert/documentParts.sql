INSERT INTO Noms (NomId, Name, Alias) VALUES (80,N'Типове документи',N'documentParts');
GO

INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personDocumentId' ,N'Документ за самоличност',NULL,NULL,N'personDocumentId' ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personEducation'  ,N'Образования'            ,NULL,NULL,N'personEducation'  ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personEmployment' ,N'Месторабота'            ,NULL,NULL,N'personEmployment' ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personMedical'    ,N'Медицински'             ,NULL,NULL,N'personMedical'    ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personCheck'      ,N'Проверка'               ,NULL,NULL,N'personCheck'      ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personTraining'   ,N'Обучение'               ,NULL,NULL,N'personTraining'   ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personOther'      ,N'Друг документ'          ,NULL,NULL,N'personOther'      ,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(80,N'personApplication',N'Заявление'              ,NULL,NULL,N'personApplication',1,NULL);
GO
