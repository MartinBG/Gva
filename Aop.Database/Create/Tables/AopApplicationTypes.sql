﻿PRINT 'AopApplicationTypes'
GO 

CREATE TABLE [dbo].[AopApplicationTypes] (
    [AopApplicationTypeId]    INT  NOT NULL IDENTITY,
    [Name] NVARCHAR(200) NOT NULL,
    [Alias] NVARCHAR(200) NOT NULL,
	[IsActive] BIT NOT NULL,
	[Version] ROWVERSION NOT NULL,
    CONSTRAINT [PK_AopApplicationTypes]           PRIMARY KEY ([AopApplicationTypeId])
)
GO
