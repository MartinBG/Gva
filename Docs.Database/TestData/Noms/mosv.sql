SET IDENTITY_INSERT [Noms] ON

INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (104, N'Видове заявители', N'applicantTypes')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (105, N'Видове на информацията', N'informationTypes')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (106, N'Начини на подаване', N'submitTypes')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (107, N'Отговорни институции', N'institutions')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (108, N'Статуси1', N'statuses1')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (109, N'Статуси2', N'statuses2')

INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (110, N'Начин на предоставяне', N'provideTypes')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (111, N'Теми', N'themes')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (112, N'Начин на плащане', N'paymentTypes')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (113, N'Причина за неразглеждане', N'denialReasons')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (114, N'Тип решение', N'decisionTypes')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (115, N'Срок на решение', N'decisionDeadlineTypes')

INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (116, N'Причини за удължаване', N'extensionReasons')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (117, N'Причини за отказ за ДОИ', N'doiDenialReasons')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (118, N'Причини за отказ от подателя', N'applicantDenialReasons')
INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (119, N'Резултат от обжалване', N'appealResults')

SET IDENTITY_INSERT [Noms] OFF
GO

INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(104,N'A',N'Журналисти',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(104,N'A1',N'Граждани',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(104,N'A2',N'НПО',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(104,N'A3',N'Други',NULL,NULL,NULL,1,NULL);
GO

INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(106,N'A',N'Ел.заявление',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(106,N'A1',N'На гише',NULL,NULL,NULL,1,NULL);
GO

INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(107,N'I1',N'Изпълнителната агенция по околна среда',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(107,N'I2',N'РИОСВ София',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(107,N'I3',N'РИОСВ Пловдив',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(107,N'I4',N'РИОСВ Варна',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(107,N'I5',N'РИОСВ Бургас',NULL,NULL,NULL,1,NULL);
GO

INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(108,N'S1',N'Не е разглеждано',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(108,N'S2',N'Решение',NULL,NULL,'acceptance',1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(108,N'S3',N'Без разглеждане',NULL,NULL,'denial',1,NULL);
GO

INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(109,N'S1',N'Не е разглеждано',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(109,N'S2',N'Предприети действия',NULL,NULL,'hasActions',1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(109,N'S3',N'Без действия',NULL,NULL,NULL,1,NULL);
GO

--Вид на информацията 
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(105,N'it1',N'Официална информация',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(105,N'it2',N'Служебна информация',NULL,NULL,NULL,1,NULL);
GO

-- Начин на предоставяне
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(110,N'pt1',N'Публикуване на информацията',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(110,N'pt2',N'Преглед на информацията - оригинал или копие',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(110,N'pt3',N'Устна справка',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(110,N'pt4',N'Копия на хартиен носител',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(110,N'pt5',N'Копия на технически носител',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(110,N'pt6',N'Форма, отговаряща на комуникативните възможности на хора със зрителни увреждания',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(110,N'pt7',N'Форма, отговаряща на комуникативните възможности на хора с увреждания на слухово-говорния апарат',NULL,NULL,NULL,1,NULL);
GO

-- Теми
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(111,N'th1',N'Упражняване на права или законни интереси',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(111,N'th2',N'Отчетност на институцията',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(111,N'th3',N'Процес на вземане на решения',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(111,N'th4',N'Изразходване на публични стредства',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(111,N'th5',N'Контролна дейност на администрацията',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(111,N'th6',N'Предотвратяване или разкриване на корупция или нередности',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(111,N'th7',N'Проекти на нормативни актове',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(111,N'th8',N'Други теми',NULL,NULL,NULL,1,NULL);
GO


--112 Начин на плащане
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(112,N'pts1',N'В институцията (в брой)',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(112,N'pts2',N'По банков път',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(112,N'pts3',N'Друг начин',NULL,NULL,NULL,1,NULL);
GO

--113 Причина за неразглеждане
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(113,N'dr1',N'Не се съдържат данни за Трите имена/наименование',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(113,N'dr2',N'Не се съдържат данни за Седалището на заявителя',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(113,N'dr3',N'Не се съдържат данни за Точно описание на исканата информация',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(113,N'dr4',N'Не се съдържат данни за Адрес за кореспонденция',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(113,N'dr5',N'Други данни',NULL,NULL,NULL,1,NULL);
GO

--114 Тип решение
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(114,N'dt1',N'Решения за Предоставяне на свободен ДОИ',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(114,N'dt2',N'Решения за Предоставяне на частичен ДОИ',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(114,N'dt3',N'Решения за Предоставяне на ДОИ при наличие на надделяващ обществен интерес',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(114,N'dt4',N'Решения за Препращане на заявлението, когато органът не разполага с исканата информация, но знае за нейното местонахождение',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(114,N'dt5',N'Решения за Уведомление на заявителя за липса на исканата обществена информация',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(114,N'dt6',N'Решения за Отказ за предоставяне на ДОИ',NULL,NULL,NULL,1,NULL);
GO

--115 Срок на решение
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(115,N'ddt1',N'Веднага',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(115,N'ddt2',N'В 14 дневен срок',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(115,N'ddt3',N'В законноустановения срок след удължаването му',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(115,N'ddt4',N'След срока',NULL,NULL,NULL,1,NULL);
GO
--
--116 Причини за удължаване
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(116,N'er1',N'Уточняване предмета на исканата информация',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(116,N'er2',N'Исканата информация е в голямо количество и е необходимо допълнително време за нейната подготовка',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(116,N'er3',N'Исканата информация се отнася до трето лице и е необходимо неговото съгласие за предоставянето й',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(116,N'er4',N'Други причини',NULL,NULL,NULL,1,NULL);
GO

--INSERT [dbo].[Noms] ([NomId], [Name], [Alias]) VALUES (117, N'Причини за отказ за ДОИ', N'doiDenialReasons')
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(117,N'ddr1',N'Исканата информация е класифицирана информация, представляваща служебна тайна',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(117,N'ddr2',N'Исканата информация е класифицирана информация, представляваща държавна тайна',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(117,N'ddr3',N'Исканата информация представлява търговска тайна, и нейното предоставяне или разпространение би довело до нелоялна конкуренция между търговци',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(117,N'ddr4',N'Достъпът засяга интересите на трето лице (фирма) и няма негово изрично писмено съгласие за предоставяне на исканата обществена информация',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(117,N'ddr5',N'Достъпът засяга интересите на трето лице (физическо лице) и няма негово изрично писмено съгласие за предоставяне на исканата обществена информация',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(117,N'ddr6',N'Исканата обществена информация е предоставена на заявителя през предходните 6 месеца',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(117,N'ddr7',N'Служебната обществена информация е свързана с оперативната подготовка на актовете на органите и няма самостоятелно значение (мнения и препоръки, изготвени от или за органа, становища и консултации)',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(117,N'ddr8',N'Служебната обществена информация съдържа мнения и позиции във връзка с настоящи или предстоящи преговори, водени от органа или от негово име, както и сведения, свързани с тях, и е подготвена от администрациите на съответните органи',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(117,N'ddr9',N'Други основания',NULL,NULL,NULL,1,NULL);
GO

--118 Причини за отказ от подателя
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(118,N'adr1',N'Заявителят не се е явил в определения срок (до 30дни)',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(118,N'adr2',N'Заявителят не е платил определените разходи',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(118,N'adr3',N'Отказ на заявителя от предоставения му достъп',NULL,NULL,NULL,1,NULL);
GO

--119 Резултат от обжалване
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(119,N'ar1',N'Признаване на решението',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(119,N'ar2',N'Изцяло отменя решението',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(119,N'ar3',N'Частично отменя решението',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(119,N'ar4',N'Изменя обжалваното решение',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(119,N'ar5',N'При обжалване на отказ – иска необходимите доказателства за това',NULL,NULL,NULL,1,NULL);
GO


--SET IDENTITY_INSERT [LotSets] ON

--INSERT INTO [LotSets]
--    ([LotSetId], [Name]         , [Alias]        )
--VALUES
--    (6         , N'Достъп'      , N'Admission'  ),
--    (7         , N'Сигнал'      , N'Signal'     ),
--    (8         , N'Предложение' , N'Suggestion' )

--SET IDENTITY_INSERT [LotSets] OFF
--GO

--SET IDENTITY_INSERT [LotSetParts] ON

--INSERT INTO [LotSetParts]
--    ([LotSetPartId], [LotSetId], [Name]       , [Alias]                 , [PathRegex]        , LotSchemaId)
--VALUES                                                                                                 
--    (71             , 6         ,'Достъп'       , 'admissionData'      , N'^admissionData$'  , null   ),
--    (72             , 7         ,'Сигнал'       , 'signalData'         , N'^signalData$'     , null   ),
--    (73             , 8         ,'Предложение'  , 'suggestionData'     , N'^suggestionData$' , null   )

--SET IDENTITY_INSERT [LotSetParts] OFF
--GO
