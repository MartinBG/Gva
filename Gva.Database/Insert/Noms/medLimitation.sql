PRINT 'medLimitation'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (34,N'Ограничения за медицинска годност',N'medLimitation');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7828,34,N'OSL',N'OSL',N'OSL',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7829,34,N'OFL',N'OFL',N'OFL',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7830,34,N'VML',N'VML',N'VML',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7831,34,N'OCL',N'OCL',N'OCL',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7832,34,N'VDL',N'VDL',N'VDL',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7833,34,N'TML',N'TML',N'TML',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7834,34,N'VNL',N'VNL',N'VNL',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7835,34,N'OML',N'OML',N'OML',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7836,34,N'MCL',N'MCL',N'MCL',NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
