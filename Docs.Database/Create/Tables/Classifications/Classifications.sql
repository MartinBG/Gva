print 'Classifications'
GO

CREATE TABLE dbo.Classifications (
    ClassificationId                INT                 NOT NULL IDENTITY(1,1),
    Name				  NVARCHAR (200)      NOT NULL,
    Alias			      NVARCHAR (200)  NULL,
    IsActive              BIT                 NOT NULL,
    Version               ROWVERSION          NOT NULL,
    CONSTRAINT PK_Classifications PRIMARY KEY CLUSTERED (ClassificationId ASC),
);
GO

exec spDescTable  N'Classifications', N'Класификационни схеми.'
exec spDescColumn N'Classifications', N'ClassificationId', N'Уникален системно генериран идентификатор.'
GO
