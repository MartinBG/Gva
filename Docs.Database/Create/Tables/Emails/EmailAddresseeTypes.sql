print 'EmailAddresseeTypes'
GO 

CREATE TABLE [dbo].[EmailAddresseeTypes](
    [EmailAddresseeTypeId] [int] IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR (200) NOT NULL,
    [Alias] NVARCHAR (200) NULL,
    [Version] ROWVERSION NOT NULL,
 CONSTRAINT [PK_EmailAddresseeTypes] PRIMARY KEY ([EmailAddresseeTypeId])
)
GO
