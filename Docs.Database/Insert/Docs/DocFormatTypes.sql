SET IDENTITY_INSERT DocFormatTypes ON

INSERT INTO DocFormatTypes(DocFormatTypeId, Name, Alias, IsActive) VALUES (1, N'Електронен', N'Electronic', 1)
INSERT INTO DocFormatTypes(DocFormatTypeId, Name, Alias, IsActive) VALUES (2, N'Електронен с хартия', N'ElectronicWithPaper', 1)
INSERT INTO DocFormatTypes(DocFormatTypeId, Name, Alias, IsActive) VALUES (3, N'Хартиен', N'Paper', 1)

SET IDENTITY_INSERT DocFormatTypes OFF
GO 
