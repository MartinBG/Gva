SET IDENTITY_INSERT DocStatuses ON

INSERT INTO DocStatuses(DocStatusId, Name, Alias, IsActive) VALUES (1, N'Чернова', N'Draft', 1)
INSERT INTO DocStatuses(DocStatusId, Name, Alias, IsActive) VALUES (2, N'Изготвен', N'Prepared', 1)
INSERT INTO DocStatuses(DocStatusId, Name, Alias, IsActive) VALUES (3, N'От портал', N'FromPortal', 1)
INSERT INTO DocStatuses(DocStatusId, Name, Alias, IsActive) VALUES (4, N'Обработен', N'Processed', 1)
INSERT INTO DocStatuses(DocStatusId, Name, Alias, IsActive) VALUES (5, N'Приключен', N'Finished', 1)
INSERT INTO DocStatuses(DocStatusId, Name, Alias, IsActive) VALUES (6, N'Отхвърлен', N'Canceled', 1)

SET IDENTITY_INSERT DocStatuses OFF
GO 
