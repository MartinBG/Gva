print 'Insert EmailTypes'
GO 

SET IDENTITY_INSERT [dbo].[EmailTypes] ON 

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (1, N'CorrespondentEmail', N'CorrespondentEmail',
    N'Нов електронен документ по преписка @@CaseNum към портала за електронни административни услуги на "Министерство на правосъдието"',

    N'Има нов електронен документ по ваша преписка @@CaseNum към портала за електронни административни услуги на "Министерство на правосъдието", който ви изпращаме приложено.' + CHAR(13)+CHAR(10) +
    N'Документа може да бъде разгледан чрез портала на адрес: @@DocViewUrl' + CHAR(13)+CHAR(10) +
    N'Цялата преписка, включваща и приложения документ, също е достъпна за преглед през портала на адрес: @@CaseViewUrl' + CHAR(13)+CHAR(10) +
    CHAR(13)+CHAR(10) +
    N'Номер на преписка: @@CaseNum' + CHAR(13)+CHAR(10) +
    N'Код за достъп: @@AccessCode')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (2, N'ReceiptAcknowledgedEmail', N'ReceiptAcknowledgedEmail',
    N'Потвърждаване за получаване на заявление към портала за електронни административни услуги на "Министерство на правосъдието"',

    N'Вашето заявление, подадено през портала за електронни административни услуги на "Министерство на правосъдието", е потвърдено.' + CHAR(13)+CHAR(10) +
    N'Създадената преписка е достъпна за преглед през портала на адрес: @@CaseViewUrl' + CHAR(13)+CHAR(10) +
    CHAR(13)+CHAR(10) +
    N'Номер на преписка: @@CaseNum' + CHAR(13)+CHAR(10) +
    N'Код за достъп: @@AccessCode')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (3, N'ReceiptNotAcknowledgedEmail', N'ReceiptNotAcknowledgedEmail',
    N'Потвърждаване за получаване на заявление към портала за електронни административни услуги на "Министерство на правосъдието"',

    N'Вашето заявление, подадено през портала за електронни административни услуги на "Министерство на правосъдието", е отхвърлено.' + CHAR(13)+CHAR(10) +
    N'Получаването не се потвърждава на следното основание: @@Discrepancies.')

INSERT [dbo].[EmailTypes] ([EmailTypeId], [Name], [Alias], [Subject], [Body]) 
VALUES (4, N'ElectronicServiceStageChanged', N'ElectronicServiceStageChanged',
    N'Смяна на етап на преписка @@CaseNum към портала за електронни административни услуги на "Министерство на правосъдието"',

    N'Обработката на вашата преписка @@CaseNum преминава в нов етап: @@StageName.' + CHAR(13)+CHAR(10) +
    N'Цялата преписка е достъпна за преглед през портала на адрес: @@CaseViewUrl' + CHAR(13)+CHAR(10) +
    CHAR(13)+CHAR(10) +
    N'Номер на преписка: @@CaseNum' + CHAR(13)+CHAR(10) +
    N'Код за достъп: @@AccessCode')

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
