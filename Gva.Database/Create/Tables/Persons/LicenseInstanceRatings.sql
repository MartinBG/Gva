print 'LicenseInstanceRatings'
GO

CREATE TABLE [dbo].[LicenseInstanceRatings] (
    [LicenseInstanceRatingId]     INT             NOT NULL IDENTITY(1,1),
    [LicenseInstanceId]           INT             NOT NULL,
    [RatingInstanceId]            INT             NOT NULL,
    [PaperNumber]                 NVARCHAR (50)   NULL,
    [NoNumber]                    BIT             NULL,
    [Ordinal]                     INT             NULL,
    [Version]                     ROWVERSION      NOT NULL,
    CONSTRAINT [PK_LicenseInstanceRatings]                  PRIMARY KEY ([LicenseInstanceRatingId]),
    CONSTRAINT [FK_LicenseInstanceRatings_LicenseInstances] FOREIGN KEY ([LicenseInstanceId]) REFERENCES [dbo].[LicenseInstances] ([LicenseInstanceId]),
    CONSTRAINT [FK_LicenseInstanceRatings_RatingInstances]  FOREIGN KEY ([RatingInstanceId])  REFERENCES [dbo].[RatingInstances]  ([RatingInstanceId])
);
GO

exec spDescTable  N'LicenseInstanceRatings'                              , N'Квалификации приложени към лиценз.'
exec spDescColumn N'LicenseInstanceRatings', N'LicenseInstanceRatingId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LicenseInstanceRatings', N'LicenseInstanceId'        , N'Лиценз.'
exec spDescColumn N'LicenseInstanceRatings', N'RatingInstanceId'         , N'Квалификация.'
exec spDescColumn N'LicenseInstanceRatings', N'PaperNumber'              , N'Номер на хартиеният лист на свидетелството за правоспособност.'
exec spDescColumn N'LicenseInstanceRatings', N'NoNumber'                 , N'Без номер.'
exec spDescColumn N'LicenseInstanceRatings', N'Ordinal'                  , N'Подредба.'
GO
