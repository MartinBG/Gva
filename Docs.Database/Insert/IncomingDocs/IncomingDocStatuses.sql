print 'Insert IncomingDocStatuses'
GO 

SET IDENTITY_INSERT IncomingDocStatuses ON

INSERT INTO IncomingDocStatuses(IncomingDocStatusId, Name, Alias) VALUES (1, N'В процес на обработка', N'Pending')
INSERT INTO IncomingDocStatuses(IncomingDocStatusId, Name, Alias) VALUES (2, N'Регистриран', N'Registered')
INSERT INTO IncomingDocStatuses(IncomingDocStatusId, Name, Alias) VALUES (3, N'Отказан', N'NotRegistered')
INSERT INTO IncomingDocStatuses(IncomingDocStatusId, Name, Alias) VALUES (4, N'Не може да се обработи поради възникнала грешка', N'Incorrect')

SET IDENTITY_INSERT IncomingDocStatuses OFF
GO 
