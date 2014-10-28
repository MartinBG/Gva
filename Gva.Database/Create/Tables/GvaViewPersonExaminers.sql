PRINT 'GvaViewPersonExaminers'
GO 

CREATE TABLE [dbo].[GvaViewPersonExaminers] (
    [LotId]          INT           NOT NULL,
    [ExaminerCode]   NVARCHAR(50)  NOT NULL,
    [StampNum]       NVARCHAR(50)  NULL,
    [Valid]          BIT           NOT NULL
    CONSTRAINT [PK_GvaViewPersonExaminers]                PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewPersonExaminers_GvaViewPersons] FOREIGN KEY ([LotId])       REFERENCES [dbo].[GvaViewPersons] ([LotId])
)
GO

exec spDescTable  N'GvaViewPersonExaminers', N'Данни за проверяващ.'
exec spDescColumn N'GvaViewPersonExaminers', N'LotId'                     , N'Идентификатор на партида.'
exec spDescColumn N'GvaViewPersonExaminers', N'ExaminerCode'              , N'Код на проверяващ присвоен от съответните власти.'
exec spDescColumn N'GvaViewPersonExaminers', N'StampNum'                  , N'Персонален номер на авторизация (номер на печата).'
exec spDescColumn N'GvaViewPersonExaminers', N'Valid'                     , N'Маркер за валидност.'
GO
