print 'Excel Insert IrregularityTypes'
GO

SET IDENTITY_INSERT [IrregularityTypes] ON
INSERT INTO [IrregularityTypes]([IrregularityTypeId],[Name],[Alias],[Description])VALUES(1,N'Неизползване или използване на грешен образец',N'InvalidApplication',N'');
INSERT INTO [IrregularityTypes]([IrregularityTypeId],[Name],[Alias],[Description])VALUES(2,N'Нарушена структура на данни в електронен документ',N'NotMatchingSignature',N'');
INSERT INTO [IrregularityTypes]([IrregularityTypeId],[Name],[Alias],[Description])VALUES(3,N'Невалиден електронен подпис',N'InvalidCertificate',N'');
INSERT INTO [IrregularityTypes]([IrregularityTypeId],[Name],[Alias],[Description])VALUES(4,N'Липса на съответствие между заявено авторство в документа и автора на електронния подпис',N'MismatchingAuthorAndCertificateOwner',N'');
INSERT INTO [IrregularityTypes]([IrregularityTypeId],[Name],[Alias],[Description])VALUES(5,N'Липса на данни в подаден документ',N'MissingData',N'');
INSERT INTO [IrregularityTypes]([IrregularityTypeId],[Name],[Alias],[Description])VALUES(6,N'Липса на указан приложен документ в комплекта',N'MissingAttachedDocuments',N'');
INSERT INTO [IrregularityTypes]([IrregularityTypeId],[Name],[Alias],[Description])VALUES(7,N'Други проверки, предвидени в нормативен акт',N'OtherChecks',N'');
SET IDENTITY_INSERT [IrregularityTypes] OFF
GO

