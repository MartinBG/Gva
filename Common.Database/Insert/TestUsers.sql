SET IDENTITY_INSERT [Users] ON

INSERT INTO [Users]
    ([UserId],[Username],[PasswordHash]                                                         ,[PasswordSalt]             ,[HasPassword],[Fullname]       ,[Notes],[CertificateThumbprint],[IsActive],[Version])
VALUES
    (1       ,N'admin1' ,N'ABK2DHMbHpkLhil2936eM5ahyZosvo1ead50BV4b7g5SdMZPWvywYv7dKRbSv9OZFw==',N'YqjPanxWi2qhXqP++B5Zlg==',1            ,N'Администратор1',N''    ,N''                    ,1         ,DEFAULT  )

SET IDENTITY_INSERT [Users] OFF
GO