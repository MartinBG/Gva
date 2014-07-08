PRINT 'GvaViewOrganizationExaminers'
GO 

CREATE TABLE [dbo].[GvaViewOrganizationExaminers] (
    [LotId]          INT           NOT NULL,
    [PartIndex]      INT           NOT NULL,
    [PersonId]       INT           NOT NULL,
    [ExaminerCode]   NVARCHAR(50)  NOT NULL,
    [StampNum]       NVARCHAR(50)  NULL,
    [PermitedAW]     BIT           NOT NULL,
    [PermitedCheck]  BIT           NOT NULL,
    [Valid]          BIT           NOT NULL
    CONSTRAINT [PK_GvaViewOrganizationExaminers]                      PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewOrganizationExaminers_GvaViewOrganizations] FOREIGN KEY ([LotId])       REFERENCES [dbo].[GvaViewOrganizations] ([LotId]),
    CONSTRAINT [FK_GvaViewOrganizationExaminers_GvaViewPersons]       FOREIGN KEY ([PersonId])    REFERENCES [dbo].[GvaViewPersons] ([LotId])
)
GO

exec spDescTable  N'GvaViewOrganizationExaminers', N'Данни за инспектор.'
exec spDescColumn N'GvaViewOrganizationExaminers', N'PartIndex'                 , N'Идентификатор на партида.'
exec spDescColumn N'GvaViewOrganizationExaminers', N'ExaminerCode'              , N'Код на проверяващ присвоен от съответните власти.'
exec spDescColumn N'GvaViewOrganizationExaminers', N'StampNum'                  , N'Персонален номер на авторизация (номер на печата).'
exec spDescColumn N'GvaViewOrganizationExaminers', N'PermitedAW'                , N'Разрешена проверка на ЛГ.'
exec spDescColumn N'GvaViewOrganizationExaminers', N'PermitedCheck'             , N'Разрешена проверка на лица.'
exec spDescColumn N'GvaViewOrganizationExaminers', N'Valid'                     , N'Маркер за валидност.'
GO
