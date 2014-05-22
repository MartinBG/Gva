PRINT 'AopEmployers'
GO 

CREATE TABLE [dbo].[AopEmployers] (
    [AopEmployerId]    INT  NOT NULL IDENTITY,
    [Name] NVARCHAR(500) NOT NULL,
	[LotNum] NVARCHAR(50) NOT NULL,
    [AopEmployerTypeId] INT NOT NULL,
	[Version] ROWVERSION NOT NULL,
    CONSTRAINT [PK_AopEmployers]           PRIMARY KEY ([AopEmployerId]),
	CONSTRAINT [FK_AopEmployers_AopEmployerTypes]      FOREIGN KEY ([AopEmployerTypeId])            REFERENCES [dbo].[AopEmployerTypes] ([AopEmployerTypeId])
)
GO
