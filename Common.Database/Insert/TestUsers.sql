SET IDENTITY_INSERT [Users] ON

INSERT INTO [Users] ([UserId],[Username],[PasswordHash],[PasswordSalt],[HasPassword],[Fullname],[Notes],[CertificateThumbprint],[IsActive])
	      SELECT    1       ,N'admin'   ,N'AMWr4Peeajc9Q0bsdV7mWBwK6fLJN/Cr/ksp2jV4M50RQ587JQHptHzA7HLDOLSHGg==',N'88SHIJlJUThYeUFDUQKJoQ==',1       ,N'Администратор'       ,N''    ,N''                    ,1
UNION ALL SELECT	3       ,N'system'  ,N'AMWr4Peeajc9Q0bsdV7mWBwK6fLJN/Cr/ksp2jV4M50RQ587JQHptHzA7HLDOLSHGg==',N'88SHIJlJUThYeUFDUQKJoQ==',1       ,N'Системен потребител' ,N''    ,N''                    ,1
UNION ALL SELECT    4       ,N'peter'   ,N'AMWr4Peeajc9Q0bsdV7mWBwK6fLJN/Cr/ksp2jV4M50RQ587JQHptHzA7HLDOLSHGg==',N'88SHIJlJUThYeUFDUQKJoQ==',1       ,N'Администратор'       ,N''    ,N''                    ,1
UNION ALL SELECT    5       ,N'test1'   ,N'ABIyNHO6L7Kz25WG+DqSMK3b1S0vdyfk4Jg8rVaDNIecOcw9b9v11w2jI2tasvfpPQ==',N'rYQCvEmLmQBla59wepaPGA==',1       ,N'test1'               ,N''    ,N''                    ,1

SET IDENTITY_INSERT [Users] OFF
GO
