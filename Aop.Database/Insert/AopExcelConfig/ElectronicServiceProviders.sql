print 'Excel Insert ElectronicServiceProviders'
GO

SET IDENTITY_INSERT [ElectronicServiceProviders] ON
INSERT INTO [ElectronicServiceProviders]([ElectronicServiceProviderId], [Code], [Name], [Bulstat], [Alias], [EndPointAddress], [IsActive]) VALUES(1,N'01',N'АОП',N'131236380',N'aop','',1);
SET IDENTITY_INSERT [ElectronicServiceProviders] OFF
GO

