print 'DocCorrespondentContacts'
GO 

CREATE TABLE DocCorrespondentContacts
(
    DocCorrespondentContactId	INT				IDENTITY (1, 1) NOT NULL,
	DocId						INT				NOT NULL,
	CorrespondentContactId		INT				NOT NULL,
    Version						ROWVERSION      NOT NULL,
    CONSTRAINT PK_DocCorrespondentContacts PRIMARY KEY CLUSTERED (DocCorrespondentContactId),
	CONSTRAINT [FK_DocCorrespondentContacts_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_DocCorrespondentContacts_CorrespondentContacts] FOREIGN KEY ([CorrespondentContactId]) REFERENCES [dbo].[CorrespondentContacts] ([CorrespondentContactId]),
)
GO 

exec spDescTable  N'DocCorrespondentContacts', N'Контакти на кореспонденти по документ.'
exec spDescColumn N'DocCorrespondentContacts', N'DocCorrespondentContactId', N'Уникален системно генериран идентификатор.'
