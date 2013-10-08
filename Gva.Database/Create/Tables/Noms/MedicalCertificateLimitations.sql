print 'MedicalCertificateLimitations'
GO

CREATE TABLE [dbo].[MedicalCertificateLimitations] (
    [MedicalCertificateId] INT             NOT NULL,
    [MedicalLimitationId]  INT             NOT NULL,
    CONSTRAINT [PK_MedicalCertificateLimitations]                     PRIMARY KEY ([MedicalCertificateId], [MedicalLimitationId]),
    CONSTRAINT [FK_MedicalCertificateLimitations_MedicalCertificates] FOREIGN KEY ([MedicalCertificateId]) REFERENCES [dbo].[MedicalCertificates] ([MedicalCertificateId]),
    CONSTRAINT [FK_MedicalCertificateLimitations_MedicalLimitations]  FOREIGN KEY ([MedicalLimitationId])  REFERENCES [dbo].[MedicalLimitations]  ([MedicalLimitationId])
);
GO

exec spDescTable  N'MedicalCertificateLimitations'                          , N'Ограничения към свидетелство за медицинска годност.'
exec spDescColumn N'MedicalCertificateLimitations', N'MedicalCertificateId' , N'Медицинско/Свидетелство за медицинска годност.'
exec spDescColumn N'MedicalCertificateLimitations', N'MedicalLimitationId'  , N'Ограничение към медицинска годност.'
GO
