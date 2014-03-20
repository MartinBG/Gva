SET IDENTITY_INSERT AssignmentTypes ON

INSERT INTO AssignmentTypes(AssignmentTypeId, Name, Alias, IsActive) VALUES (1, N'Със срок', N'WithDeadline', 1)
INSERT INTO AssignmentTypes(AssignmentTypeId, Name, Alias, IsActive) VALUES (2, N'Без срок', N'WithoutDeadline', 1)

SET IDENTITY_INSERT AssignmentTypes OFF
GO 
