SET IDENTITY_INSERT AopChecklistStatuses ON

INSERT INTO AopChecklistStatuses(AopChecklistStatusId, Name, Alias, IsActive) VALUES (1, N'Нов', N'New', 1)
INSERT INTO AopChecklistStatuses(AopChecklistStatusId, Name, Alias, IsActive) VALUES (2, N'Копие', N'Copy', 1)
INSERT INTO AopChecklistStatuses(AopChecklistStatusId, Name, Alias, IsActive) VALUES (3, N'Поправка', N'Correct', 1)

SET IDENTITY_INSERT AopChecklistStatuses OFF
GO 
