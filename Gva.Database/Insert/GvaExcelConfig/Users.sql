print 'Excel Insert Users'
GO

Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'MCvetkov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Минчо Цветков', N'', N'', 1, N'MCvetkov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 3, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'isivanov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Иван С. Иванов', N'', N'', 1, N'isivanov@caa.bg', 1)
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
                        values (N'pgetov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Петър Гетов', N'', N'', 1, N'pgetov@caa.bg', 1)
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
                        values (N'snaydenova', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Светла Найденова', N'', N'', 1, N'snaydenova@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 19, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'lpetrov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Любомир Петров', N'', N'', 1, N'lpetrov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 21, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'amanev', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Андрей Манев', N'', N'', 1, N'amanev@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 23, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'kfilipov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Камен Филипов', N'', N'', 1, N'kfilipov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 25, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'ndzhambov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Николай Джамбов', N'', N'', 1, N'ndzhambov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 28, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'ntoteva', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Надя Тотева', N'', N'', 1, N'ntoteva@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 30, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'milov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Мартин Илов', N'', N'', 1, N'milov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 32, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'pkunchev', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Петко Кунчев', N'', N'', 1, N'pkunchev@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 35, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'ivivanov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Иван Х. Иванов', N'', N'', 1, N'ivivanov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 37, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'vvenkov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Валери Венков', N'', N'', 1, N'vvenkov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 39, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'ghristov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Георги Христов', N'', N'', 1, N'ghristov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 41, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'LManasiev', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Людмил Манасиев', N'', N'', 1, N'LManasiev@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 44, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'IBambov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Иван Бамбов', N'', N'', 1, N'IBambov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 47, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'mihail', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Михаил Божерянов', N'', N'', 1, N'mihail@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 49, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'jivko', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Живко Богданов', N'', N'', 1, N'jivko@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 51, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'hristo', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Христо Алексиев', N'', N'', 1, N'hristo@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 53, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'VValkov', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Валентин Вълков', N'', N'', 1, N'VValkov@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 56, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'nadka', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Надка Кръстева', N'', N'', 1, N'nadka@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 58, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'slavko', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Славко Вараджаков', N'', N'', 1, N'slavko@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 60, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'SLeshev', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Станимир Лешев', N'', N'', 1, N'SLeshev@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 63, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'albena', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Албена Попова', N'', N'', 1, N'albena@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 65, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'DTarlev', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Димитър Тарлев', N'', N'', 1, N'DTarlev@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 68, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'GDochev', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Ганчо Дочев', N'', N'', 1, N'GDochev@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 71, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'nikolinka', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Николинка Угринова', N'', N'', 1, N'nikolinka@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 73, 1
Insert into Users (UserName, PasswordHash, PasswordSalt, FullName, Notes, CertificateThumbPrint, HasPassword, Email, IsActive) 
                        values (N'systemusergva', N'AF1qZXhljakwqt8BKrMYAipAGgs5313e43qW+mr2RrIl1D1vLlbRlLYssxYcHFlbgQ==', N'+FoFiIwx7qMV3ROW7PxWgw==', N'Системен служител ГВА', N'', N'', 1, N'systemusergva@caa.bg', 1)
                        Insert into UnitUsers(UserId, UnitId, IsActive) 
                            select SCOPE_IDENTITY(), 80, 1
GO

