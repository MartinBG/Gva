/*global angular, require, _*/
(function (angular, _) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');
  var today = new Date();

  angular.module('app').factory('docs', ['docCases',
    function (docCases) {
      return [
        {
          docId: 1,
          parentDocId: null,
          docStatusId: 1,
          docStatusName: 'Чернова',
          docSubject: 'Заявление',
          docSubjectLabel: 'Относно',
          docTypeId: 1,
          docDirectionName: 'Входящ',
          docDirectionId: 1,
          docTypeName: 'Издаване на свидетелство за правоспособност на авиационен персонал',
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
          docCasePartTypeName: 'Публичен',
          docTypeGroupId: 1,
          docCorrespondents: [],
          docUnits: [],
          docUnitsFrom: [],
          docUnitsTo: [],
          numberOfDocuments: undefined,
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
          privateDocFiles: [
            {
              key: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1670',
              name: 'TestFile1.pdf',
              relativePath: undefined
            }
          ],
          publicDocFiles: [
            {
              key: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1671',
              name: 'TestFile2.pdf',
              relativePath: undefined
            },
            {
              key: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1672',
              name: 'TestFile3.pdf',
              relativePath: undefined
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
          docRelations: _(docCases).filter({ docCaseId: 1 }).first().docCase,
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
          ],
          applicationId: 1
        }, {
          docId: 2,
          parentDocId: null,
          docStatusId: 2,
          docStatusName: 'Изготвен',
          docSubjectLabel: 'Относно',
          docSubject: 'Заявление за добавяне на автомати',
          docTypeId: 1,
          docDirectionName: 'Входящ',
          docDirectionId: 1,
          docTypeName: 'Издаване на свидетелство за правоспособност на авиационен персонал',
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
          docCasePartTypeName: 'Публичен',
          docTypeGroupId: 1,
          docCorrespondents: [],
          docUnits: [],
          docUnitsFrom: [],
          docUnitsTo: [],
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
              key: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1600',
              name: 'TestFile112.pdf',
              relativePath: undefined
            }
          ],
          publicDocFiles: [
            {
              key: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1673',
              name: 'TestFile4.pdf',
              relativePath: undefined
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
          docRelations: _(docCases).filter({ docCaseId: 2 }).first().docCase,
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
          parentDocId: null,
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
          docCasePartTypeName: 'Публичен',
          docTypeGroupId: 1,
          docCorrespondents: [],
          docUnits: [],
          docUnitsFrom: [],
          docUnitsTo: [],
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
              key: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1611',
              name: 'TestFile111.pdf',
              relativePath: undefined
            }
          ],
          publicDocFiles: [
            {
              key: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1674',
              name: 'TestFile5.pdf',
              relativePath: undefined
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
          docRelations: _(docCases).filter({ docCaseId: 3 }).first().docCase,
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
        }
      ];
    }
  ]);
}(angular, _));

