/*global angular, _*/
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

    function personMapper(p) {
      if (!!p) {
        return {
          lin: p.part.lin,
          uin: p.part.uin,
          names: p.part.firstName + ' ' +
            p.part.middleName + ' ' + p.part.lastName,
          /*jshint -W052*/
          age: ~~((Date.now() - new Date(p.part.dateOfBirth)) / (31557600000))
          /*jshint +W052*/
        };
      }
    }

    $httpBackendConfiguratorProvider
      .when('GET', '/api/apps',
        function ($params, $filter, applicationsFactory) {
          var applications = _(applicationsFactory.getAll()).map(function (application) {
            if (application.lotId) { //todo change person data better
              application.person = personMapper(application.personData);
            }
            return application;
          }).value();

          return [200, applications];
        })
      .when('GET', '/api/apps/:id',
        function ($params, $filter, applicationsFactory) {
          var application = applicationsFactory.getApplication(parseInt($params.id, 10));

          if (application) {
            if (application.lotId) { //todo change person data better
              application.person = personMapper(application.personData);
            }

            return [200, application];
          }
          else {
            return [404];
          }

        })
      .when('POST', '/api/apps/new',
        function ($jsonData, applicationsFactory, docs, docCases) {
          if (!$jsonData || !$jsonData.doc || !$jsonData.lotId) {
            return [400];
          }

          var newApplication = {
            applicationId: applicationsFactory.getNextApplicationId(),
            docId: null,
            lotId: $jsonData.lotId
          };

          if ($jsonData.doc.docId) {
            newApplication.docId = $jsonData.doc.docId;

            var doc = _(docs).filter({docId: newApplication.docId}).first();
            doc.applicationId = newApplication.applicationId;
          }
          else {
            var today = new Date();
            var nextDocId = _(docs).pluck('docId').max().value() + 1;

            var newDoc = _.assign(_.cloneDeep(defaultDoc), $jsonData.doc);
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
            newDoc.applicationId = newApplication.applicationId;

            var docCaseObj = {
              docCaseId: newDoc.docId,
              docCase: [{
                docId: newDoc.docId,
                regDate: newDoc.regDate,
                regNumber: newDoc.regUri,
                direction: newDoc.docDirectionName,
                casePartType: newDoc.docCasePartTypeName,
                statusName: newDoc.docStatusName,
                description: newDoc.docTypeName
              }]
            };
            docCases.push(docCaseObj);

            newDoc.docRelations = docCaseObj.docCase;
            
            docs.push(newDoc);

            newApplication.docId = newDoc.docId;
          }

          applicationsFactory.saveApplication(newApplication);

          return [200, { applicationId: newApplication.applicationId }];
        })
      .when('GET', '/api/nomenclatures/persons',
        function (personLots) {
          var noms= [],
            nomItem = {
              nomTypeValueId: '',
              name: '',
              content: []
            };

          _.forEach(personLots, function (item) {
            var t = {};

            nomItem.nomTypeValueId = item.lotId;
            nomItem.name = item.personData.part.firstName + ' ' + item.personData.part.lastName;
            nomItem.content = item;

            _.assign(t, nomItem, true);
            noms.push(t);
          });

          return [200, noms];
        });
  });
}(angular, _));
