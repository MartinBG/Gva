print 'Excel Insert Users'
GO

Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'systemuseraop', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'systemuseraop', N'', N'', 1, N'systemuseraop@aop.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 7, 1
GO

