PRINT 'GvaViewAircraftAws'
GO 

CREATE TABLE [dbo].[GvaViewAircraftAws] (
    [LotPartId] INT           NOT NULL,
    [LotId] INT           NOT NULL,
    [RegId] INT               NOT NULL,
    [IssueDate]            DATETIME2  NOT NULL,
    [ValidFromDate]            DATETIME2  NOT NULL,
    [ValidToDate]            DATETIME2  NOT NULL,
    [Inspector]             NVARCHAR(50)    NULL,
    [EASA15IssueDate]            DATETIME2  NULL,
    [EASA15IssueValidToDate]            DATETIME2  NULL,
    CONSTRAINT [PK_GvaViewAircraftAws]      PRIMARY KEY ([LotPartId]),
    CONSTRAINT [FK_GvaViewAircraftAws_LotParts]  FOREIGN KEY ([LotPartId]) REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewAircraftAws_GvaViewAircrafts]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[Lots] ([LotId])
)
GO

exec spDescTable  N'GvaViewAircraftAws', N'Регистрации на ВС'
exec spDescColumn N'GvaViewAircraftAws', N'LotPartId', N'Идентификатор на регистрация на ВС.'
exec spDescColumn N'GvaViewAircraftAws', N'LotId'           , N'Идентификатор на партида на ВС.'
exec spDescColumn N'GvaViewAircraftAws', N'RegId'           , N'Рег. номер.'
exec spDescColumn N'GvaViewAircraftAws', N'IssueDate'           , N'Идентификатор на летателна годност'
exec spDescColumn N'GvaViewAircraftAws', N'ValidFromDate'           , N'Идентификатор на летателна годност'
exec spDescColumn N'GvaViewAircraftAws', N'ValidToDate'           , N'Идентификатор на летателна годност'
exec spDescColumn N'GvaViewAircraftAws', N'Inspector'           , N'Идентификатор на летателна годност'
exec spDescColumn N'GvaViewAircraftAws', N'EASA15IssueDate'           , N'Идентификатор на летателна годност'
exec spDescColumn N'GvaViewAircraftAws', N'EASA15IssueValidToDate'           , N'Идентификатор на летателна годност'
GO
