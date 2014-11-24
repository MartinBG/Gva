print 'Insert RoleClassifications'
GO

SET IDENTITY_INSERT [RoleClassifications] ON

--1	Администратор
--2	Регистратор
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(1,2,2,1);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(2,2,2,2);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(3,2,2,3);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(4,2,2,4);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(5,2,2,5);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(6,2,2,6);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(7,2,2,7);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(8,2,2,8);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(9,2,2,9);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(10,2,2,10);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(11,2,2,11);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(12,2,2,12);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(13,2,2,13);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(14,2,2,14);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(15,2,2,15);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(16,2,2,16);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(17,2,2,17);

INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(18,2,3,1);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(19,2,3,2);

--3	Експерт
--4	Изготвяне на справки
--5	Съгласуващ
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(20,5,2,1);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(21,5,2,5);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(22,5,2,11);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(23,5,2,7);

INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(24,5,3,1);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(25,5,3,2);

--6	Мениджър
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(26,6,2,1);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(27,6,2,5);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(28,6,2,11);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(29,6,2,6);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(30,6,2,7);

INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(31,6,3,1);
INSERT INTO [RoleClassifications]([RoleClassificationId],[RoleId],[ClassificationId],[ClassificationPermissionId])VALUES(32,6,3,2);


SET IDENTITY_INSERT [RoleClassifications] OFF

/*
1	Четене	Read
2	Редакция	Edit
3	Регистриране	Register
4	Управление на публ. файлове	PublicFileManagement
5	Искане за одобряване, съгласуване, подписване	Management
6	Право за подписване	DocWorkflowSign
7	Право за съгласуване, одобряване	DocWorkflowDiscuss
8	Одобряване, съгласуване, подписване от др. лице	SubstituteManagement
9	Изтриване на одобряване, съгласуване, подписване	DocWorkflowManagement
10	Ел.подписване	ESign
11	Приключване	Finish
12	Сторниране	Reverse
13	Техн. редакция	EditTech
14	Техн. редакция на етап	EditTechElectronicServiceStage
15	Управление раздел на преписка	DocCasePartManagement
16	Движение на документ	DocMovement
17	Изпращане на имейл от системата	SendMail
*/
