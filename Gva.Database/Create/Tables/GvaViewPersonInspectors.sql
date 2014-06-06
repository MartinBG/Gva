PRINT 'GvaViewPersonInspectors'
GO 

CREATE TABLE [dbo].[GvaViewPersonInspectors] (
    [LotId]          INT           NOT NULL,
    [ExaminerCode]   NVARCHAR(50)  NOT NULL,
    [CaaName]        NVARCHAR(200) NOT NULL,
    [StampNum]       NVARCHAR(50)  NULL,
    [IsValid]        BIT           NOT NULL
    CONSTRAINT [PK_GvaViewPersonInspectors]                PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewPersonInspectors_Lots]           FOREIGN KEY ([LotId])       REFERENCES [dbo].[Lots] ([LotId])
)
GO

exec spDescTable  N'GvaViewPersonInspectors', N'Данни за инспектор.'
exec spDescColumn N'GvaViewPersonInspectors', N'LotId'                     , N'Идентификатор на партида.'
exec spDescColumn N'GvaViewPersonInspectors', N'ExaminerCode'              , N'Код на проверяващ присвоен от съответните власти.'
exec spDescColumn N'GvaViewPersonInspectors', N'CaaName'                   , N'Име на гражданска въздухоплавателна авиация.'
exec spDescColumn N'GvaViewPersonInspectors', N'StampNum'                  , N'Персонален номер на авторизация (номер на печата).'
exec spDescColumn N'GvaViewPersonInspectors', N'IsValid'                   , N'Маркер за валидност.'
GO
