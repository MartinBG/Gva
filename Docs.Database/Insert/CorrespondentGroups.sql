SET IDENTITY_INSERT [CorrespondentGroups] ON

INSERT INTO [CorrespondentGroups]
    ([CorrespondentGroupId], [Name]              , [Alias]      , [IsActive])
VALUES
    (1                     , N'Министерски съвет', N''          , 1         ),
    (2                     , N'Заявители'        , N'Applicants', 1         ),
    (3                     , N'Системни'         , N'System'    , 1         )

SET IDENTITY_INSERT [CorrespondentGroups] OFF
GO
