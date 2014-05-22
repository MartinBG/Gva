SET IDENTITY_INSERT AopProcedureStatuses ON

INSERT INTO AopProcedureStatuses(AopProcedureStatusId, Name, Alias, IsActive) VALUES (1, N'Вид 1', N'Type1', 1)
INSERT INTO AopProcedureStatuses(AopProcedureStatusId, Name, Alias, IsActive) VALUES (2, N'Вид 2', N'Type2', 1)

SET IDENTITY_INSERT AopProcedureStatuses OFF
GO 
