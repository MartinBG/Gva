﻿GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Номенклатура Въздухоплавателни услуги', N'aircarrierServices')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]                , [Name]                , [NameAlt], [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'Превозът на пътници', N'Превозът на пътници', NULL     , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'Превозът на багаж'  , N'Превозът на багаж'  , NULL     , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'Санитарни цели'     , N'Санитарни цели'     , NULL     , NULL           , NULL   , 1         , NULL         )
GO
