﻿INSERT INTO Noms (NomId, Name, Alias) VALUES (81,N'Да/Не номенклатура',N'boolean');
GO

INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(81,N'Y',N'Да',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(81,N'N',N'Не',NULL,NULL,NULL,1,NULL);
GO