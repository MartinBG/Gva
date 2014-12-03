﻿PRINT 'Insert Countries'
GO

SET IDENTITY_INSERT Gva.[dbo].[Countries] ON
GO

INSERT INTO Gva.[dbo].[Countries]
    ([CountryId], [Code], [Name], [IsActive])
VALUES
(1, 'AW', N'Аруба', 1),
(2, 'AF', N'Афганистан', 1),
(3, 'AO', N'Ангола', 1),
(4, 'AI', N'Ангуила', 1),
(5, 'AL', N'Албания', 1),
(6, 'AD', N'Андора', 1),
(7, 'AN', N'Нидерландски Антили', 1),
(8, 'AE', N'ОАЕ', 1),
(9, 'AR', N'Аржентина', 1),
(10, 'AM', N'Арменска република', 1),
(11, 'AS', N'Източна Самоа САЩ', 1),
(12, 'AQ', N'Антарктида', 1),
(13, 'TF', N'Френски южни тер.', 1),
(14, 'AG', N'Антигуа и Барбуда', 1),
(15, 'AU', N'Австралийски съюз', 1),
(16, 'AT', N'Австрия', 1),
(17, 'AZ', N'Азербайджан', 1),
(18, 'BI', N'Бурунди', 1),
(19, 'BE', N'Белгия', 1),
(20, 'BJ', N'Бенин', 1),
(21, 'BF', N'Буркина Фасо', 1),
(22, 'BD', N'Бангладеш', 1),
(23, 'BG', N'България', 1),
(24, 'BH', N'Бахрейн', 1),
(25, 'BS', N'Бахамски острови', 1),
(26, 'BA', N'Босна и Херцеговина', 1),
(27, 'BY', N'Беларус', 1),
(28, 'BZ', N'Белиз', 1),
(29, 'BM', N'Бермуда', 1),
(30, 'BO', N'Боливия', 1),
(31, 'BR', N'Бразилия', 1),
(32, 'BB', N'Барбадос', 1),
(33, 'BN', N'Бруней Даруссалам', 1),
(34, 'BT', N'Кралство Бутан', 1),
(35, 'BW', N'Ботсуана', 1),
(36, 'CF', N'Центр.афганистанска репуб', 1),
(37, 'CA', N'Канада', 1),
(38, 'CC', N'Кокосови острови', 1),
(39, 'CH', N'Швейцария', 1),
(40, 'CL', N'Чили', 1),
(41, 'CN', N'Китай', 1),
(42, 'CI', N'Кот дИвоар', 1),
(43, 'CM', N'Камерун', 1),
(44, 'CD', N'Конго, Демократична република', 1),
(45, 'CG', N'Конго, Народна република', 1),
(46, 'CK', N'Острови Кук', 1),
(47, 'CO', N'Колумбия', 1),
(48, 'KM', N'Коморски острови', 1),
(49, 'CV', N'Кабо Верде', 1),
(50, 'CR', N'Коста Рика', 1),
(51, 'CU', N'Куба', 1),
(52, 'CX', N'Рождественски острови', 1),
(53, 'KY', N'Кайманови острови', 1),
(54, 'CY', N'Кипър', 1),
(55, 'CZ', N'Чешка Република', 1),
(56, 'DE', N'Германия', 1),
(57, 'DJ', N'Джибути', 1),
(58, 'DM', N'Доминика', 1),
(59, 'DK', N'Дания', 1),
(60, 'DO', N'Доминиканска република', 1),
(61, 'DZ', N'Алжир', 1),
(62, 'EC', N'Еквадор', 1),
(63, 'EG', N'Египет', 1),
(64, 'ER', N'Еритрея', 1),
(65, 'EH', N'Западна Сахара', 1),
(66, 'ES', N'Испания', 1),
(67, 'EE', N'Естония', 1),
(68, 'ET', N'Етиопия', 1),
(69, 'FI', N'Финландия', 1),
(70, 'FJ', N'Фиджи', 1),
(71, 'FK', N'Фолкл. Малв. Острови', 1),
(72, 'FR', N'Франция', 1),
(73, 'FO', N'Ферьорски острови', 1),
(74, 'FM', N'Микронезия', 1),
(75, 'GA', N'Габон', 1),
(76, 'GB', N'Великобритания', 1),
(77, 'GE', N'Грузия', 1),
(78, 'GH', N'Гана', 1),
(79, 'GI', N'Гибралтар', 1),
(80, 'GN', N'Гвинея', 1),
(81, 'GP', N'Гваделупа', 1),
(82, 'GM', N'Гамбия', 1),
(83, 'GW', N'Гвинея - Бисау' , 1),
(84, 'GQ', N'Екваториална Гвинея', 1),
(85, 'GR', N'Гърция', 1),
(86, 'GD', N'Гренада', 1),
(87, 'GL', N'Гренландия', 1),
(88, 'GT', N'Гватемала', 1),
(89, 'GF', N'Гвиана', 1),
(90, 'GU', N'Гуам', 1),
(91, 'GY', N'Гаяна', 1),
(92, 'HK', N'Хонг Конг', 1),
(93, 'HM', N'Хърд и Макд. Острови', 1),
(94, 'HN', N'Хондурас', 1),
(95, 'HT', N'Хаити', 1),
(96, 'HU', N'Унгария', 1),
(97, 'ID', N'Индонезия', 1),
(98, 'IN', N'Индия', 1),
(99, 'IO', N'Британия в Индийски океан', 1),
(100, 'IE', N'Ирландия', 1),
(101, 'IR', N'Иран', 1),
(102, 'IQ', N'Ирак', 1),
(103, 'IS', N'Исландия', 1),
(104, 'IL', N'Израел', 1),
(105, 'IT', N'Италия', 1),
(106, 'JM', N'Ямайка', 1),
(107, 'JO', N'Йордания', 1),
(108, 'JP', N'Япония', 1),
(109, 'KZ', N'Казахстан', 1),
(110, 'KE', N'Кения', 1),
(111, 'KG', N'Киргистан', 1),
(112, 'KH', N'Камбоджа', 1),
(113, 'KI', N'Кирибати', 1),
(114, 'KN', N'Сейнт Китс и Нейвис', 1),
(115, 'KR', N'Република Корея', 1),
(116, 'KW', N'Кувейт', 1),
(117, 'LA', N'Лаос', 1),
(118, 'LB', N'Ливан', 1),
(119, 'LR', N'Либерия', 1),
(120, 'LY', N'Либия', 1),
(121, 'LC', N'Санта Лучия', 1),
(122, 'LI', N'Лихтенщайн', 1),
(123, 'LK', N'Шри Ланка', 1),
(124, 'LS', N'Лесото', 1),
(125, 'LT', N'Литва', 1),
(126, 'LU', N'Люксембург', 1),
(127, 'LV', N'Латвия', 1),
(128, 'MO', N'Макао', 1),
(129, 'MA', N'Мароко', 1),
(130, 'MC', N'Монако', 1),
(131, 'MD', N'Молдова', 1),
(132, 'MG', N'Мадагаскар', 1),
(133, 'MV', N'Малдиви', 1),
(134, 'MX', N'Мексико', 1),
(135, 'MH', N'Маршалски острови', 1),
(136, 'MK', N'Македония', 1),
(137, 'ML', N'Мали', 1),
(138, 'MT', N'Малта', 1),
(139, 'MM', N'Съюз Мианмар', 1),
(140, 'MN', N'Монголия', 1),
(141, 'MP', N'Мариански острови', 1),
(142, 'MZ', N'Мозамбик', 1),
(143, 'MR', N'Мавритания', 1),
(144, 'MS', N'Монсерат', 1),
(145, 'MQ', N'Мартиника', 1),
(146, 'MU', N'Мавриций', 1),
(147, 'MW', N'Малави', 1),
(148, 'MY', N'Малайзия', 1),
(149, 'YT', N'Майоте', 1),
(150, 'NA', N'Намибия', 1),
(151, 'NC', N'Нова Каледония', 1),
(152, 'NE', N'Нигер', 1),
(153, 'NF', N'Норфолк', 1),
(154, 'NG', N'Нигерия', 1),
(155, 'NI', N'Никарагуа', 1),
(156, 'NU', N'Ниуе', 1),
(157, 'NL', N'Нидерландия', 1),
(158, 'NO', N'Норвегия', 1),
(159, 'NP', N'Непал', 1),
(160, 'NR', N'Науру', 1),
(161, 'NZ', N'Нова Зеландия', 1),
(162, 'OM', N'Оман', 1),
(163, 'PK', N'Пакистан', 1),
(164, 'PA', N'Панама', 1),
(165, 'PN', N'Питкерн', 1),
(166, 'PE', N'Перу', 1),
(167, 'PH', N'Филипини', 1),
(168, 'PW', N'Палау', 1),
(169, 'PG', N'ПАПУА - Нова Гвинея', 1),
(170, 'PL', N'Полша', 1),
(171, 'PR', N'Пуерто Рико', 1),
(172, 'KP', N'КНДР', 1),
(173, 'PT', N'Португалия', 1),
(174, 'PY', N'Парагвай', 1),
(175, 'PS', N'Палестински територии', 1),
(176, 'PF', N'Френска Полинезия', 1),
(177, 'QA', N'Катар', 1),
(178, 'RE', N'Реюнион', 1),
(179, 'RO', N'Румъния', 1),
(180, 'RU', N'Руска федерация', 1),
(181, 'RW', N'Руанда', 1),
(182, 'SA', N'Саудитска Арабия', 1),
(183, 'SD', N'Судан', 1),
(184, 'SN', N'Сенегал', 1),
(185, 'SG', N'Сингапур', 1),
(186, 'GS', N'Южна Грузия', 1),
(187, 'SH', N'Остров Св.Елена', 1),
(188, 'SJ', N'Свалбард и Ян Майе', 1),
(189, 'SB', N'Соломонови острови', 1),
(190, 'SL', N'Сиера Леоне', 1),
(191, 'SV', N'Салвадор', 1),
(192, 'SM', N'Сан Марино', 1),
(193, 'SO', N'Сомалия', 1),
(194, 'PM', N'Сен Пиер и Микелон', 1),
(195, 'ST', N'Сао Томе и Принсипи', 1),
(196, 'SR', N'Суринам', 1),
(197, 'SK', N'Словашка Република', 1),
(198, 'SI', N'Словения', 1),
(199, 'SE', N'Швеция', 1),
(200, 'SZ', N'Свазиленд', 1),
(201, 'SC', N'Сейшелски острови', 1),
(202, 'SY', N'Сирия', 1),
(203, 'TC', N'О-в Търкс и Кайкос' , 1),
(204, 'TD', N'Република Чад', 1),
(205, 'TG', N'Того', 1),
(206, 'TH', N'Тайланд', 1),
(207, 'TJ', N'Таджикистан', 1),
(208, 'TK', N'Токелау', 1),
(209, 'TM', N'Туркменистан', 1),
(210, 'TP', N'Източен Тимор', 1),
(211, 'TO', N'Тонга', 1),
(212, 'TT', N'Тринидад и Тобаго', 1),
(213, 'TN', N'Тунис', 1),
(214, 'TR', N'Турция', 1),
(215, 'TV', N'Тувалу', 1),
(216, 'TW', N'Тайван', 1),
(217, 'TZ', N'Танзания', 1),
(218, 'UG', N'Уганда', 1),
(219, 'UA', N'Украйна', 1),
(220, 'UM', N'САЩ - второст.територ.' , 1),
(221, 'UY', N'Уругвай', 1),
(222, 'US', N'САЩ', 1),
(223, 'UZ', N'Узбекистан', 1),
(224, 'VA', N'Ватикана', 1),
(225, 'VC', N'Сейнт Винс. И Грен.', 1),
(226, 'VE', N'Венецуела', 1),
(227, 'VG', N'Вирж. О-ви (Великобрит.)', 1),
(228, 'VI', N'Виржински о-ви (Амер.)' , 1),
(229, 'VN', N'Виетнам', 1),
(230, 'VU', N'Вануату', 1),
(231, 'WF', N'Уолис и Футуна', 1),
(232, 'WS', N'Самоа', 1),
(233, 'YE', N'Йеменска Република', 1),
(234, 'ZA', N'Южноафр. Република', 1),
(235, 'ZM', N'Замбия', 1),
(236, 'ZW', N'Зимбабве', 1),
(237, 'RS', N'Сърбия', 1),
(238, 'ME', N'Черна гора', 1),
(239, 'XK', N'Косово', 1),
(240, 'ZX', N'Непоказано', 1),
(241, 'BL', N'Сейнт Бартолемей', 1),
(242, 'CE', N'ЧСФР', 1),
(243, 'GG', N'Гърнси', 1),
(244, 'IM', N'Остров Ман', 1),
(245, 'JE', N'Джърси', 1),
(246, 'MF', N'Сейнт Мартин', 1),
(247, 'XY', N'Бивша Югославия', 1),
(248, 'XC', N'Бивша Чехословакия', 1),
(249, 'XU', N'Бивша СССР', 1),
(250, 'XS', N'Бивша Сърбия и Черна гора', 1),
(251, 'HR', N'Република Хърватия', 1)
GO
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO