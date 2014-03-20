/*global angular, require, moment, _*/
(function (angular, require, moment, _) {
  'use strict';

  angular.module('app').factory('docsFactory', [
    'docs',
    'docCases',
    'docStages',
    function (docs, docCases, docStages) {

      var nomenclatures = require('./nomenclatures.sample');

      var defaultDoc = {
        docId: null,
        parentDocId: null,
        docStatusId: null,
        docStatusName: null,
        docSubject: null,
        docSubjectLabel: null,
        docTypeId: null,
        docDirectionName: null,
        docDirectionId: null,
        docTypeName: null,
        regDate: null,
        regUri: null,
        regIndex: null,
        regNumber: null,
        correspondentName: null,
        corrRegNumber: null,
        corrRegDate: null,
        docSourceType: null,
        docDestinationType: null,
        assignmentType: null,
        assignmentDate: null,
        assignmentDeadline: null,
        accessCode: null,
        caseRegUri: null,
        docFormatTypeId: null,
        docCasePartTypeId: null,
        docCasePartTypeName: null,
        docTypeGroupId: null,
        docCorrespondents: [],
        docUnitsFrom: [],
        docUnitsTo: [],
        docUnitsCCopy: [],
        docUnitsImportedBy: [],
        docUnitsMadeBy: [],
        docUnitsInCharge: [],
        docUnitsControlling: [],
        docUnitsReaders: [],
        docUnitsEditors: [],
        docUnitsRegistrators: [],
        isVisibleRoleFrom: true,
        isVisibleRoleTo: true,
        isVisibleRoleImportedBy: true,
        isVisibleRoleMadeBy: false,
        isVisibleRoleCCopy: true,
        isVisibleCorrespondent: true,
        isVisibleDocSourceTypeId: true,
        isVisibleDocDestinationTypeId: true,
        isVisibleRoleInCharge: true,
        isVisibleRoleControlling: true,
        isVisibleAssignment: true,
        isVisibleRoleReaders: true,
        isVisibleRoleEditors: true,
        isVisibleRoleRegistrators: true,
        isVisibleCollapseAssignment: false,
        isVisibleCollapsePermissions: false,
        isRead: false,
        isDocIncoming: false,
        isDocInternal: false,
        isDocInternalOutgoing: false,
        isDocOutgoing: false,
        isRemark: false,
        isResolution: false,
        isTask: false,
        docBody: null,
        docFiles: [],
        docWorkflows: [],
        isVisibleDocWorkflows: true,
        docElectronicServiceStages: [],
        docRelations: [],
        docClassifications: []
      };
      
      return {
        createDoc: function (doc) {
          var nextDocId = _(docs).pluck('docId').max().value() + 1;
          var newDoc = _.assign(_.cloneDeep(defaultDoc), doc);

          var todayDate = moment();

          newDoc.docId = nextDocId;
          newDoc.docStatusId = 2;
          newDoc.docStatusName = 'Чернова';
          newDoc.docStatusAlias = 'Draft';
          newDoc.docSubjectLabel = 'Относно';
          newDoc.docSourceType = nomenclatures.docSourceType[1];
          newDoc.newassignmentType = 2;
          newDoc.assignmentDate = todayDate.format('YYYY-MM-DDTHH:mm:ss');
          newDoc.assignmentDeadline = todayDate.format('YYYY-MM-DDTHH:mm:ss');

          var docTypeAlias =
            _(nomenclatures.docType).filter({ nomValueId: newDoc.docTypeId}).first().alias;
          if (docTypeAlias === 'resolution') {
            newDoc.isResolution = true;
          }
          else if (docTypeAlias === 'task') {
            newDoc.isTask = true;
          }
          else if (docTypeAlias === 'note') {
            newDoc.isRemark = true;
          }
          else {
            var docDirectionAlias =_(nomenclatures.docDirection)
              .filter({ docDirectionId: newDoc.docDirectionId}).first().alias;

            if (docDirectionAlias === 'Incoming') {
              newDoc.isDocIncoming = true;
            }
            else if (docDirectionAlias === 'Internal') {
              newDoc.isDocInternal = true;
            }
              else if (docDirectionAlias === 'Outgoing') {
              newDoc.isDocOutgoing = true;
            }
            else if (docDirectionAlias === 'InternalOutgoing') {
              newDoc.isDocInternalOutgoing = true;
            }
          }

          //Add docRelations
          var docCaseObj;
          if (!newDoc.parentDocId) {
            docCaseObj = {
              docCaseId: newDoc.docId,
              docCase: []
            };
            docCases.push(docCaseObj);
          }
          else {
            docCaseObj =
              _(docCases).filter(function(dc) {
                return _(dc.docCase).some({ docId : parseInt(newDoc.parentDocId, 10)});
              }).first();
          }

          docCaseObj.docCase.push({
            docId: newDoc.docId,
            regDate: newDoc.regDate,
            regNumber: newDoc.regUri,
            direction: newDoc.docDirectionName,
            casePartType: newDoc.docCasePartTypeName,
            statusName: newDoc.docStatusName,
            description: newDoc.docTypeName
          });

          newDoc.docRelations = docCaseObj.docCase;

          //Add docElectronicServiceStage

          var nextDocEStageId = _(docStages).pluck('docElectronicServiceStageId').max().value() + 1;

          var eStage =
            _(nomenclatures.electronicServiceStage)
            .filter({ docTypeId: newDoc.docTypeId, isFirstByDefault: true})
            .first();

          var newDocStage = {
            docElectronicServiceStageId: nextDocEStageId,
            electronicServiceStageId: eStage.nomValueId,
            docId: newDoc.docId,
            startingDate: todayDate.startOf('day').format('YYYY-MM-DDTHH:mm:ss'),
            electronicServiceStageName: eStage.name,
            electronicServiceStageExecutors: 'Служител ДКХ',
            expectedEndingDate:
              todayDate.startOf('day').add('days', eStage.duration).format('YYYY-MM-DDTHH:mm:ss'),
            endingDate: null,
            isCurrentStage: true
          };

          docStages.push(newDocStage);

          newDoc.docElectronicServiceStages = [newDocStage];

          docs.push(newDoc);

          return newDoc;
        },

        registerDoc: function (doc) {
          var todayDate = moment();

          var nextDocId = _(docs).pluck('docId').max().value() + 1;

          var newDoc = _.assign(_.cloneDeep(defaultDoc), doc);

          newDoc.docId = nextDocId;
          newDoc.docStatusId = 2;
          newDoc.docStatusName = 'Чернова';
          newDoc.docStatusAlias = 'Draft';
          newDoc.docSubjectLabel = 'Относно';
          newDoc.docSourceType = nomenclatures.docSourceType[1];
          newDoc.regNumber = nextDocId;
          newDoc.regDate = todayDate.format('YYYY-MM-DDTHH:mm:ss');
          newDoc.regIndex = '000030';
          newDoc.newassignmentType = 2;
          newDoc.assignmentDate = todayDate.format('YYYY-MM-DDTHH:mm:ss');
          newDoc.assignmentDeadline = todayDate.format('YYYY-MM-DDTHH:mm:ss');
          newDoc.regUri = '000030-' + nextDocId + '-' + todayDate.format('YYYY-MM-DD');

          var docTypeAlias =
            _(nomenclatures.docType).filter({ nomValueId: newDoc.docTypeId}).first().alias;
          if (docTypeAlias === 'resolution') {
            newDoc.isResolution = true;
          }
          else if (docTypeAlias === 'task') {
            newDoc.isTask = true;
          }
          else if (docTypeAlias === 'note') {
            newDoc.isRemark = true;
          }
          else {
            var docDirectionAlias = _(nomenclatures.docDirection)
              .filter({ docDirectionId: newDoc.docDirectionId}).first().alias;

            if (docDirectionAlias === 'Incoming') {
              newDoc.isDocIncoming = true;
            }
            else if (docDirectionAlias === 'Internal') {
              newDoc.isDocInternal = true;
            }
              else if (docDirectionAlias === 'Outgoing') {
              newDoc.isDocOutgoing = true;
            }
            else if (docDirectionAlias === 'InternalOutgoing') {
              newDoc.isDocInternalOutgoing = true;
            }
          }

          //Add docRelations
          var docCaseObj;
          if (!newDoc.parentDocId) {
            docCaseObj = {
              docCaseId: newDoc.docId,
              docCase: []
            };
            docCases.push(docCaseObj);
          }
          else {
            docCaseObj =
              _(docCases).filter(function(dc) {
                return _(dc.docCase).some({ docId : parseInt(newDoc.parentDocId, 10)});
              }).first();
          }

          docCaseObj.docCase.push({
            docId: newDoc.docId,
            regDate: newDoc.regDate,
            regNumber: newDoc.regUri,
            direction: newDoc.docDirectionName,
            casePartType: newDoc.docCasePartTypeName,
            statusName: newDoc.docStatusName,
            description: newDoc.docTypeName
          });

          newDoc.docRelations = docCaseObj.docCase;

          //Add docElectronicServiceStage

          var nextDocEStageId = _(docStages).pluck('docElectronicServiceStageId').max().value() + 1;

          var eStage =
            _(nomenclatures.electronicServiceStage)
            .filter({ docTypeId: newDoc.docTypeId, isFirstByDefault: true})
            .first();

          var newDocStage = {
            docElectronicServiceStageId: nextDocEStageId,
            electronicServiceStageId: eStage.nomValueId,
            docId: newDoc.docId,
            startingDate: todayDate.startOf('day').format('YYYY-MM-DDTHH:mm:ss'),
            electronicServiceStageName: eStage.name,
            electronicServiceStageExecutors: 'Служител ДКХ',
            expectedEndingDate:
              todayDate.startOf('day').add('days', eStage.duration).format('YYYY-MM-DDTHH:mm:ss'),
            endingDate: null,
            isCurrentStage: true
          };

          docStages.push(newDocStage);

          newDoc.docElectronicServiceStages = [newDocStage];

          docs.push(newDoc);

          return newDoc;
        }
      };

    }
  ]);
}(angular, require, moment, _));
