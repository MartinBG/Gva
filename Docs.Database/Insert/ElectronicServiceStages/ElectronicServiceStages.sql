SET IDENTITY_INSERT ElectronicServiceStages ON

INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(1,101,N'Етап 1',NULL,N'Stage1',0 ,0,1,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(2,101,N'Етап 2',NULL,N'Stage2',14,1,0,0,1);
INSERT INTO [ElectronicServiceStages]([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])VALUES(3,101,N'Етап 3',NULL,N'Stage3',14,1,0,1,1);

SET IDENTITY_INSERT ElectronicServiceStages OFF
GO 
