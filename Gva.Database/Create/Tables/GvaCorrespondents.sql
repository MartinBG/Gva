PRINT 'GvaCorrespondents'
GO 

CREATE TABLE [dbo].[GvaCorrespondents] (
    [GvaCorrespondentId]    INT  NOT NULL IDENTITY,
    [LotId]               INT  NOT NULL,
    [CorrespondentId]     INT  NOT NULL,
    [IsActive]     bit  NOT NULL
    CONSTRAINT [PK_GvaCorrespondents]                PRIMARY KEY ([GvaCorrespondentId]),
    CONSTRAINT [FK_GvaCorrespondents_Lots]           FOREIGN KEY ([LotId])              REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaCorrespondents_Correspondents] FOREIGN KEY ([CorrespondentId])    REFERENCES [dbo].[Correspondents] ([CorrespondentId])
)
GO


