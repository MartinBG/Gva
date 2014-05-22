print 'Excel Insert ElectronicServiceStages'
GO

SET IDENTITY_INSERT [ElectronicServiceStages] ON
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(1,11,N'Обработва се',NULL,N'Submitted',0,0,1,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(2,11,N'Отхвърлено',NULL,N'Rejected',14,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(3,11,N'Разглежда се',NULL,N'InProgress',30,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(4,11,N'Приключило',NULL,N'Finished',30,0,0,1,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(5,12,N'Обработва се',NULL,N'Submitted',0,0,1,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(6,12,N'Отхвърлено',NULL,N'Rejected',14,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(7,12,N'Разглежда се',NULL,N'InProgress',30,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(8,12,N'Приключило',NULL,N'Finished',30,0,0,1,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(9,13,N'Обработва се',NULL,N'Submitted',0,0,1,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(10,13,N'Отхвърлено',NULL,N'Rejected',14,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(11,13,N'Разглежда се',NULL,N'InProgress',30,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(12,13,N'Приключило',NULL,N'Finished',30,0,0,1,1);
SET IDENTITY_INSERT [ElectronicServiceStages] OFF
GO

