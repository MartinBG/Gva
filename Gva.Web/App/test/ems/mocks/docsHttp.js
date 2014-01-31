/*global angular, _, jQuery, require*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var nomenclatures = require('./nomenclatures.sample');

    var defaultDoc = {
      docId: null,
      parentDocId: null,
      docStatusId: 2,
      docStatusName: 'Чернова',
      docSubject: null,
      docSubjectLabel: 'Относно',
      docTypeId: null,
      docDirectionName: null,
      docDirectionId: null,
      docTypeName: null,
      regDate: null,
      regUri: null,
      regIndex: '000030',
      regNumber: null,
      correspondentName: null,
      corrRegNumber: null,
      corrRegDate: null,
      docSourceType: 2,
      docDestinationType: null,
      assignmentType: 2,
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
      numberOfDocuments: 1,
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
      docBody: '',
      privateDocFiles: [],
      publicDocFiles: [],
      docWorkflows: [],
      isVisibleDocWorkflows: true,
      docElectronicServiceStages: [],
      docRelations: [],
      docClassifications: []
    };

    $httpBackendConfiguratorProvider
      .when('GET', '/api/docs?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units',
        function ($params, docs) {
          var t = nomenclatures.get('countries', 'Belgium'),
            result = _(docs).filter(function (d) {
              var isMatch = true;


              if (d || $params) {
                return true;
              }
              else {
                return false;
              }

              return isMatch;
            })
          .value();

          t = undefined;

          return [200, result];
        })
      .when('POST', '/api/docs',
        function ($jsonData, docs, docCases) {
          if (!$jsonData) {
            return [400];
          }

          var today = new Date();

          var newDoc;
          var numberOfDocs = !!$jsonData.numberOfDocuments ? $jsonData.numberOfDocuments : 1;

          for (var i = 0; i < numberOfDocs; i++) {
            var nextDocId = _(docs).pluck('docId').max().value() + 1;

            newDoc = _.cloneDeep(defaultDoc);
            jQuery.extend(newDoc, $jsonData);

            newDoc.docId = nextDocId;
            newDoc.regNumber = nextDocId;
            newDoc.regDate = new Date();
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
          }

          return [200, newDoc];
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
