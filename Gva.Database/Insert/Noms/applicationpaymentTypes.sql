PRINT 'applicationpaymentTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (43,N'Видове плащания по заявления',N'applicationpaymentTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8050,43,N'чл.117а(1)',N'член 117 а(1)',N'article 117 a(1)',NULL,NULL,1,N'{"dateValidFrom":"1900-01-01T00:00:00.000Z","dateValidTo":"2100-01-01T00:00:00.000Z"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8051,43,N'чл.117а(2)',N'член 117 а(2)',N'article 117 a(2)',NULL,NULL,1,N'{"dateValidFrom":"1900-01-01T00:00:00.000Z","dateValidTo":"2100-01-01T00:00:00.000Z"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8052,43,N'Чл. 121(2)',N'Чл. 121(2) - УПЛГ',N'Art. 121(2) - ARC',NULL,NULL,1,N'{"dateValidFrom":"1900-01-01T00:00:00.000Z","dateValidTo":"2100-01-01T00:00:00.000Z"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8053,43,N'чл. 117б',N'Чл. 117б - За издаване на удостоверения за одобрение на авиационни организации,',N'Art. 117b',NULL,NULL,1,N'{"dateValidFrom":"1900-01-01T00:00:00.000Z","dateValidTo":"2100-01-01T00:00:00.000Z"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8054,43,N'чл. 117в',N'чл. 117в - За издаване на удостоверения на ВС',N'Art. 117c',NULL,NULL,1,N'{"dateValidFrom":"1900-01-01T00:00:00.000Z","dateValidTo":"2100-01-01T00:00:00.000Z"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8055,43,N'чл. 117г',N'чл. 117г - За разглеждане и одобрение на техн. документация',N'Art. 117d',NULL,NULL,1,N'{"dateValidFrom":"1900-01-01T00:00:00.000Z","dateValidTo":"2100-01-01T00:00:00.000Z"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8056,43,N'чл. 117д',N'чл. 117д - За вписване на промени в Регистъра на Гражданските ВС на Р. България',N'Art. 117e',NULL,NULL,1,N'{"dateValidFrom":"1900-01-01T00:00:00.000Z","dateValidTo":"2100-01-01T00:00:00.000Z"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8057,43,N'Чл. 117e',N'Чл. 117e - За издаване на разрешения за използване на радиостанции на ВС',N'Art. 117f',NULL,NULL,1,N'{"dateValidFrom":"1900-01-01T00:00:00.000Z","dateValidTo":"2100-01-01T00:00:00.000Z"}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
