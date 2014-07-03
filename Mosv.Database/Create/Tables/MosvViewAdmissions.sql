PRINT 'MosvViewAdmissions'
GO 

CREATE TABLE [dbo].[MosvViewAdmissions] (
    [LotId]                        INT          NOT NULL,
	[ApplicationDocId]			   INT			NULL,
    [IncomingLot]                  NVARCHAR(50) NULL,
    [IncomingNumber]               NVARCHAR(50) NULL,
    [IncomingDate]                 DATETIME2    NULL,
    [ApplicantType]                NVARCHAR(50) NULL,
    [Applicant]                    NVARCHAR(50) NULL,
    CONSTRAINT [PK_MosvViewAdmissions]      PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_MosvViewAdmissions_Lots]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[Lots] ([LotId]),
	CONSTRAINT [FK_MosvViewAdmissions_Docs]  FOREIGN KEY ([ApplicationDocId]) REFERENCES [dbo].[Docs] ([DocId])
)
GO

exec spDescTable  N'MosvViewAdmissions', N'Достъпи.'
exec spDescColumn N'MosvViewAdmissions', N'LotId', N'Идентификатор на партида на ВС.'
exec spDescColumn N'MosvViewAdmissions', N'IncomingLot', N'Партида'
exec spDescColumn N'MosvViewAdmissions', N'IncomingNumber', N'Вх.номер'
exec spDescColumn N'MosvViewAdmissions', N'IncomingDate', N'Дата'
exec spDescColumn N'MosvViewAdmissions', N'ApplicantType', N'Вид заявител'
exec spDescColumn N'MosvViewAdmissions', N'Applicant', N'Заявител'
GO
