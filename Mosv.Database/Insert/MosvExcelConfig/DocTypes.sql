﻿print 'Excel Insert DocTypes'
GO

SET IDENTITY_INSERT [DocTypes] ON
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(1,1,NULL,NULL,N'Резолюция',N'Resolution',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(2,1,NULL,NULL,N'Задача',N'Task',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(3,1,NULL,NULL,N'Забележка',N'Remark',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(4,1,NULL,NULL,N'Чеклист',N'EditableDocumentFile',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(5,100,NULL,NULL,N'Писмо',N'',0,N'',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(6,100,NULL,NULL,N'Приемно предавателен протокол',N'',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(7,100,NULL,NULL,N'Становище',N'Note',0,N'',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(8,100,NULL,NULL,N'Доклад',N'Report',0,N'',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(9,100,NULL,NULL,N'Решение за предоставяне',N'ProvideDecision',0,N'',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(10,100,NULL,NULL,N'Решение за отказ',N'RejectDecision',0,N'',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(11,300,NULL,NULL,N'Съобщение, че получаването не се потвърждава',N'ReceiptNotAcknowledgedMessage',1,N'0000-000001',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(12,300,NULL,NULL,N'Потвърждаване за получаване',N'ReceiptAcknowledgedMessage',1,N'0000-000002',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(13,300,NULL,NULL,N'Указания за отстраняване на нередовности',N'RemovingIrregularitiesInstructions',1,N'0000-003010',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(14,400,1,1,N'Невалидна услуга',N'InvalidService',1,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(15,1001,7,7,N'Заявление за подаване на сигнал',N'R-6056',1,N'0010-006056',N'0010-006056',N'22',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(16,1001,7,7,N'Заявление за предоставяне на достъп до обществена информация',N'R-6016',1,N'0010-006016',N'0010-006016',N'22',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(17,1001,7,7,N'Заявление за подаване на предложение',N'R-6054',1,N'0010-006054',N'0010-006054',N'22',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(18,1002,8,8,N'Заявление за подаване на сигнал',N'R-6056',1,N'0010-006056',N'0010-006056',N'19',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(19,1002,8,8,N'Заявление за предоставяне на достъп до обществена информация',N'R-6016',1,N'0010-006016',N'0010-006016',N'19',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(20,1002,8,8,N'Заявление за подаване на предложение',N'R-6054',1,N'0010-006054',N'0010-006054',N'19',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(21,1003,9,9,N'Заявление за подаване на сигнал',N'R-6056',1,N'0010-006056',N'0010-006056',N'12',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(22,1003,9,9,N'Заявление за предоставяне на достъп до обществена информация',N'R-6016',1,N'0010-006016',N'0010-006016',N'12',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(23,1003,9,9,N'Заявление за подаване на предложение',N'R-6054',1,N'0010-006054',N'0010-006054',N'12',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(24,1004,10,10,N'Заявление за подаване на сигнал',N'R-6056',1,N'0010-006056',N'0010-006056',N'11',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(25,1004,10,10,N'Заявление за предоставяне на достъп до обществена информация',N'R-6016',1,N'0010-006016',N'0010-006016',N'11',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(26,1004,10,10,N'Заявление за подаване на предложение',N'R-6054',1,N'0010-006054',N'0010-006054',N'11',14,10,1);
SET IDENTITY_INSERT [DocTypes] OFF
GO

