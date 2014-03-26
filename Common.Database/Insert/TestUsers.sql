SET IDENTITY_INSERT [Users] ON

INSERT INTO [Users]
    ([UserId],[Username],[PasswordHash]                                                         ,[PasswordSalt]             ,[HasPassword],[Fullname]       ,[Notes],[CertificateThumbprint],[IsActive],[Version])
VALUES
    (1       ,N'admin'  ,N'AMWr4Peeajc9Q0bsdV7mWBwK6fLJN/Cr/ksp2jV4M50RQ587JQHptHzA7HLDOLSHGg==',N'88SHIJlJUThYeUFDUQKJoQ==',1            ,N'Администратор' ,N''    ,N''                    ,1         ,DEFAULT  )

SET IDENTITY_INSERT [Users] OFF
GO
