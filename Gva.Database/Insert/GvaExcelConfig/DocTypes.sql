﻿print 'Excel Insert DocTypes'
GO

SET IDENTITY_INSERT [DocTypes] ON
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(1,1,NULL,NULL,N'Резолюция',N'Resolution',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(2,1,NULL,NULL,N'Резолюция върху документ',N'ResolutionParentOnly',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(3,1,NULL,NULL,N'Задача',N'Task',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(4,1,NULL,NULL,N'Задача върху документ',N'TaskParentOnly',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(5,1,NULL,NULL,N'Забележка',N'Remark',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(6,100,NULL,NULL,N'Писмо',N'',0,N'',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(7,100,NULL,NULL,N'Приемно предавателен протокол',N'',0,N'',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(8,300,NULL,NULL,N'Съобщение, че получаването не се потвърждава',N'ReceiptNotAcknowledgedMessage',1,N'0010-000001',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(9,300,NULL,NULL,N'Потвърждаване за получаване',N'ReceiptAcknowledgedMessage',1,N'0010-000002',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(10,300,NULL,NULL,N'Указания за отстраняване на нередовности',N'RemovingIrregularitiesInstructions',1,N'0010-003010',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(11,300,NULL,NULL,N'Отказ за издаване на индивидуален административен акт',N'IndividualAdministrativeActRefusal',1,N'0010-000009',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(12,300,NULL,NULL,N'Отказ за разглеждане на искането по същество',N'CorrespondenceConsiderationRefusal',1,N'0010-000010',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(13,400,1,1,N'Невалидна услуга',N'InvalidService',1,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(14,201,4,4,N'Заявление',N'Request',0,N'01',N'01',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(15,201,NULL,NULL,N'Допълване на заявление',N'Request supplement',0,N'02',N'02',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(16,202,4,4,N'Издаване на оперативен лиценз на въздушен превозвач',N'R-4686',1,N'0010-004686',N'0010-004686',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(17,202,4,4,N'Издаване на свидетелство за авиационен оператор, извършващ търговски превози на пътници и товари',N'R-5090',1,N'0010-005090',N'0010-005090',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(18,202,4,4,N'Издаване на свидетелство за авиационен оператор, извършващ специализирани авиационни работи',N'R-5000',1,N'0010-005000',N'0010-005000',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(19,202,4,4,N'Издаване на свидетелство за професионално обучение на авиационен учебен център',N'R-4834',1,N'0010-004834',N'0010-004834',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(20,202,4,4,N'Издаване на свидетелство за правоспособност на авиационен персонал – пилоти',N'R-4186',1,N'0010-004186',N'0010-004186',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(21,202,4,4,N'Издаване на свидетелство за правоспособност на авиационен персонал – членове на летателния състав от екипажите на ВС, различни от пилоти',N'R-4244',1,N'0010-004244',N'0010-004244',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(22,202,4,4,N'Издаване на свидетелство за правоспособност на членовете на кабинния екипаж',N'R-4864',1,N'0010-004864',N'0010-004864',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(23,202,4,4,N'Издаване на свидетелство за правоспособност на авиационен персонал – полетни диспечери',N'R-4242',1,N'0010-004242',N'0010-004242',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(24,202,4,4,N'Издаване на свидетелство за правоспособност за техническо обслужване на самолети и хеликоптери',N'R-4240',1,N'0010-004240',N'0010-004240',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(25,202,4,4,N'Признаване на свидетелство за правоспособност на чужди граждани',N'R-4296',1,N'0010-004296',N'0010-004296',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(26,202,4,4,N'Издаване на удостоверение за одобрение за ползване на авиационен тренажор',N'R-4900',1,N'0010-004900',N'0010-004900',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(27,202,4,4,N'Издаване на разрешително за проверяващ',N'R-5144',1,N'0010-005144',N'0010-005144',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(28,202,4,4,N'Издаване на свидетелство за одобрение на организация за обучение',N'R-4860',1,N'0010-004860',N'0010-004860',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(29,202,4,4,N'Издаване на удостоверение за одобрение на организация за обучение, намираща се в друга държава',N'R-4862',1,N'0010-004862',N'0010-004862',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(30,202,4,4,N'Издаване на удостоверение за регистрация на ВС',N'R-4356',1,N'0010-004356',N'0010-004356',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(31,202,4,4,N'Издаване на свидетелство за правоспособност на ръководители на полети, на ученик-ръководители на полети, на асистент-координатори на полети и на координатори по УВД',N'R-4284',1,N'0010-004284',N'0010-004284',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(32,202,4,4,N'Издаване на свидетелство за правоспособност на инженерно-технически състав, извършващ техническо обслужване на средствата за управление на въздушното движение',N'R-4958',1,N'0010-004958',N'0010-004958',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(33,202,4,4,N'Издаване на свидетелство за преподавател към авиационни учебни центрове',N'R-4824',1,N'0010-004824',N'0010-004824',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(34,202,4,4,N'Издаване на свидетелство на доставчик на аеронавигационно обслужване',N'R-4738',1,N'0010-004738',N'0010-004738',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(35,202,4,4,N'Издаване на свидетелство за експлоатационна годност на автоматизираните системи за УВД',N'R-4764',1,N'0010-004764',N'0010-004764',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(36,202,4,4,N'Издаване на свидетелства за експлоатационна годност на навигационните съоръжения за въздушна навигация и кацане',N'R-4766',1,N'0010-004766',N'0010-004766',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(37,202,4,4,N'Издаване на удостоверение за експлоатационна годност на граждански летища',N'R-4588',1,N'0010-004588',N'0010-004588',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(38,202,4,4,N'Издаване на удостоверение за експлоатационна годност на летателни площадки',N'R-4590',1,N'0010-004590',N'0010-004590',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(39,202,4,4,N'Издаване на удостоверение за експлоатационна годност на системи и съоръжения за наземно обслужване',N'R-4614',1,N'0010-004614',N'0010-004614',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(40,202,4,4,N'Издаване на лиценз на летищен оператор',N'R-4598',1,N'0010-004598',N'0010-004598',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(41,202,4,4,N'Издаване на лиценз на оператор по наземно обслужване или самообслужване на летище',N'R-4606',1,N'0010-004606',N'0010-004606',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(42,202,4,4,N'Издаване и промяна на удостоверение за одобрение на организация за управление на постоянна летателна годност  (EASA Form 14)',N'R-5132',1,N'0010-005132',N'0010-005132',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(43,202,4,4,N'Издаване на удостоверение за летателна годност на ВС',N'R-4470',1,N'0010-004470',N'0010-004470',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(44,202,4,4,N'Издаване на удостоверение за преглед на летателна годност (EASA Form 15a)',N'R-4544',1,N'0010-004544',N'0010-004544',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(45,202,4,4,N'Издаване на удостоверение за съответствие с нормите за авиационен шум',N'R-4514',1,N'0010-004514',N'0010-004514',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(46,202,4,4,N'Издаване и поддържане на сертификат за компетентност на лица, изпълняващи задачи по проверка и контрол за сигурност в гражданското въздухоплаване',N'R-4810',1,N'0010-004810',N'0010-004810',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(47,202,4,4,N'Издаване на Разрешително за ползване на радиостанция на въздухоплавателно средство',N'R-4490',1,N'0010-004490',N'0010-004490',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(48,202,4,4,N'Издаване на Удостоверение за отписване от Регистъра на ВС',N'R-4396',1,N'0010-004396',N'0010-004396',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(49,202,4,4,N'Издаване на експортно удостоверение за летателна годност',N'R-4566',1,N'0010-004566',N'0010-004566',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(50,202,4,4,N'Издаване на Специално удостоверение за ЛГ за любителски построено ВС',N'R-4578',1,N'0010-004578',N'0010-004578',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(51,202,4,4,N'Издаване на разрешение за полет',N'R-4926',1,N'0010-004926',N'0010-004926',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(52,202,4,4,N'Одобряване на ръководен персонал (EASA Form 4)',N'R-5116',1,N'0010-005116',N'0010-005116',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(53,202,4,4,N'Одобряване на описание на организация за управление поддържането на постоянна летателна годност',N'R-5094',1,N'0010-005094',N'0010-005094',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(54,202,4,4,N'Одобряване на технически борден дневник',N'R-5096',1,N'0010-005096',N'0010-005096',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(55,202,4,4,N'Одобряване на програма (план) за техническо обслужване на ВС',N'R-5104',1,N'0010-005104',N'0010-005104',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(56,202,4,4,N'Назначаване на S-Mode код (24 битов адрес на ВС)',N'R-4378',1,N'0010-004378',N'0010-004378',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(57,202,4,4,N'Вписване в свидетелство за правоспособност на авиационен персонал - пилоти',N'R-5178',1,N'0010-005178',N'0010-005178',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(58,202,4,4,N'Потвърждаване и/или възстановяване на квалификационен клас в свидетелство за правоспособност на авиационен персонал - пилоти',N'R-5196',1,N'0010-005196',N'0010-005196',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(59,202,4,4,N'Вписване в свидетелство за правоспособност на авиационен персонал – членове на летателния състав от екипажите на ВС, различни от пилоти',N'R-5248',1,N'0010-005248',N'0010-005248',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(60,202,4,4,N'Потвърждаване и/или възстановяване на квалификационен клас в  свидетелство за правоспособност на авиационен персонал - членове на летателния състав от екипажите на ВС, различни от пилоти',N'R-5250',1,N'0010-005250',N'0010-005250',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(61,202,4,4,N'Вписване в свидетелство за правоспособност на членовете на кабинния екипаж',N'R-5242',1,N'0010-005242',N'0010-005242',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(62,202,4,4,N'Потвърждаване и/или възстановяване на квалификационен клас в  свидетелство за правоспособност на авиационен персонал - членове на кабинния екипаж',N'R-5244',1,N'0010-005244',N'0010-005244',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(63,202,4,4,N'Промяна на компетентен орган на свидетелства за правоспособност  на авиационния персонал',N'R-5134',1,N'0010-005134',N'0010-005134',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(64,202,4,4,N'Преобразуване на свидетелства за правоспособност на авиационен персонал, издадени от трети държави',N'R-5246',1,N'0010-005246',N'0010-005246',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(65,202,4,4,N'Вписване в свидетелство за правоспособност на ръководители на полети, ученик-ръководител полети, на асистент координатори на полети и на координатори по УВД',N'R-5160',1,N'0010-005160',N'0010-005160',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(66,202,4,4,N'Потвърждаване и/или възстановяване на квалификационен клас в  свидетелство за правоспособност на ръководители на полети, ученик-ръководител полети, на асистент координатори на полети и на координатори по УВД',N'R-5164',1,N'0010-005164',N'0010-005164',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(67,202,4,4,N'Замяна на  свидетелство за правоспособност на ръководители на полети, ученик-ръководител полети, на асистент координатори на полети и на координатори по УВД',N'R-5166',1,N'0010-005166',N'0010-005166',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(68,202,4,4,N'Вписване в свидетелство за правоспособност на инженеро-технически състав, извършващ техническо обслужване на средствата за управление на въздушното движение',N'R-5168',1,N'0010-005168',N'0010-005168',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(69,202,4,4,N'Потвърждаване и/или възстановяване на квалификационен клас в  свидетелство за правоспособност на инженеро-технически състав, извършващ техническо обслужване на средствата за управление на въздушното движение',N'R-5170',1,N'0010-005170',N'0010-005170',N'01',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(70,202,4,4,N'Замяна/подмяна/премахване на ограничения на  свидетелство за правоспособност на инженеро-технически състав, извършващ техническо обслужване на средствата за управление на въздушното движение',N'R-5218',1,N'0010-005218',N'0010-005218',N'01',14,10,1);
SET IDENTITY_INSERT [DocTypes] OFF
GO

