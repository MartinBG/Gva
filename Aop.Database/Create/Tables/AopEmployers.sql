PRINT 'AopEmployers'
GO 

CREATE TABLE [dbo].[AopEmployers] (
    [AopEmployerId] INT NOT NULL IDENTITY,
    [Name] NVARCHAR(500) NOT NULL,
    [LotNum] NVARCHAR(50) NULL,
    [Uic] NVARCHAR(50) NULL,
    [AopEmployerTypeId] INT NOT NULL,
    [Version] ROWVERSION NOT NULL,
    CONSTRAINT [PK_AopEmployers] PRIMARY KEY ([AopEmployerId]),
    CONSTRAINT [FK_AopEmployers_NomValues] FOREIGN KEY ([AopEmployerTypeId]) REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO
