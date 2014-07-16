print 'Emails'
GO 

CREATE TABLE [dbo].[Emails](
    [EmailId] [int] IDENTITY(1,1) NOT NULL,
    [EmailTypeId] [int] NOT NULL,
    [EmailStatusId] [int] NOT NULL,
    [Subject] [nvarchar](1000) NULL,
    [Body] [nvarchar](MAX) NULL,
    [SentDate] [datetime] NULL,
    [Version] ROWVERSION NOT NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY ([EmailId]),
 CONSTRAINT [FK_Emails_EmailStatuses] FOREIGN KEY ([EmailStatusId]) REFERENCES [dbo].[EmailStatuses] ([EmailStatusId]),
 CONSTRAINT [FK_Emails_EmailTypes] FOREIGN KEY ([EmailTypeId]) REFERENCES [dbo].[EmailTypes] ([EmailTypeId])
)
GO
