PRINT 'Insert instructorExaminerCertificateAttachmentAuthorizations'
GO
INSERT [dbo].[Noms] ([Name], [Alias], [Category]) VALUES (N'Разрешения за приложение към сертификат за Проверяващ/Инструктор', N'instructorExaminerCertificateAttachmentAuthorizations', N'person')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]                 , [Name]  , [NameAlt], [ParentValueId], [Alias] , [IsActive], [TextContent])
VALUES
    (@nomId , N'CRI'                 , N'CRI'  , N'CRI'   , NULL           , N'CRI'  , 1         , NULL        ),
    (@nomId , N'FI'                  , N'FI'   , N'FI'    , NULL           , N'FI'   , 1         , NULL        ),
    (@nomId , N'FTI'                 , N'FTI'  , N'FTI'   , NULL           , N'FTI'  , 1         , NULL        ),
    (@nomId , N'IRI'                 , N'IRI'  , N'IRI'   , NULL           , N'IRI'  , 1         , NULL        ),
    (@nomId , N'MCCI'                , N'MCCI' , N'MCCI'  , NULL           , N'MCCI' , 1         , NULL        ),
    (@nomId , N'MI'                  , N'MI'   , N'MI'    , NULL           , N'MI'   , 1         , NULL        ),
    (@nomId , N'SFI'                 , N'SFI'  , N'SFI'   , NULL           , N'SFI'  , 1         , NULL        ),
    (@nomId , N'STI'                 , N'STI'  , N'STI'   , NULL           , N'STI'  , 1         , NULL        ),
    (@nomId , N'TRI'                 , N'TRI'  , N'TRI'   , NULL           , N'TRI'  , 1         , NULL        ),
    (@nomId , N'CRE'                 , N'CRE'  , N'CRE'   , NULL           , N'CRE'  , 1         , NULL        ),
    (@nomId , N'FE'                  , N'FE'   , N'FE'    , NULL           , N'FE'   , 1         , NULL        ),
    (@nomId , N'FIE'                 , N'FIE'  , N'FIE'   , NULL           , N'FIE'  , 1         , NULL        ),
    (@nomId , N'IRE'                 , N'IRE'  , N'IRE'   , NULL           , N'IRE'  , 1         , NULL        ),
    (@nomId , N'SFE'                 , N'SFE'  , N'SFE'   , NULL           , N'SFE'  , 1         , NULL        ),
    (@nomId , N'TRE'                 , N'TRE'  , N'TRE'   , NULL           , N'TRE'  , 1         , NULL        )
GO
