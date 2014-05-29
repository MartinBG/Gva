print 'Excel Insert ElectronicServiceStages'
GO

SET IDENTITY_INSERT [ElectronicServiceStages] ON
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(1,13,N'Обработва се',NULL,N'Pending',0,0,1,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(2,13,N'Отхвърлено',NULL,N'Rejected',14,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(3,13,N'Разглежда се',NULL,N'InProcess',30,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(4,13,N'Приключило',NULL,N'Completed',30,0,0,1,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(5,14,N'Обработва се',NULL,N'Pending',0,0,1,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(6,14,N'Отхвърлено',NULL,N'Rejected',14,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(7,14,N'Разглежда се',NULL,N'InProcess',30,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(8,14,N'Приключило',NULL,N'Completed',30,0,0,1,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(9,15,N'Обработва се',NULL,N'Pending',0,0,1,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(10,15,N'Отхвърлено',NULL,N'Rejected',14,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(11,15,N'Разглежда се',NULL,N'InProcess',30,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(12,15,N'Приключило',NULL,N'Completed',30,0,0,1,1);
SET IDENTITY_INSERT [ElectronicServiceStages] OFF
GO

