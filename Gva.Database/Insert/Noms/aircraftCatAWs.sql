PRINT 'aircraftCatAWs'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (77775,'Категория на опериране','aircraftCatAWs');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777781,77775,N'A1',N'Транспортна категория - пътници',N'Transport Category - Passengers',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777782,77775,N'A2',N'Транспортна категория - товари и/или поща',N'Transport Category - Cargo/Mail',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777783,77775,N'A3',N'Полети за спешна медицинска помощ',N'Emergency Medical Service',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777784,77775,N'AW01',N'Превоз на товари на външно окачване',N'External Load',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777785,77775,N'AW02',N'Строително-монтажни работи',N'Aerial Construction',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777786,77775,N'AW03',N'Патрулиране и наблюдение от въздуха',N'Aerial Inspection and Surveillance',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777787,77775,N'AW04',N'Фотографиране',N'Aerial Photography',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777788,77775,N'AW05',N'Геофизични изследвания и картиране',N'Aerial Surveying and Mapping',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777789,77775,N'AW06',N'Борба с пожари, вкл. горски',N'Fire Fighting incl. Forest Fire Management',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777790,77775,N'AW07',N'Авиохимически работи',N'Aerial Spraying',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777791,77775,N'AW08',N'Наблюдение и/или въздействие на времето',N'Weather Related',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777792,77775,N'AW09',N'Аварийно-спасителни работи',N'Search and Rescue',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777793,77775,N'AW10',N'Превоз на човешки органи',N'Transport Human Organs',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777794,77775,N'AW11',N'Реклама',N'Aerial Advertising',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777795,77775,N'AW12',N'Контрол над диви животни',N'Wild Life Management',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777796,77775,N'AW13',N'Други, като се посочва вида на работата',N'Others, Type is specified',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777797,77775,N'AW14',N'Учебни полети',N'Flight Training',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777798,77775,N'AW15',N'Спортни полети',N'Sporting Flights',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777799,77775,N'AW16',N'Разглеждане на забележителности или забавление',N'Aerial Sightseeing and Surveillance',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777800,77775,N'AW17',N'Теглене на безмоторни ВС',N'Glider Tower',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777801,77775,N'AW18',N'Полети за скокове с парашут',N'Parashute Jumping',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777802,77775,N'AW19',N'Други, като се посочва вида на работата',N'Others, Type is specified',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777803,77775,N'AW20',N'Облитане на радиосредства',N'Navaids Flight Inspection',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777804,77775,N'AW71',N'Специален',N'Special',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777805,77775,N'AW81',N'Частен',N'Private',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777806,77775,N'AW82',N'Частен - спортни цели',N'Private - Sport',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777807,77775,N'AW91',N'Експериментална',N'Experimental',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777808,77775,N'AW92',N'Експериментално - любителско построено',N'Experimental - Amateur Build',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777809,77775,N'AW',N'Авиационна дейност - съгласно САО',N'Aviation Works - according in AOC',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777810,77775,N'O1',N'Транспортен',N'Transport',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777811,77775,N'O103',N'Транспортна категория - пътници и спешна медицинска помощ',N'Transport Passengers and Emergency Medical Service ',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777812,77775,N'O114',N'Превоз на пътници, учебни полети',N'Transport Passengers, Training Flights',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777813,77775,N'O2',N'Общо предназначение',N'General Purpose',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777814,77775,N'O214',N'За обучение и транспортен',N'Training and Transport',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777815,77775,N'O3',N'Санитарен',N'Hospital Attendant',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777816,77775,N'O4',N'Обучение и туристически полети',N'Training and Turism',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777817,77775,N'O5',N'Учебни полети',N'Training Flights',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777818,77775,N'O6',N'Наблюдение от въздуха, реклама и учебни полети',N'Aerial Sightseeing, Advertising and Training Fligths',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777819,77775,N'O7',N'Амфибия, специална - съгл.САО',N'Amphibian, special - in accordance with CAO',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777820,77775,N'OW01',N'Превоз на товари с външно окачване и служебни пътници',N'External Load and Office Staff',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777821,77775,N'OW05',N'Геоложки проучвания',N'Geological Research',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777822,77775,N'OW07',N'Селско-стопански',N'Agricultural',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777823,77775,N'OW08',N'Рибно разузнаване',N'Fish Investigation',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777824,77775,N'OW1-14',N'Транспортен и тренировъчен',N'Transport and Training',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777825,77775,N'OW13',N'Туристически полети',N'Turistics Flights',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777826,77775,N'OW14',N'За обучение и спорт',N'Training and Sport',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777827,77775,N'OW15',N'Спортен и тренировъчен',N'Sport and Training',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777828,77775,N'Q1',N'Учебни и развлекателни полети',N'Training and Surveillance Flights',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777829,77775,N'S1',N'Транспортен, геоложки проучвания и СМР',N'Transport, Geological Research',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777830,77775,N'S11',N'Теглене на безмоторни самолети',N'Aerial Work  -Towing of gliders',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777831,77775,N'S2',N'Патрулиране, реклама, контрол диви животни, забавления',N'Inspection, Advertising, Wild Life Management, Entertiment',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777832,77775,N'S3',N'Патрулиране, реклама, полети за скокове с парашут',N'Inspection, Advertising, Parashut Jumping',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777833,77775,N'S4',N'Транспортна категория - Карго, Обучение',N'Transport Category - Kargo and Training',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777834,77775,N'S5',N'Обучение, реклама, разглеждане на забележителности от въздуха',N'Training, Advertising, Aerial Sightseeing',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777835,77775,N'S6',N'За обучение и спорт',N'Training and Sport',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777836,77775,N'S7',N'Учебни полети, разглеждане на забележителности',N'Training Flights, Aerial Sightseeing',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777837,77775,N'S8',N'Развлекателни полети, наблюдение от въздуха и обучение на пилоти, съгл.специф.в САО',N'Surveillance, Aerial Sightseeing and Pilot’s training in accordance with CAO',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777838,77775,N'S9',N'Развлекателни полети, наблюдение от въздуха, реклама и учебни полети',N'Surveillance, Sightseeing, Advertising and Training Flights',NULL,NULL,1);


GO

SET IDENTITY_INSERT [NomValues] OFF
GO
