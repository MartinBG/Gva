print 'Insert EmailStatuses'
GO 

SET IDENTITY_INSERT [dbo].[EmailStatuses] ON
INSERT [dbo].[EmailStatuses] ([EmailStatusId], [Name], [Alias]) VALUES (1, N'Pending', N'Pending')
INSERT [dbo].[EmailStatuses] ([EmailStatusId], [Name], [Alias]) VALUES (2, N'Delivered', N'Delivered')
INSERT [dbo].[EmailStatuses] ([EmailStatusId], [Name], [Alias]) VALUES (3, N'Failed', N'Failed')
SET IDENTITY_INSERT [dbo].[EmailStatuses] OFF

GO
