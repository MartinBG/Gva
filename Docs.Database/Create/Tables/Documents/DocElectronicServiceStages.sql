print 'DocElectronicServiceStages'
GO 

CREATE TABLE DocElectronicServiceStages
(
    DocElectronicServiceStageId		INT            IDENTITY (1, 1) NOT NULL,
    DocId               INT NOT NULL,
    ElectronicServiceStageId INT NOT NULL,
    StartingDate        DATETIME NOT NULL,
    ExpectedEndingDate  DATETIME NULL,
    EndingDate          DATETIME NULL,
    IsCurrentStage      BIT             NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocElectronicServiceStages PRIMARY KEY CLUSTERED (DocElectronicServiceStageId),
    CONSTRAINT [FK_DocElectronicServiceStages_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId]),
    CONSTRAINT [FK_DocElectronicServiceStages_ElectronicServiceStages] FOREIGN KEY ([ElectronicServiceStageId]) REFERENCES [dbo].[ElectronicServiceStages] ([ElectronicServiceStageId]),
)
GO 

exec spDescTable  N'DocElectronicServiceStages', N'Етапи към документите'
exec spDescColumn N'DocElectronicServiceStages', N'DocElectronicServiceStageId', N'Уникален системно генериран идентификатор.'