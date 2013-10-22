print 'ElectronicServiceStages'
GO 

CREATE TABLE ElectronicServiceStages
(
    ElectronicServiceStageId	INT     IDENTITY (1, 1) NOT NULL,
    DocTypeId           INT NOT NULL,
    Name				NVARCHAR (200)  NOT NULL,
	[Description]		NVARCHAR (MAX)  NULL,
	Alias				NVARCHAR (200)  NULL,
    Duration            INT             NULL,
    IsDurationReset     BIT             NOT NULL,         
    IsFirstByDefault    BIT             NOT NULL,
    IsLastStage         BIT             NOT NULL,
    IsActive		    BIT             NOT NULL,
    Version				ROWVERSION      NOT NULL,
    CONSTRAINT PK_ElectronicServiceStages PRIMARY KEY CLUSTERED (ElectronicServiceStageId),
    CONSTRAINT [FK_ElectronicServiceStages_DocTypes] FOREIGN KEY ([DocTypeId]) REFERENCES [dbo].[DocTypes] ([DocTypeId]),
)
GO 

exec spDescTable  N'ElectronicServiceStages', N'Етапи за различните услуги'
exec spDescColumn N'ElectronicServiceStages', N'ElectronicServiceStageId', N'Уникален системно генериран идентификатор.'