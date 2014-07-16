print 'EmailAttachments'
GO 

CREATE TABLE [dbo].[EmailAttachments](
    [EmailAttachmentId] [int] IDENTITY(1,1) NOT NULL,
    [EmailId] [int] NOT NULL,
    [Name] [nvarchar](500) NULL,
    [ContentId] [uniqueidentifier] NOT NULL,
    [Version] ROWVERSION NOT NULL,
 CONSTRAINT [PK_EmailAttachments] PRIMARY KEY ([EmailAttachmentId]),
 CONSTRAINT [FK_EmailAttachments_Emails] FOREIGN KEY ([EmailId]) REFERENCES [dbo].[Emails] ([EmailId])
)
GO
