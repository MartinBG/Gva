/*global angular, _, jQuery*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

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
      docUnits: [],
      docUnitsFrom: [],
      docUnitsTo: [],
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
      docBody: null,
      privateDocFiles: [],
      publicDocFiles: [],
      docWorkflows: [],
      isVisibleDocWorkflows: true,
      docElectronicServiceStages: [],
      docRelations: [],
      docClassifications: []
    };

    $httpBackendConfiguratorProvider
        .when('GET',
        '/api/docs?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units&docIds&hasLot',
        function ($params, docs, applicationsFactory) {

          var searchParams = _.cloneDeep($params);
          delete searchParams.docIds;
          delete searchParams.hasLot;

          var docIdsArray = !!$params.docIds ? $params.docIds.split(',') : null;

          var result = _(docs).filter(function (doc) {

            if (docIdsArray && !_.contains(docIdsArray, doc.docId.toString())) {
              return false;
            }

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
        function ($jsonData, docs, docCases) {

          if (!$jsonData) {
            return [400];
          }

          var today = new Date();
          var nextDocId = _(docs).pluck('docId').max().value() + 1;
          var newDoc = _.assign(_.cloneDeep(defaultDoc), $jsonData);
          delete newDoc.numberOfDocs;

          newDoc.docId = nextDocId;
          newDoc.docStatusId = 2;
          newDoc.docStatusName = 'Чернова';
          newDoc.docSubjectLabel = 'Относно';
          newDoc.newassignmentType = 2;
          newDoc.assignmentDate = new Date(today.getTime() + (48 * 60 * 60 * 1000));
          newDoc.assignmentDeadline = new Date(today.getTime() + (48 * 60 * 60 * 1000));

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
              _(docCases).filter({ docCaseId: parseInt(newDoc.parentDocId, 10)}).first();
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

          docs.push(newDoc);

          return [200, { docId: newDoc.docId }];
        })
      .when('POST', '/api/docs/new/register',
        function ($jsonData, docs, docCases) {
          if (!$jsonData) {
            return [400];
          }

          var registeredDocIds = [];

          var today = new Date();
          var docsNumber = !!$jsonData.numberOfDocs ? $jsonData.numberOfDocs : 1;
          var newDoc;

          var findCase = function(dc) {
            return _(dc.docCase).some({ docId : parseInt(newDoc.parentDocId, 10)});
          };

          for (var i = 0; i < docsNumber; i++) {
            var nextDocId = _(docs).pluck('docId').max().value() + 1;

            newDoc = _.assign(_.cloneDeep(defaultDoc), $jsonData);
            delete newDoc.numberOfDocs;

            newDoc.docId = nextDocId;
            newDoc.docStatusId = 2;
            newDoc.docStatusName = 'Чернова';
            newDoc.docSubjectLabel = 'Относно';
            newDoc.regNumber = nextDocId;
            newDoc.regDate = new Date();
            newDoc.regIndex = '000030';
            newDoc.newassignmentType = 2;
            newDoc.assignmentDate = new Date(today.getTime() + (48 * 60 * 60 * 1000));
            newDoc.assignmentDeadline = new Date(today.getTime() + (48 * 60 * 60 * 1000));
            newDoc.regUri = '000030-' + nextDocId + '-05.01.2014';

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
                _(docCases).filter(findCase)
                .first();
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

            docs.push(newDoc);

            registeredDocIds.push(newDoc.docId);
          }

          return [200, { docIds : registeredDocIds }];
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
        });
  });
}(angular, _, jQuery));
