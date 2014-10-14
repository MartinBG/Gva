PRINT 'GvaViewOrganizationApprovals'
GO 

CREATE TABLE [dbo].[GvaViewOrganizationApprovals] (
    [LotId]                    INT               NOT NULL,
    [PartIndex]                INT               NOT NULL,
    [PartId]                   INT               NOT NULL,
    [DocumentNumber]           NVARCHAR(50)      NULL,
    [ApprovalTypeId]           INT               NULL,
    [ApprovalStateId]          INT               NULL,
    CONSTRAINT [PK_GvaViewOrganizationApproval]                        PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewOrganizationApproval_GvaViewOrganizations]   FOREIGN KEY ([LotId])                REFERENCES [dbo].[GvaViewOrganizations] ([LotId]),
    CONSTRAINT [FK_GvaViewOrganizationApproval_ApprovalTypeId]         FOREIGN KEY ([ApprovalTypeId])       REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewOrganizationApproval_ApprovalStateId]        FOREIGN KEY ([ApprovalStateId])      REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

exec spDescTable  N'GvaViewOrganizationApprovals', N'Данни за одобрение.'
exec spDescColumn N'GvaViewOrganizationApprovals', N'LotId'                    , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewOrganizationApprovals', N'PartIndex'                , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewOrganizationApprovals', N'DocumentNumber'           , N'Номер на документ.'
exec spDescColumn N'GvaViewOrganizationApprovals', N'ApprovalTypeId'           , N'Типа на одобрението'
exec spDescColumn N'GvaViewOrganizationApprovals', N'ApprovalStateId'          , N'Състояние на одобрението.'
GO
