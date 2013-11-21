PRINT 'ratingSubClasses'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (21,N'Подкласове ВС за екипажи',N'ratingSubClasses');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7020,21,N'A1',N'Комуникации - Системи за оперативна гласова комуникация',NULL,7007,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7021,21,N'A2',N'Комуникации - УКВ радиовръзка "въздух-земя"',NULL,7007,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7022,21,N'A3',N'Комуникации - Преносни системи и мрежи',NULL,7007,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7023,21,N'A4',N'Комуникации - Цифрови мрежи и протоколи',NULL,7007,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7024,21,N'A5',N'Аеронавигационна фиксирана телекомуникационна мрежа',NULL,7007,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7025,21,N'A6',N'Сателитни комуникации',NULL,7007,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7026,21,N'A7',N'Системи за запис и възпроизвеждане на гласова информация',NULL,7007,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7027,21,N'A8',N'Радиорелейни линии',NULL,7007,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7028,21,N'B1',N'Системи за трасова навигация',NULL,7008,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7029,21,N'B2',N'Системи за навигация за точен подход и кацане',NULL,7008,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7030,21,N'B3',N'Системи за спътникова навигация',NULL,7008,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7031,21,N'C1',N'Първичен радиолокатор за обзор на летателните полета',NULL,7009,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7032,21,N'C2',N'Метеорологичен радиолокатор',NULL,7009,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7033,21,N'C3',N'Радиолокатор за наземно движение',NULL,7009,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7034,21,N'C4',N'Конвенционален и моноимпулсен вторичен радиолокатор',NULL,7009,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7035,21,N'C5',N'Вторичен радиолокатор, работещ в режим "S"',NULL,7009,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7036,21,N'C6',N'Системи за автоматичен зависим обзор',NULL,7009,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7037,21,N'D1',N'Системи за многорадарна обработка на данни - MRTS',NULL,7010,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7038,21,N'D2',N'Системи за изобразяване на радарна информация - ODS',NULL,7010,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7039,21,N'D3',N'Системи за преобразуване и разпространение на радарна информация - RMCDE, RFE',NULL,7010,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7040,21,N'D4',N'Системи за запис и възпроизвеждане на радарна информация - REC/PLB',NULL,7010,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7041,21,N'D5',N'Системи за обработка на полетна информация - FDP',NULL,7010,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7042,21,N'D6',N'Системи за изобразяване на спомагателна информация - IDS',NULL,7010,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7043,21,N'D7',N'Системи за обработка на аеронавигационна информация',NULL,7010,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7044,21,N'F',N'F-',NULL,7008,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
