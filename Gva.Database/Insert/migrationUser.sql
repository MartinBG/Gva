SET IDENTITY_INSERT [Users] ON
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [PasswordSalt], [HasPassword], [Fullname], [Notes], [CertificateThumbprint], [IsActive]) VALUES (2, 'migration', NULL, NULL, 0, 'migration', NULL, NULL, 1)
GO

SET IDENTITY_INSERT [Users] OFF
GO
