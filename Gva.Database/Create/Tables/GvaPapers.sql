PRINT 'GvaPapers'
GO 

CREATE TABLE [dbo].[GvaPapers] (
    [GvaPaperId]        INT            NOT NULL IDENTITY,
    [Name]              NVARCHAR (50)  NOT NULL,
    [FromDate]          DATETIME2      NOT NULL,
	[ToDate]            DATETIME2      NOT NULL,
    [IsActive]          BIT            NOT NULL,
    [FirstNumber]       INT            NOT NULL,
    CONSTRAINT [PK_GvaPapers]         PRIMARY KEY ([GvaPaperId])
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_GvaPapers_Name]
ON [dbo].[GvaPapers](Name)
GO


exec spDescTable  N'GvaPapers', N'Информация за хартия за принтиране на документи.'
exec spDescColumn N'GvaPapers', N'GvaPaperId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaPapers', N'Name'             , N'Наименование на хартита.'
exec spDescColumn N'GvaPapers', N'FromDate'         , N'От дата.'
exec spDescColumn N'GvaPapers', N'ToDate'           , N'До дата.'
exec spDescColumn N'GvaPapers', N'IsActive'         , N'Флаг дали е активен записа.'
exec spDescColumn N'GvaPapers', N'FirstNumber'      , N'Първи № на тази хартия.'

GO
