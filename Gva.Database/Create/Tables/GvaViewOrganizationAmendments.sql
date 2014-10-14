PRINT 'GvaViewOrganizationAmendments'
GO 

CREATE TABLE [dbo].[GvaViewOrganizationAmendments] (
    [LotId]                    INT               NOT NULL,
    [PartIndex]                INT               NOT NULL,
	[PartId]                   INT               NOT NULL,
    [ApprovalPartIndex]        INT               NOT NULL,
    [DocumentNumber]           NVARCHAR(50)      NULL,
    [DocumentDateIssue]        DATETIME          NULL,
    [ChangeNum]                INT               NULL,
    [ApplicationName]          NVARCHAR(MAX)     NULL,
    [Index]                    INT               NOT NULL,
    CONSTRAINT [PK_GvaViewOrganizationAmendments]                                PRIMARY KEY ([LotId], [ApprovalPartIndex], [PartIndex]),
    CONSTRAINT [FK_GvaViewOrganizationAmendments_GvaViewOrganizationApprovals]   FOREIGN KEY ([LotId], [ApprovalPartIndex]) REFERENCES [dbo].[GvaViewOrganizationApprovals] ([LotId], [PartIndex])
)
GO

exec spDescTable  N'GvaViewOrganizationAmendments', N'Данни за изменение към одобрение.'
exec spDescColumn N'GvaViewOrganizationAmendments', N'LotId'                    , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewOrganizationAmendments', N'ApprovalPartIndex'        , N'Идентификатор на одобрението към което е свързано това изменение'
exec spDescColumn N'GvaViewOrganizationAmendments', N'DocumentNumber'           , N'Номер на документ.'
exec spDescColumn N'GvaViewOrganizationAmendments', N'DocumentDateIssue'        , N'Дата на издаване на изменението.'
exec spDescColumn N'GvaViewOrganizationAmendments', N'ChangeNum'                , N'Номер на изменението.'
exec spDescColumn N'GvaViewOrganizationAmendments', N'ApplicationName'          , N'Наименование на заявлението.'
GO
