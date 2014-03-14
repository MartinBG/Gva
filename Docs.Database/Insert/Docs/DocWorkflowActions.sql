SET IDENTITY_INSERT DocWorkflowActions ON

INSERT INTO DocWorkflowActions(DocWorkflowActionId, Name, Alias, IsActive) VALUES (1, N'За подпис', N'SignRequest', 1)
INSERT INTO DocWorkflowActions(DocWorkflowActionId, Name, Alias, IsActive) VALUES (2, N'За съгласуване', N'DiscussRequest', 1)
INSERT INTO DocWorkflowActions(DocWorkflowActionId, Name, Alias, IsActive) VALUES (3, N'За одобрение', N'ApprovalRequest', 1)
INSERT INTO DocWorkflowActions(DocWorkflowActionId, Name, Alias, IsActive) VALUES (4, N'За регистрация', N'RegistrationRequest', 1)

INSERT INTO DocWorkflowActions(DocWorkflowActionId, Name, Alias, IsActive) VALUES (5, N'Подпис', N'Sign', 1)
INSERT INTO DocWorkflowActions(DocWorkflowActionId, Name, Alias, IsActive) VALUES (6, N'Съгласуване', N'Discuss', 1)
INSERT INTO DocWorkflowActions(DocWorkflowActionId, Name, Alias, IsActive) VALUES (7, N'Одобрение', N'Approval', 1)
INSERT INTO DocWorkflowActions(DocWorkflowActionId, Name, Alias, IsActive) VALUES (8, N'Регистрация (авт.)', N'Registration', 1)
INSERT INTO DocWorkflowActions(DocWorkflowActionId, Name, Alias, IsActive) VALUES (9, N'Електронен подпис (авт.)', N'ElectronicSign', 1)

SET IDENTITY_INSERT DocWorkflowActions OFF
GO
