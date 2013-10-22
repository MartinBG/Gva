print 'vwDocs'
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'vwDocs'))
DROP VIEW vwDocs
GO

CREATE VIEW vwDocs
AS

select 
 d.[DocId]
,d.[DocDirectionId]
,d.[DocEntryTypeId]
,d.[DocCasePartTypeId]
,d.[DocSubject]
,d.[DocBody]
,d.[DocStatusId]
,d.[DocSourceTypeId]
,d.[DocDestinationTypeId]
,d.[DocTypeId]
,d.[DocFormatTypeId]
,d.[DocRegisterId]
,d.[RegUri]
,d.[RegIndex]
,d.[RegNumber]
,d.[RegDate]
,d.[ExternalRegNumber]
,d.[CorrRegNumber]
,d.[CorrRegDate]
,d.[AccessCode]
,d.[AssignmentTypeId]
,d.[AssignmentDate]
,d.[AssignmentDeadline]
,d.[IsCase]
,d.[IsRegistered]
,d.[IsSigned]
,d.[LockObjectId]
,d.[ModifyDate]
,d.[ModifyUserId]
,d.[IsActive]
,d.[Version]
,de.[Alias] as DocEntryTypeAlias
,de.[Name] as DocEntryTypeName
,dd.[Alias] as DocDirectionAlias
,dd.[Name] as DocDirectionName
,dt.[Alias] as DocTypeAlias
,dt.[Name] as DocTypeName
,dt.[IsElectronicService] as DoccTypeIsElectronicService
,ds.[Alias] as DocStatusAlias
,ds.[Name] as DocStatusName
,dcp.[Alias] as DocCasePartTypeAlias
,dcp.[Name] as DocCasePartTypeName
,dt.[Name] + ': ' + d.[DocSubject] as DocName

from 
Docs d
left join DocEntryTypes de on de.DocEntryTypeId = d.DocEntryTypeId
left join DocDirections dd on dd.DocDirectionId = d.DocDirectionId
left join DocTypes dt on dt.DocTypeId = d.DocTypeId
left join DocStatuses ds on ds.DocStatusId = d.DocStatusId
left join DocCasePartTypes dcp on dcp.DocCasePartTypeId = d.DocCasePartTypeId

GO

