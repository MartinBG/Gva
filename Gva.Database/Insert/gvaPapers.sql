USE [$(dbName)]
GO
SET IDENTITY_INSERT [dbo].[GvaPapers] ON 
GO

INSERT [dbo].[GvaPapers] ([GvaPaperId], [Name], [FromDate], [ToDate], [IsActive], [FirstNumber]) VALUES (1 , N'Стара хартия' , N'1900-01-01 00:00:00.0000000', N'2020-01-01 00:00:00.0000000', 1, 1 )

SET IDENTITY_INSERT [dbo].[GvaPapers] OFF 
GO