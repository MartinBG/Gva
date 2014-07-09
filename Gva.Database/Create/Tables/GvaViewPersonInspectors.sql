PRINT 'GvaViewPersonInspectors'
GO 

CREATE TABLE [dbo].[GvaViewPersonInspectors] (
    [LotId]          INT           NOT NULL,
    [ExaminerCode]   NVARCHAR(50)  NOT NULL,
    [CaaId]          INT           NOT NULL,
    [StampNum]       NVARCHAR(50)  NULL,
    [Valid]          BIT           NOT NULL
    CONSTRAINT [PK_GvaViewPersonInspectors]                PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewPersonInspectors_GvaViewPersons] FOREIGN KEY ([LotId])       REFERENCES [dbo].[GvaViewPersons] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonInspectors_NomValues]      FOREIGN KEY ([CaaId])       REFERENCES [dbo].[NomValues] ([NomValueId]),
)
GO

exec spDescTable  N'GvaViewPersonInspectors', N'Данни за инспектор.'
exec spDescColumn N'GvaViewPersonInspectors', N'LotId'                     , N'Идентификатор на партида.'
exec spDescColumn N'GvaViewPersonInspectors', N'ExaminerCode'              , N'Код на проверяващ присвоен от съответните власти.'
exec spDescColumn N'GvaViewPersonInspectors', N'CaaId'                     , N'Гражданска въздухоплавателна авиация.'
exec spDescColumn N'GvaViewPersonInspectors', N'StampNum'                  , N'Персонален номер на авторизация (номер на печата).'
exec spDescColumn N'GvaViewPersonInspectors', N'Valid'                     , N'Маркер за валидност.'
GO
