print 'Settlements'
GO

CREATE TABLE [dbo].[Settlements] (
    [SettlementId]       INT             NOT NULL IDENTITY(1,1),
    [Code]               NVARCHAR (50)   NULL,
    [Name]               NVARCHAR (50)   NULL,
    [Version]            ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Settlements] PRIMARY KEY ([SettlementId])
);
GO

exec spDescTable  N'Settlements'                   , N'Населени места'
exec spDescColumn N'Settlements', N'SettlementId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Settlements', N'Code'          , N'Код.'
exec spDescColumn N'Settlements', N'Name'          , N'Наименование.'
GO
