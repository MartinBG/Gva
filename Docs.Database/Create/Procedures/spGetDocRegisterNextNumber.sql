print 'spGetDocRegisterNextNumber'
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spGetDocRegisterNextNumber'))
DROP PROCEDURE spGetDocRegisterNextNumber
GO

CREATE PROCEDURE spGetDocRegisterNextNumber
		@DocRegisterId int
AS
BEGIN
    update DocRegisters set CurrentNumber = CurrentNumber + 1 where  DocRegisterId = @DocRegisterId
    select * from DocRegisters where DocRegisterId = @DocRegisterId
END

GO