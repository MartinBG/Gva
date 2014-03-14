SET IDENTITY_INSERT DocDestinationTypes ON

INSERT INTO DocDestinationTypes(DocDestinationTypeId, Name, Alias, IsActive) VALUES (1, N'Имейл', N'Email', 1)
INSERT INTO DocDestinationTypes(DocDestinationTypeId, Name, Alias, IsActive) VALUES (2, N'По куриер', N'ByCourier', 1)

SET IDENTITY_INSERT DocDestinationTypes OFF
GO 
