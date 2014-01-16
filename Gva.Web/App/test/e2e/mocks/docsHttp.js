/*global angular, require, _*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var nomenclatures = require('./nomenclatures.sample'),
      today = new Date(),
      docs = [{
        docId: 1,
        docStatusId: 1,
        docStatusName: 'Чернова',
        docSubject: 'Заявление за обновяване на автомати',
        docSubjectLabel: 'Относно',
        docTypeId: 1,
        docDirectionName: 'Входящ',
        docDirectionId: 1,
        docTypeName: 'ИСКАНЕ по чл. 19, ал. 2, т. 2 от Наредбата за документите',
        regDate: today,
        regUri: '000030-1-05.01.2014',
        regIndex: '000030',
        regNumber: 1,
        correspondentName: 'Ви Ем Уеър',
        corrRegNumber: 123,
        corrRegDate: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
        docSourceType: nomenclatures.docSourceTypes[0],
        docDestinationType: nomenclatures.docDestinationTypes[1],
        assignmentType: nomenclatures.assignmentTypes[1],
        assignmentDate: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
        assignmentDeadline: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
        accessCode: '56DY77ICXP',
        caseRegUri: undefined,
        docFormatTypeId: 1,
        docCasePartTypeId: 1,
        docTypeGroupId: 1,
        docCorrespondents: [],
        docUnits: [],
        numberOfDocuments: undefined,
        isVisibleRoleFrom: true,
        isVisibleRoleTo: false,
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
        privateDocFiles: [
          {
            docFileId: 2010,
            name: 'Файл за прайвит док файлс',
            docFileTypeName: 'Неопределен',
            docFileTypeIsEditable: false,
            isActive: true
          }
        ],
        publicDocFiles: [
          {
            docFileId: 2010,
            name: 'Файл за публик док файлс',
            docFileTypeName: 'Неопределен',
            docFileTypeIsEditable: false,
            isActive: true
          }
        ],
        docWorkflows: [
          {
            eventDate: new Date(today.getTime() + (24 * 60 * 60 * 1000)),
            docWorkflowActionName: 'Подпис',
            yesNo: true,
            principalUnitName: 'admin',
            toUnitName: '',
            note: null
          },
          {
            eventDate: new Date(today.getTime() + (24 * 60 * 60 * 1000)),
            docWorkflowActionName: 'Съгласуване',
            yesNo: true,
            principalUnitName: 'admin',
            toUnitName: '',
            note: null
          },
          {
            eventDate: new Date(today.getTime() + (24 * 60 * 60 * 1000)),
            docWorkflowActionName: 'Одобрение',
            yesNo: true,
            principalUnitName: 'admin',
            toUnitName: '',
            note: null
          }
        ],
        isVisibleDocWorkflows: true,
        docElectronicServiceStages: [
          {
            startingDate: '2014-01-08T17:21:43.303',
            electronicServiceStageName: 'Приемане на заявление за  административна услуга',
            electronicServiceStageExecutors: '<b>Служител ДКХ</b>',
            expectedEndingDate: null,
            endingDate: '2014-01-09T17:38:49.683',
            isCurrentStage: false
          },
          {
            startingDate: '2014-01-08T17:21:43.303',
            electronicServiceStageName: 'Приемане на заявление за отстраняване на заявителя',
            electronicServiceStageExecutors: 'Служител ДКХ',
            expectedEndingDate: null,
            endingDate: '2014-01-09T17:38:49.683',
            isCurrentStage: true
          }
        ],
        docRelations: [
          {
            docId: 827068,
            isCurrent: true,
            docDocCasePartTypeStyleColor: 'black',
            docRegDate: '2014-01-08T17:22:14.58',
            regNumberCol: '000030-2-08.01.2014<br/>Входящ | Публичен',
            docDocStatusName: 'Чернова',
            descriptionCol: 'ИСКАНЕ по чл. 6 и 18 от Наредбата за документите'
          },
          {
            docId: 827068,
            isCurrent: false,
            docDocCasePartTypeStyleColor: 'red',
            docRegDate: '2014-01-08T17:22:14.58',
            regNumberCol: 'Вътрешен | Вътрешен',
            docDocStatusName: 'Приключен',
            descriptionCol: 'Резолюция: Резолюция'
          },
          {
            docId: 827068,
            isCurrent: false,
            docDocCasePartTypeStyleColor: 'black',
            docRegDate: '2014-01-08T17:22:14.58',
            regNumberCol: 'Вътрешен | Вътрешен',
            docDocStatusName: 'Приключен',
            descriptionCol: 'Задача: Задача'
          }
        ],
        docClassifications: [
          {
            classificationName: 'Всички документи',
            classificationDate: '2014-01-08T17:21:55.387'
          },
          {
            classificationName: 'Услуги',
            classificationDate: '2014-01-08T17:21:55.387'

          },
          {
            classificationName: 'Искане',
            classificationDate: '2014-01-08T17:21:55.387'
          }
        ]
      }, {
        docId: 2,
        docStatusId: 2,
        docStatusName: 'Изготвен',
        docSubjectLabel: 'Относно',
        docSubject: 'Заявление за добавяне на автомати',
        docTypeId: 1,
        docDirectionName: 'Входящ',
        docDirectionId: 1,
        docTypeName: 'ИСКАНЕ по чл. 21, ал. 2, т. 2 от Наредбата за документите',
        regDate: new Date(today.getTime() + (24 * 60 * 60 * 1000)),
        regUri: '000030-2-05.01.2014',
        regIndex: '000030',
        regNumber: 2,
        correspondentName: 'Стефанстрой ЕООД',
        corrRegNumber: null,
        corrRegDate: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
        docSourceType: nomenclatures.docSourceTypes[0],
        docDestinationType: nomenclatures.docDestinationTypes[1],
        assignmentType: nomenclatures.assignmentTypes[1],
        assignmentDate: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
        assignmentDeadline: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
        accessCode: '56DY77ICXP',
        caseRegUri: undefined,
        docFormatTypeId: 1,
        docCasePartTypeId: 1,
        docTypeGroupId: 1,
        docCorrespondents: [],
        docUnits: [],
        numberOfDocuments: undefined,
        isVisibleRoleFrom: true,
        isVisibleRoleTo: false,
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
        privateDocFiles: [
          {
            docFileId: 2010,
            name: 'Файл за прайвит док файлс',
            docFileTypeName: 'Неопределен',
            docFileTypeIsEditable: false,
            isActive: true
          }
        ],
        publicDocFiles: [
          {
            docFileId: 2010,
            name: 'Файл за публик док файлс',
            docFileTypeName: 'Неопределен',
            docFileTypeIsEditable: false,
            isActive: true
          }
        ],
        docWorkflows: [
          {
            eventDate: new Date(today.getTime() + (24 * 60 * 60 * 1000)),
            docWorkflowActionName: 'Подпис',
            yesNo: true,
            principalUnitName: 'admin',
            toUnitName: '',
            note: null
          },
          {
            eventDate: new Date(today.getTime() + (24 * 60 * 60 * 1000)),
            docWorkflowActionName: 'Съгласуване',
            yesNo: true,
            principalUnitName: 'admin',
            toUnitName: '',
            note: null
          },
          {
            eventDate: new Date(today.getTime() + (24 * 60 * 60 * 1000)),
            docWorkflowActionName: 'Одобрение',
            yesNo: true,
            principalUnitName: 'admin',
            toUnitName: '',
            note: null
          }
        ],
        isVisibleDocWorkflows: true,
        docElectronicServiceStages: [
          {
            startingDate: '2014-01-08T17:21:43.303',
            electronicServiceStageName: 'Приемане на заявление за  административна услуга',
            electronicServiceStageExecutors: 'Служител ДКХ',
            expectedEndingDate: null,
            endingDate: '2014-01-09T17:38:49.683',
            isCurrentStage: false
          },
          {
            startingDate: '2014-01-08T17:21:43.303',
            electronicServiceStageName: 'Приемане на заявление за  административна услуга',
            electronicServiceStageExecutors: 'Служител ДКХ',
            expectedEndingDate: null,
            endingDate: '2014-01-09T17:38:49.683',
            isCurrentStage: false
          }
        ],
        docRelations: [
          {
            docId: 827068,
            isCurrent: true,
            docDocCasePartTypeStyleColor: 'black',
            docRegDate: '2014-01-08T17:22:14.58',
            regNumberCol: '000030-2-08.01.2014<br/>Входящ | Публичен',
            docDocStatusName: 'Чернова',
            descriptionCol: 'ИСКАНЕ по чл. 6 и 18 от Наредбата за документите'
          },
          {
            docId: 827068,
            isCurrent: true,
            docDocCasePartTypeStyleColor: 'black',
            docRegDate: '2014-01-08T17:22:14.58',
            regNumberCol: 'Вътрешен | Вътрешен',
            docDocStatusName: 'Приключен',
            descriptionCol: 'Резолюция: Резолюция'
          },
          {
            docId: 827068,
            isCurrent: true,
            docDocCasePartTypeStyleColor: 'black',
            docRegDate: '2014-01-08T17:22:14.58',
            regNumberCol: 'Вътрешен | Вътрешен',
            docDocStatusName: 'Приключен',
            descriptionCol: 'Задача: Задача'
          }
        ],
        docClassifications: [
          {
            classificationName: 'Всички документи',
            classificationDate: '2014-01-08T17:21:55.387'
          },
          {
            classificationName: 'Услуги',
            classificationDate: '2014-01-08T17:21:55.387'

          },
          {
            classificationName: 'Искане',
            classificationDate: '2014-01-08T17:21:55.387'
          }
        ]
      }, {
        docId: 3,
        docStatusId: 3,
        docStatusName: 'Обработен',
        docSubjectLabel: 'Относно',
        docSubject: 'Искане за закриване на обект',
        docTypeId: 2,
        docDirectionName: 'Входящ',
        docDirectionId: 1,
        docTypeName: 'Писмо',
        regDate: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
        regUri: '000030-3-05.01.2014',
        regIndex: '000030',
        regNumber: 2,
        correspondentName: 'Казино Легал',
        corrRegNumber: null,
        corrRegDate: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
        docSourceType: nomenclatures.docSourceTypes[0],
        docDestinationType: nomenclatures.docDestinationTypes[1],
        assignmentType: nomenclatures.assignmentTypes[1],
        assignmentDate: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
        assignmentDeadline: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
        accessCode: '56DY77ICXP',
        caseRegUri: undefined,
        docFormatTypeId: 1,
        docCasePartTypeId: 1,
        docTypeGroupId: 1,
        docCorrespondents: [],
        docUnits: [],
        numberOfDocuments: undefined,
        isVisibleRoleFrom: true,
        isVisibleRoleTo: false,
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
        privateDocFiles: [
          {
            docFileId: 2010,
            name: 'Файл за прайвит док файлс',
            docFileTypeName: 'Неопределен',
            docFileTypeIsEditable: false,
            isActive: true
          }
        ],
        publicDocFiles: [
          {
            docFileId: 2010,
            name: 'Файл за публик док файлс',
            docFileTypeName: 'Неопределен',
            docFileTypeIsEditable: false,
            isActive: true
          }
        ],
        docWorkflows: [
          {
            eventDate: new Date(today.getTime() + (24 * 60 * 60 * 1000)),
            docWorkflowActionName: 'Подпис',
            yesNo: true,
            principalUnitName: 'admin',
            toUnitName: '',
            note: null
          },
          {
            eventDate: new Date(today.getTime() + (24 * 60 * 60 * 1000)),
            docWorkflowActionName: 'Съгласуване',
            yesNo: true,
            principalUnitName: 'admin',
            toUnitName: '',
            note: null
          },
          {
            eventDate: new Date(today.getTime() + (24 * 60 * 60 * 1000)),
            docWorkflowActionName: 'Одобрение',
            yesNo: true,
            principalUnitName: 'admin',
            toUnitName: '',
            note: null
          }
        ],
        isVisibleDocWorkflows: true,
        docElectronicServiceStages: [
          {
            startingDate: '2014-01-08T17:21:43.303',
            electronicServiceStageName: 'Приемане на заявление за  административна услуга',
            electronicServiceStageExecutors: 'Служител ДКХ',
            expectedEndingDate: null,
            endingDate: '2014-01-09T17:38:49.683',
            isCurrentStage: false
          },
          {
            startingDate: '2014-01-08T17:21:43.303',
            electronicServiceStageName: 'Приемане на заявление за  административна услуга',
            electronicServiceStageExecutors: 'Служител ДКХ',
            expectedEndingDate: null,
            endingDate: '2014-01-09T17:38:49.683',
            isCurrentStage: false
          }
        ],
        docRelations: [
          {
            docId: 827068,
            isCurrent: true,
            docDocCasePartTypeStyleColor: 'black',
            docRegDate: '2014-01-08T17:22:14.58',
            regNumberCol: '000030-2-08.01.2014<br/>Входящ | Публичен',
            docDocStatusName: 'Чернова',
            descriptionCol: 'ИСКАНЕ по чл. 6 и 18 от Наредбата за документите'
          },
          {
            docId: 827068,
            isCurrent: true,
            docDocCasePartTypeStyleColor: 'black',
            docRegDate: '2014-01-08T17:22:14.58',
            regNumberCol: 'Вътрешен | Вътрешен',
            docDocStatusName: 'Приключен',
            descriptionCol: 'Резолюция: Резолюция'
          },
          {
            docId: 827068,
            isCurrent: true,
            docDocCasePartTypeStyleColor: 'black',
            docRegDate: '2014-01-08T17:22:14.58',
            regNumberCol: 'Вътрешен | Вътрешен',
            docDocStatusName: 'Приключен',
            descriptionCol: 'Задача: Задача'
          }
        ],
        docClassifications: [
          {
            classificationName: 'Всички документи',
            classificationDate: '2014-01-08T17:21:55.387'
          },
          {
            classificationName: 'Услуги',
            classificationDate: '2014-01-08T17:21:55.387'

          },
          {
            classificationName: 'Искане',
            classificationDate: '2014-01-08T17:21:55.387'
          }
        ]
      }],
      nextDocId = 3;

    $httpBackendConfiguratorProvider
      .when('GET', '/api/docs?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units',
        function ($params) {
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
      .when('GET', '/api/docs/new',
        function () {
          var newDoc = {
            docId: undefined,
            docStatusId: undefined,
            docStatusName: undefined,
            docSubject: undefined,
            docSubjectLabel: undefined,
            docTypeId: undefined,
            docDirectionName: undefined,
            docDirectionId: undefined,
            docTypeName: undefined,
            regDate: undefined,
            regUri: undefined,
            regIndex: undefined,
            regNumber: undefined,
            correspondentName: undefined,
            caseRegUri: undefined,//
            docFormatTypeId: undefined,
            docCasePartTypeId: undefined,
            docTypeGroupId: undefined,//
            docCorrespondents: [],//
            numberOfDocuments: undefined//
          };

          return [200, newDoc];

        })
      .when('POST', '/api/docs/saveNew',
        function ($jsonData) {
          if (!$jsonData || $jsonData.corrId) {
            return [400];
          }

          $jsonData.docId = ++nextDocId;
          $jsonData.regUri = '000030-' + $jsonData.docId + '-05.01.2014';
          docs.push($jsonData);

          return [200];
        })
      .when('GET', '/api/docs/:docId',
        function ($params) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          return [200, doc];
        })
       .when('POST', '/api/docs/:docId',
        function ($params, $jsonData, $filter) {
          var docId = parseInt($params.docId, 10),
            docIndex = docs.indexOf($filter('filter')(docs, { docId: docId })[0]);

          if (docIndex === -1) {
            return [400];
          }

          docs[docIndex] = $jsonData;

          return [200];
        });
  });
}(angular, _));
