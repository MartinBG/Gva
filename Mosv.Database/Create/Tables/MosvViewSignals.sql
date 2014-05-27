PRINT 'MosvViewSignals'
GO 

CREATE TABLE [dbo].[MosvViewSignals] (
    [LotId]                        INT          NOT NULL,
    [IncomingLot]                  NVARCHAR(50) NULL,
    [IncomingNumber]               NVARCHAR(50) NULL,
    [IncomingDate]                 DATETIME2    NULL,
    [Applicant]                    NVARCHAR(50) NULL,
    [Institution]                  NVARCHAR(50) NULL,
    [Violation]                    NVARCHAR(50) NULL,
    CONSTRAINT [PK_MosvViewSignals]      PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_MosvViewSignals_Lots]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[Lots] ([LotId])
)
GO

exec spDescTable  N'MosvViewSignals', N'Достъпи.'
exec spDescColumn N'MosvViewSignals', N'LotId', N'Идентификатор на партида на ВС.'
exec spDescColumn N'MosvViewSignals', N'IncomingLot', N'Партида'
exec spDescColumn N'MosvViewSignals', N'IncomingNumber', N'Вх.номер'
exec spDescColumn N'MosvViewSignals', N'IncomingDate', N'Дата'
exec spDescColumn N'MosvViewSignals', N'Applicant', N'Заявител'
exec spDescColumn N'MosvViewSignals', N'Institution', N'Институция'
exec spDescColumn N'MosvViewSignals', N'Violation', N'Извършено нарушение'
GO
