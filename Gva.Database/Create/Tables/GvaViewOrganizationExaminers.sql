PRINT 'GvaViewOrganizationExaminers'
GO 

CREATE TABLE [dbo].[GvaViewOrganizationExaminers] (
    [LotId]          INT           NOT NULL,
    [LotPartId]      INT           NOT NULL,
    [PersonLotId]    INT           NOT NULL,
    [ExaminerCode]   NVARCHAR(50)  NOT NULL,
    [StampNum]       NVARCHAR(50)  NULL,
    [PermitedAW]     BIT           NOT NULL,
    [PermitedCheck]  BIT           NOT NULL,
    [Valid]          BIT           NOT NULL
    CONSTRAINT [PK_GvaViewOrganizationExaminers]                PRIMARY KEY ([LotId], [LotPartId]),
    CONSTRAINT [FK_GvaViewOrganizationExaminers_Lots]           FOREIGN KEY ([LotId])       REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewOrganizationExaminers_Lots2]          FOREIGN KEY ([PersonLotId]) REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewOrganizationExaminers_LotParts]       FOREIGN KEY ([LotPartId])   REFERENCES [dbo].[LotParts] ([LotPartId])
)
GO

exec spDescTable  N'GvaViewOrganizationExaminers', N'Данни за инспектор.'
exec spDescColumn N'GvaViewOrganizationExaminers', N'LotId'                     , N'Идентификатор на партида.'
exec spDescColumn N'GvaViewOrganizationExaminers', N'ExaminerCode'              , N'Код на проверяващ присвоен от съответните власти.'
exec spDescColumn N'GvaViewOrganizationExaminers', N'StampNum'                  , N'Персонален номер на авторизация (номер на печата).'
exec spDescColumn N'GvaViewOrganizationExaminers', N'PermitedAW'                , N'Разрешена проверка на ЛГ.'
exec spDescColumn N'GvaViewOrganizationExaminers', N'PermitedCheck'             , N'Разрешена проверка на лица.'
exec spDescColumn N'GvaViewOrganizationExaminers', N'Valid'                     , N'Маркер за валидност.'
GO
