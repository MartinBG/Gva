﻿print 'Excel Insert Users'
GO

Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'MCvetkov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Минчо Цветков', N'', N'', 1, N'MCvetkov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 3, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'VLorentzo', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Владимир Лоренцо Димитров', N'', N'', 1, N'VLorentzo@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 6, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'VNaumova', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Ваня Наумова', N'', N'', 1, N'VNaumova@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 9, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'kkirina', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Катя Кирина', N'', N'', 1, N'kkirina@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 11, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'peter.g', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Петър Гетов', N'', N'', 1, N'peter.g@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 13, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'vmarkova', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Венера Маркова', N'', N'', 1, N'vmarkova@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 15, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'idorjinov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Иво Доржинов', N'', N'', 1, N'idorjinov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 17, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'ivan.s.ivanov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Иван С. Иванов', N'', N'', 1, N'ivan.s.ivanov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 20, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'nadia.toteva', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Надя Тотева', N'', N'', 1, N'nadia.toteva@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 22, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'ivan.h', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Иван Х. Иванов', N'', N'', 1, N'ivan.h@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 26, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'LManasiev', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Людмил Манасиев', N'', N'', 1, N'LManasiev@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 29, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'IBambov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Иван Бамбов', N'', N'', 1, N'IBambov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 32, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'mihail', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Михаил Божерянов', N'', N'', 1, N'mihail@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 34, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'jivko', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Живко Богданов', N'', N'', 1, N'jivko@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 36, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'hristo', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Христо Алексиев', N'', N'', 1, N'hristo@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 38, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'VValkov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Валентин Вълков', N'', N'', 1, N'VValkov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 41, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'nadka', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Надка Кръстева', N'', N'', 1, N'nadka@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 43, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'slavko', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Славко Вараджаков', N'', N'', 1, N'slavko@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 45, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'SLeshev', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Станимир Лешев', N'', N'', 1, N'SLeshev@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 48, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'albena', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Албена Попова', N'', N'', 1, N'albena@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 50, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'DTarlev', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Димитър Тарлев', N'', N'', 1, N'DTarlev@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 53, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'GDochev', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Ганчо Дочев', N'', N'', 1, N'GDochev@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 56, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'nikolinka', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Николинка Угринова', N'', N'', 1, N'nikolinka@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 58, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'systemusergva', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Системен служител ГВА', N'', N'', 1, N'systemusergva@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 65, 1
GO

