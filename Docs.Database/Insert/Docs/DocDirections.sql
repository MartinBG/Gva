SET IDENTITY_INSERT DocDirections ON

INSERT INTO DocDirections(DocDirectionId, Name, Alias, IsActive) VALUES (1, N'Входящ', N'Incomming', 1)
INSERT INTO DocDirections(DocDirectionId, Name, Alias, IsActive) VALUES (2, N'Вътрешен', N'Internal', 1)
INSERT INTO DocDirections(DocDirectionId, Name, Alias, IsActive) VALUES (3, N'Изходящ', N'Outgoing', 1)
INSERT INTO DocDirections(DocDirectionId, Name, Alias, IsActive) VALUES (4, N'Циркулярен', N'InternalOutgoing', 1)
                                                                  
SET IDENTITY_INSERT DocDirections OFF
GO 
