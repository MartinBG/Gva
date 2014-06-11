print 'Insert AdministrativeEmailTypes'
GO 

SET IDENTITY_INSERT [dbo].[AdministrativeEmailTypes] ON 

INSERT [dbo].[AdministrativeEmailTypes] ([AdministrativeEmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (1, N'CorrespondentEmail', N'CorrespondentEmail', N'Известяване на кореспондент', N'Уеб порталът е достъпен на адрес: @@Param1.')

INSERT [dbo].[AdministrativeEmailTypes] ([AdministrativeEmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (2, N'ReceiveConfirmationEmail', N'ReceiptAcknowledgedEmail', N'Потвърждаване за получаване', N'Вашето заявление е потвърдено.')

INSERT [dbo].[AdministrativeEmailTypes] ([AdministrativeEmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (3, N'ReceiveConfirmationEmail', N'ReceiptNotAcknowledgedEmail', N'Потвърждаване за получаване', N'Вашето заявление е отхвърлено.')

INSERT [dbo].[AdministrativeEmailTypes] ([AdministrativeEmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (4, N'ElectronicServiceStageChanged', N'ElectronicServiceStageChanged', N'Смяна на етап', N'Обработката на вашата преписка преминава в нов етап: @@Param1.')

INSERT [dbo].[AdministrativeEmailTypes] ([AdministrativeEmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (5, N'ResolutionOrTaskAssigned', N'ResolutionOrTaskAssigned', N'Наложена резолюция/задача', N'Имате разпределена резолюция/задача, достъпна на адрес: @@Param1.')

INSERT [dbo].[AdministrativeEmailTypes] ([AdministrativeEmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (6, N'WorkflowActionRequest', N'WorkflowActionRequest', N'Искане за съгласуване по документ', N'Към Вас е изпратено искане за съгласуване от @@Param1 по документ @@Param2:@@Param3, достъпен на адрес: @@Param4.')

INSERT [dbo].[AdministrativeEmailTypes] ([AdministrativeEmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (7, N'WorkflowAction', N'WorkflowAction', N'Съгласуване на документ', N'Имате отговор на вашето искане за съгласуване по документ @@Param1:@@Param2, достъпен на адрес: @@Param1.')

SET IDENTITY_INSERT [dbo].[AdministrativeEmailTypes] OFF
GO

