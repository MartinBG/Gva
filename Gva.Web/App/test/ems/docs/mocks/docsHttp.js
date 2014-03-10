/*global angular, _, require, jQuery, moment*/
(function (angular, _, require, jQuery, moment) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

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
      docUnitsRoleReaders: [],
      docUnitsEditors: [],
      docUnitsRoleRegistrators: [],
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

    $httpBackendConfiguratorProvider
        .when('GET','/api/docs?'+
          'filter&fromDate&toDate&regUri&docName&'+
          'docTypeId&docStatusId&corrs&units&hasLot&isCase',
        function ($params, docs, applicationsFactory) {

          var searchParams = _.cloneDeep($params);
          delete searchParams.hasLot;
          delete searchParams.isCase;
          delete searchParams.fromDate;
          delete searchParams.toDate;
          delete searchParams.filter;

          var result = docs;

          if (!!$params.filter) {
            if ($params.filter === 'current'){
              result = _(result).filter(function (doc) {
                return doc.docStatusId !== 4 && doc.docStatusId !==5;
              });
            }
            else if ($params.filter === 'finished'){
              result = _(result).filter(function (doc) {
                return doc.docStatusId === 4;
              });
            }
            else if ($params.filter === 'draft'){
              result = _(result).filter(function (doc) {
                return doc.docStatusId === 1;
              });
            }
            else if ($params.filter === 'portal'){
              result = _(result).filter(function (doc) {
                return !!doc.docSourceType && doc.docSourceType.nomValueId === 1;
              });
            }
          }

          result = _(result).filter(function (doc) {

            if ($params.hasLot && $params.hasLot.toLowerCase() === 'true') {
              if (!applicationsFactory.getByDocId(doc.docId)) {
                return false;
              }
            }
            else if ($params.hasLot && $params.hasLot.toLowerCase() === 'false') {
              if (applicationsFactory.getByDocId(doc.docId)) {
                return false;
              }
            }

            if ($params.isCase && $params.isCase.toLowerCase() === 'true') {
              if (!!doc.parentDocId) {
                return false;
              }
            }
            else if ($params.isCase && $params.isCase.toLowerCase() === 'false') {
              if (!doc.parentDocId) {
                return false;
              }
            }

            if ($params.fromDate) {
              if (moment(doc.regDate).startOf('day') < moment($params.fromDate)) {
                return false;
              }
            }

            if ($params.toDate) {
              if (moment($params.toDate) < moment(doc.regDate).startOf('day')) {
                return false;
              }
            }

            var isMatch = true;

            _.forOwn(searchParams, function (value, param) {
              if (!value || param === 'exact') {
                return;
              }

              if (searchParams.exact) {
                isMatch =
                  isMatch && doc[param] && doc[param] === searchParams[param];
              } else {
                isMatch =
                  isMatch && doc[param] && doc[param].toString().indexOf(searchParams[param]) >= 0;
              }

              //short circuit forOwn if not a match
              return isMatch;
            });

            return isMatch;
          })
          .value();

          return [200, result];
        })
        .when('POST', '/api/docs/new/create',
        function ($jsonData, docs, docCases, docStages) {

          if (!$jsonData) {
            return [400];
          }

          var nextDocId = _(docs).pluck('docId').max().value() + 1;
          var newDoc = _.assign(_.cloneDeep(defaultDoc), $jsonData);

          var todayDate = moment();

          newDoc.docId = nextDocId;
          newDoc.docStatusId = 2;
          newDoc.docStatusName = 'Чернова';
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

            if (docDirectionAlias === 'Incomming') {
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

          return [200, { docId: newDoc.docId }];
        })
        .when('POST', '/api/docs/new/register',
        function ($jsonData, docs, docCases, docStages) {
          if (!$jsonData) {
            return [400];
          }

          var todayDate = moment();

          var nextDocId = _(docs).pluck('docId').max().value() + 1;

          var newDoc = _.assign(_.cloneDeep(defaultDoc), $jsonData);

          newDoc.docId = nextDocId;
          newDoc.docStatusId = 2;
          newDoc.docStatusName = 'Чернова';
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

            if (docDirectionAlias === 'Incomming') {
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

          return [200, { docId : newDoc.docId, regUri: newDoc.regUri }];
        })
        .when('GET', '/api/docs/:docId',
        function ($params, docs) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          return [200, doc];
        })
        .when('POST', '/api/docs/:docId',
        function ($params, $jsonData, $filter, docs) {
          var docId = parseInt($params.docId, 10),
            docIndex = docs.indexOf($filter('filter')(docs, { docId: docId })[0]);

          if (docIndex === -1) {
            return [400];
          }

          docs[docIndex] = $jsonData;

          return [200];
        })
        .when('POST', '/api/docs/:docId/nextStatus',
        function ($params, docs) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          var currentStatus = _(nomenclatures.docStatus)
              .filter({ nomValueId: doc.docStatusId })
              .first();

          if (currentStatus.alias === 'Draft' ||
              currentStatus.alias === 'Prepared' ||
              currentStatus.alias === 'Processed') {
            var newStatus = _(nomenclatures.docStatus)
              .filter({ nomValueId: currentStatus.nomValueId + 1 })
              .first();

            doc.docStatusId = newStatus.nomValueId;
            doc.docStatusName = newStatus.name;
          }

          return [200, { docId: doc.docId }];
        })
        .when('POST', '/api/docs/:docId/reverseStatus',
        function ($params, docs) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          var currentStatus = _(nomenclatures.docStatus)
              .filter({ nomValueId: doc.docStatusId })
              .first();

          var newStatus;
          if (currentStatus.alias === 'Prepared' ||
              currentStatus.alias === 'Processed' ||
              currentStatus.alias === 'Finished') {
            newStatus = _(nomenclatures.docStatus)
              .filter({ nomValueId: currentStatus.nomValueId - 1 })
              .first();

            doc.docStatusId = newStatus.nomValueId;
            doc.docStatusName = newStatus.name;
          }
          else if (currentStatus.alias === 'Canceled') {
            newStatus = _(nomenclatures.docStatus)
              .filter({ alias: 'Processed' })
              .first();

            doc.docStatusId = newStatus.nomValueId;
            doc.docStatusName = newStatus.name;
          }

          return [200, { docId: doc.docId }];
        })
        .when('POST', '/api/docs/:docId/cancelStatus',
        function ($params, docs) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          var cancelStatus = _(nomenclatures.docStatus)
              .filter({ alias: 'Canceled' })
              .first();

          doc.docStatusId = cancelStatus.nomValueId;
          doc.docStatusName = cancelStatus.name;

          return [200, { docId: doc.docId }];
        })
        .when('POST', '/api/docs/:docId/setRegUri',
        function ($params, docs) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          if (!doc.regUri) {
            doc.regUri = '000030-' + doc.docId + '-' + moment().format('YYYY-MM-DD');
          }

          return [200, { docId: doc.docId }];
        })
        .when('POST', '/api/docs/:docId/setCasePart',
        function ($params, $jsonData, docs, docCases) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          var newDocCasePartType = _(nomenclatures.docCasePartType)
            .filter({ docCasePartTypeId: $jsonData.docCasePartTypeId })
            .first();

          if (!doc || !newDocCasePartType) {
            return [400];
          }

          doc.docCasePartTypeId = newDocCasePartType.docCasePartTypeId;
          doc.docCasePartTypeName = newDocCasePartType.name;

          var docCase = _(docCases).filter(function (item) {
            return _(item.docCase).any({ docId: doc.docId });
          }).first().docCase;

          var docItem = _(docCase).filter({ docId: doc.docId }).first();
          docItem.casePartType = newDocCasePartType.name;

          return [200, { docId: doc.docId }];
        });
  });
}(angular, _, require, jQuery, moment));
