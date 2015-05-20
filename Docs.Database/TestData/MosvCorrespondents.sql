print 'Insert Correspondents'
GO 

SET IDENTITY_INSERT Correspondents ON

INSERT INTO Correspondents(
[CorrespondentId]
,[CorrespondentGroupId]
,[RegisterIndexId]
,[Email]
,[CorrespondentTypeId]
,[BgCitizenFirstName]
,[BgCitizenLastName]
,[BgCitizenUIN]
,[Alias]
,[IsActive])
values (
1,--[CorrespondentId]
3,--,[CorrespondentGroupId]
2,--,[RegisterIndexId]
'',--,[Email]
1,--,[CorrespondentTypeId]
N'Празен системен кореспондент',--,[BgCitizenFirstName]
'',--,[BgCitizenLastName]
'',--,[BgCitizenUIN]
'Empty',--,[Alias]
0)--,[IsActive])


SET IDENTITY_INSERT Correspondents OFF
GO 





