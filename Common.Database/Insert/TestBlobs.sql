SET IDENTITY_INSERT [Blobs] ON

INSERT INTO [Blobs]
    ([BlobId], [Key]                                , [Hash]                                    , [Size], [Content] , [IsDeleted])
VALUES
    (1       ,'7C0604F9-FB44-4CCD-BE0E-66E82142AE76', 'A94A8FE5CCB19BA61C4C0873D391E987982FBBD3', 4     , 0x74657374, 0          )

SET IDENTITY_INSERT [Blobs] OFF
GO
