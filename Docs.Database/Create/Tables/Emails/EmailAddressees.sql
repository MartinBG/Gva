print 'EmailAddressees'
GO 

CREATE TABLE [dbo].[EmailAddressees](
    [EmailAddresseeId] [int] IDENTITY(1,1) NOT NULL,
    [EmailId] [int] NOT NULL,
    [EmailAddresseeTypeId] [int] NOT NULL,
    [Address] [nvarchar](1000) NOT NULL,
    [Version] ROWVERSION NOT NULL,
 CONSTRAINT [PK_EmailAddressees] PRIMARY KEY ([EmailAddresseeId]),
 CONSTRAINT [FK_EmailAddressees_Emails] FOREIGN KEY ([EmailId]) REFERENCES [dbo].[Emails] ([EmailId]),
 CONSTRAINT [FK_EmailAddressees_EmailAddresseeTypes] FOREIGN KEY ([EmailAddresseeTypeId]) REFERENCES [dbo].[EmailAddresseeTypes] ([EmailAddresseeTypeId])
)
GO
