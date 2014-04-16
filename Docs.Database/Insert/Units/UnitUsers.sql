SET IDENTITY_INSERT [UnitUsers] ON

INSERT INTO [UnitUsers]([UnitUserId],[UserId],[UnitId],[IsActive])VALUES(1000,1,1002,1);
INSERT INTO [UnitUsers]([UnitUserId],[UserId],[UnitId],[IsActive])VALUES(1001,3,1003,1);

SET IDENTITY_INSERT [UnitUsers] OFF
GO

