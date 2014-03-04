SET IDENTITY_INSERT [CorrespondentTypes] ON

INSERT INTO [CorrespondentTypes]
    ([CorrespondentTypeId], [Name]                         , [Alias]              , [IsActive])
VALUES
    (1                    , N'Български гражданин'         , N'BulgarianCitizen'  , 1         ),
    (2                    , N'Чужденец'                    , N'Foreigner'         , 1         ),
    (3                    , N'Юридическо лице'             , N'LegalEntity'       , 1         ),
    (4                    , N'Чуждестранно юридическо лице', N'ForeignLegalEntity', 1         )

SET IDENTITY_INSERT [CorrespondentTypes] OFF
GO
