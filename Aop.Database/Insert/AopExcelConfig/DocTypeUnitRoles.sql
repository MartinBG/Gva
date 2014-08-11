print 'Excel Insert DocTypeUnitRoles'
GO

SET IDENTITY_INSERT [DocTypeUnitRoles] ON
INSERT INTO [DocTypeUnitRoles]([DocTypeUnitRoleId],[DocTypeId],[DocDirectionId],[DocUnitRoleId],[UnitId],[IsActive])VALUES(1,15,1,2,8, 1);
INSERT INTO [DocTypeUnitRoles]([DocTypeUnitRoleId],[DocTypeId],[DocDirectionId],[DocUnitRoleId],[UnitId],[IsActive])VALUES(2,17,1,2,8, 1);
SET IDENTITY_INSERT [DocTypeUnitRoles] OFF
GO

