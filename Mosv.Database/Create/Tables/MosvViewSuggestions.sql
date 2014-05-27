PRINT 'MosvViewSuggestions'
GO 

CREATE TABLE [dbo].[MosvViewSuggestions] (
    [LotId]                        INT          NOT NULL,
    [IncomingLot]                  NVARCHAR(50) NULL,
    [IncomingNumber]               NVARCHAR(50) NULL,
    [IncomingDate]                 DATETIME2    NULL,
    [Applicant]                    NVARCHAR(50) NULL,
    CONSTRAINT [PK_MosvViewSuggestions]       PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_MosvViewSuggestions_Lots]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[Lots] ([LotId])
)
GO

exec spDescTable  N'MosvViewSuggestions', N'Достъпи.'
exec spDescColumn N'MosvViewSuggestions', N'LotId', N'Идентификатор на партида на ВС.'
exec spDescColumn N'MosvViewSuggestions', N'IncomingLot', N'Партида'
exec spDescColumn N'MosvViewSuggestions', N'IncomingNumber', N'Вх.номер'
exec spDescColumn N'MosvViewSuggestions', N'IncomingDate', N'Дата'
exec spDescColumn N'MosvViewSuggestions', N'Applicant', N'Заявител'
GO
