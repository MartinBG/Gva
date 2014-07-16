print 'Insert EmailAddresseeTypes'
GO 

SET IDENTITY_INSERT [dbo].[EmailAddresseeTypes] ON
INSERT [dbo].[EmailAddresseeTypes] ([EmailAddresseeTypeId], [Name], [Alias]) VALUES (1, N'From', N'To')
INSERT [dbo].[EmailAddresseeTypes] ([EmailAddresseeTypeId], [Name], [Alias]) VALUES (2, N'To', N'To')
INSERT [dbo].[EmailAddresseeTypes] ([EmailAddresseeTypeId], [Name], [Alias]) VALUES (3, N'Cc', N'Cc')
INSERT [dbo].[EmailAddresseeTypes] ([EmailAddresseeTypeId], [Name], [Alias]) VALUES (4, N'Bcc', N'Bcc')
SET IDENTITY_INSERT [dbo].[EmailAddresseeTypes] OFF

GO
