print 'Insert AdministrativeEmailStatuses'
GO 


SET IDENTITY_INSERT [dbo].[AdministrativeEmailStatuses] ON
INSERT [dbo].[AdministrativeEmailStatuses] ([AdministrativeEmailStatusId], [Name], [Alias]) VALUES (1, N'Pending', N'Pending')
INSERT [dbo].[AdministrativeEmailStatuses] ([AdministrativeEmailStatusId], [Name], [Alias]) VALUES (2, N'Delivered', N'Delivered')
INSERT [dbo].[AdministrativeEmailStatuses] ([AdministrativeEmailStatusId], [Name], [Alias]) VALUES (3, N'Failed', N'Failed')
SET IDENTITY_INSERT [dbo].[AdministrativeEmailStatuses] OFF

GO

