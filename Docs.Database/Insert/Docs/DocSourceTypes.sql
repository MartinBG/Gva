SET IDENTITY_INSERT DocSourceTypes ON

INSERT INTO DocSourceTypes(DocSourceTypeId, Name, Alias, IsActive) VALUES (1, N'Интернет', N'Internet', 1)
INSERT INTO DocSourceTypes(DocSourceTypeId, Name, Alias, IsActive) VALUES (2, N'Подадено на гише', N'Manual', 1)

SET IDENTITY_INSERT DocSourceTypes OFF
GO 
