PRINT 'GvaAppStages'
GO 

CREATE TABLE [dbo].[GvaAppStages] (
    [GvaAppStageId]      INT           NOT NULL IDENTITY,
    [GvaApplicationId]   INT           NOT NULL,
    [GvaStageId]         INT           NOT NULL,
    [StartingDate]       DATETIME2     NOT NULL,
	[StageTermDate]      DATETIME2     NULL,
    [InspectorLotId]     INT           NULL,
    [Ordinal]            INT           NOT NULL,
	[Note]               NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_GvaAppStages]                          PRIMARY KEY ([GvaAppStageId]),
    CONSTRAINT [FK_GvaAppStages_GvaApplications]          FOREIGN KEY ([GvaApplicationId]) REFERENCES [dbo].[GvaApplications] ([GvaApplicationId]),
    CONSTRAINT [FK_GvaAppStages_GvaStages]                FOREIGN KEY ([GvaStageId])       REFERENCES [dbo].[GvaStages] ([GvaStageId]),
    CONSTRAINT [FK_GvaAppStages_Lots]                     FOREIGN KEY ([InspectorLotId])   REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [UQ_GvaAppStages_GvaApplicationId_Ordinal] UNIQUE      ([GvaApplicationId], [Ordinal])
)
GO

exec spDescTable  N'GvaAppStages', N'Дейности по заявление'
exec spDescColumn N'GvaAppStages', N'GvaAppStageId'      , N'Уникален системно генериран идентификатор'
exec spDescColumn N'GvaAppStages', N'GvaApplicationId'   , N'Идентификатор на заявление'
exec spDescColumn N'GvaAppStages', N'GvaStageId'         , N'Идентификатор на вид дейност'
exec spDescColumn N'GvaAppStages', N'StartingDate'       , N'Начална дата'
exec spDescColumn N'GvaAppStages', N'StageTermDate'      , N'Срок'
exec spDescColumn N'GvaAppStages', N'InspectorLotId'     , N'Инспектор'
exec spDescColumn N'GvaAppStages', N'Ordinal'            , N'Пореден номер'
exec spDescColumn N'GvaAppStages', N'Note'               , N'Бележка'
GO
