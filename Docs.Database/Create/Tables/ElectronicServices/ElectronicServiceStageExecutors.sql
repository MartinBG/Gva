print 'ElectronicServiceStageExecutors'
GO 

CREATE TABLE ElectronicServiceStageExecutors
(
    ElectronicServiceStageExecutorId	INT     IDENTITY (1, 1) NOT NULL,
    ElectronicServiceStageId    INT NOT NULL,
    UnitId                      INT NOT NULL,
    IsActive                    BIT NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_ElectronicServiceStageExecutors PRIMARY KEY CLUSTERED (ElectronicServiceStageExecutorId),
    CONSTRAINT [FK_ElectronicServiceStageExecutors_ElectronicServiceStages] FOREIGN KEY ([ElectronicServiceStageId]) REFERENCES [dbo].[ElectronicServiceStages] ([ElectronicServiceStageId]),
    CONSTRAINT [FK_ElectronicServiceStageExecutors_Units] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] (UnitId),
)
GO 

exec spDescTable  N'ElectronicServiceStageExecutors', N'Изпълнители на етапи'
exec spDescColumn N'ElectronicServiceStageExecutors', N'ElectronicServiceStageExecutorId', N'Уникален системно генериран идентификатор.'