PRINT 'GvaInvalidActNumbers'
GO 

CREATE TABLE [dbo].[GvaInvalidActNumbers](
    [ActNumber]     INT           NOT NULL,
    [Reason]        NVARCHAR(200) NULL,
    [RegisterId]    INT           NOT NULL,
    CONSTRAINT [PK_GvaInvalidActNumbers] PRIMARY KEY ([ActNumber], [RegisterId])
)
GO

exec spDescTable  N'GvaInvalidActNumbers', N'Невалидни деловодни номера'
exec spDescColumn N'GvaInvalidActNumbers', N'ActNumber'           , N'Деловоден номер'
exec spDescColumn N'GvaInvalidActNumbers', N'Reason'              , N'Причина'
exec spDescColumn N'GvaInvalidActNumbers', N'RegisterId'          , N'Регистър'
GO
