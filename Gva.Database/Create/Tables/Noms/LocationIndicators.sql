print 'LocationIndicators'
GO

CREATE TABLE [dbo].[LocationIndicators] (
    [LocationIndicatorId]       INT             NOT NULL IDENTITY(1,1),
    [Code]                      NVARCHAR (50)   NULL,
    [Name]                      NVARCHAR (50)   NULL,
    [Version]                   ROWVERSION      NOT NULL,
    CONSTRAINT [PK_LocationIndicators] PRIMARY KEY ([LocationIndicatorId])
);
GO

exec spDescTable  N'LocationIndicators'                          , N'Индикатори за местоположение.'
exec spDescColumn N'LocationIndicators', N'LocationIndicatorId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LocationIndicators', N'Code'                 , N'Код.'
exec spDescColumn N'LocationIndicators', N'Name'                 , N'Наименование.'
GO
