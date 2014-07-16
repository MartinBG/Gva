print 'Insert EmailTypes'
GO 

SET IDENTITY_INSERT [dbo].[EmailTypes] ON 

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (1, N'CorrespondentEmail', N'CorrespondentEmail', N'Известяване на кореспондент', N'Уеб порталът е достъпен на адрес: @@Param1.')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (2, N'ReceiveConfirmationEmail', N'ReceiptAcknowledgedEmail', N'Потвърждаване за получаване на заявление към портала на "Държавна комисия по хазарта"', N'Вашето заявление, подадено през портала на "Държавна комисия по хазарта", е потвърдено.')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (3, N'ReceiveConfirmationEmail', N'ReceiptNotAcknowledgedEmail', N'Потвърждаване за получаване на заявление към портала на "Държавна комисия по хазарта"', N'Вашето заявление, подадено през портала на "Държавна комисия по хазарта", е отхвърлено.')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (4, N'ElectronicServiceStageChanged', N'ElectronicServiceStageChanged', N'Смяна на етап на преписка @@Param1 към "Държавна комисия по хазарта"', N'Обработката на вашата преписка @@Param1 преминава в нов етап: @@Param2.')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (5, N'DocAssigned', N'DocAssigned', N'Към Вас е насочен документ от преписка @@Param1 - @@Param2 : @@Param3', N'Към Вас е насочен документ @@Param1 - @@Param2 : @@Param3 от преписка @@Param4 (Кореспондент: @@Param5) от @@Param6, достъпна на адрес: @@Param7')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (6, N'DocAssigned2', N'DocAssigned2', N'Към Вас е насочена преписка @@Param1 - @@Param2 : @@Param3', N'Към Вас е насочена преписка @@Param1 - @@Param2 : @@Param3 (Кореспондент: @@Param4) от @@Param5, достъпна на адрес: @@Param6')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (7, N'ResolutionOrTaskAssigned', N'ResolutionOrTaskAssigned', N'Наложена резолюция/задача към преписка @@Param1 - @@Param2 : @@Param3', N'Имате разпределена резолюция/задача към документ @@Param1 - @@Param2 : @@Param3 от преписка @@Param4 (Кореспондент: @@Param5) от @@Param6, достъпна на адрес: @@Param7')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (8, N'ResolutionOrTaskAssigned2', N'ResolutionOrTaskAssigned2', N'Наложена резолюция/задача към преписка @@Param1 - @@Param2 : @@Param3', N'Имате разпределена резолюция/задача към преписка @@Param1 - @@Param2 : @@Param3 (Кореспондент: @@Param4) от @@Param5, достъпна на адрес: @@Param6')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (9, N'WorkflowActionRequest', N'WorkflowActionRequest', N'Искане за съгласуване по документ @@Param1 - @@Param2 : @@Param3', N'Към Вас е изпратено искане за съгласуване от @@Param4 по документ @@Param1 - @@Param2 : @@Param3, към преписка @@Param5, достъпен на адрес: @@Param6')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (10, N'WorkflowAction', N'WorkflowAction', N'Съгласуване на документ @@Param1 - @@Param2 : @@Param3', N'Имате отговор на вашето искане за съгласуване по документ @@Param1 - @@Param2 : @@Param3, към преписка @@Param4, достъпен на адрес: @@Param5')

SET IDENTITY_INSERT [dbo].[EmailTypes] OFF
GO
