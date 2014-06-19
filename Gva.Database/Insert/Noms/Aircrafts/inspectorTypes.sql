GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Тип инспектор', N'inspectorTypes')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]       , [NameAlt]    , [ParentValueId], [Alias]     , [IsActive])
VALUES
    (@nomId , N'I'  , N'Инспектор ГВА', N'Inspector CAA', NULL           , N'gvaInspector', 1         ),
    (@nomId , N'E'  , N'Проверяващ', N'Examiner', NULL           , N'examiner', 1         ),
    (@nomId , N'O'  , N'Друг', N'Other', NULL           , N'other', 1         )
GO