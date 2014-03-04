/*global angular, require, _*/
(function (angular, _) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');
  var today = new Date();

  angular.module('app').factory('docs', ['docCases', 'docStages', 'docWorkflows',
    function (docCases, docStages, docWorkflows) {
      return [
        {
          docId: 1,
          parentDocId: null,
          docStatusId: 1,
          docStatusName: 'Чернова',
          docStatusAlias: 'Draft',
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
          docSourceType: nomenclatures.docSourceType[0],
          docDestinationType: nomenclatures.docDestinationType[1],
          assignmentType: nomenclatures.assignmentType[1],
          assignmentDate: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
          assignmentDeadline: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
          accessCode: '56DY77ICXP',
          caseRegUri: undefined,
          docFormatTypeId: 1,
          docCasePartTypeId: 1,
          docCasePartTypeName: 'Публичен',
          docTypeGroupId: 1,
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
          isDocIncoming: true,
          isDocInternal: false,
          isDocOutgoing: false,
          isDocInternalOutgoing: false,
          isResolution: false,
          isRemark: false,
          isTask: false,
          docBody: '',
          docFiles: [
            {
              docFileId: 1,
              docId: 1,
              docFileKindId: 1,
              docFileKind: nomenclatures.docFileKinds[0],
              docFileTypeId: 1,
              docFileType: nomenclatures.docFileTypes[0],
              docFileUrl: 'api/file?fileKey=04BCC096-AB2F-4C77-AB82-' +
                 '6FC3E9CE1670&fileName=TestFile1.pdf',
              docFile: {
                key: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1670',
                name: 'TestFile1.pdf',
                relativePath: undefined
              },
              name: 'Искане',
              isNew: false,
              isDirty: false,
              isDeleted: false,
              isInEdit: false
            },
            {
              docFileId: 2,
              docId: 1,
              docFileKindId: 2,
              docFileKind: nomenclatures.docFileKinds[0],
              docFileTypeId: 1,
              docFileType: nomenclatures.docFileTypes[0],
              docFileUrl: 'api/file?fileKey=04BCC096-AB2F-4C77-AB82-' +
                 '6FC3E9CE1671&fileName=TestFile2.pdf',
              docFile: {
                key: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1671',
                name: 'TestFile2.pdf',
                relativePath: undefined
              },
              name: 'Актуален учредителен акт',
              isNew: false,
              isDirty: false,
              isDeleted: false,
              isInEdit: false
            },
            {
              docFileId: 3,
              docId: 1,
              docFileKindId: 2,
              docFileKind: nomenclatures.docFileKinds[0],
              docFileTypeId: 1,
              docFileType: nomenclatures.docFileTypes[0],
              docFileUrl: 'api/file?fileKey=04BCC096-AB2F-4C77-AB82-' +
                 '6FC3E9CE1672&fileName=TestFile3.pdf',
              docFile: {
                key: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1672',
                name: 'TestFile3.pdf',
                relativePath: undefined
              },
              name: 'Договор',
              isNew: false,
              isDirty: false,
              isDeleted: false,
              isInEdit: false
            },
            {
              docFileId: 4,
              docId: 1,
              docFileKindId: 2,
              docFileKind: nomenclatures.docFileKinds[0],
              docFileTypeId: 1,
              docFileType: nomenclatures.docFileTypes[0],
              docFileUrl: 'api/file?fileKey=04BCC096-AB2F-4C77-AB82-' +
                 '6FC3E9CE1673&fileName=TestFile4.pdf',
              docFile: {
                key: '04BCC096-AB2F-4C77-AB82-6FC3E9CE1673',
                name: 'TestFile4.pdf',
                relativePath: undefined
              },
              name: 'Документ за актуална търговска или съдебна регистрация ',
              isNew: false,
              isDirty: false,
              isDeleted: false,
              isInEdit: false
            }
          ],
          docWorkflows: _.filter(docWorkflows, { docId: 1 }),
          isVisibleDocWorkflows: true,
          docElectronicServiceStages: _.filter(docStages, { docId: 1 }),
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
          docStatusAlias: 'Prepared',
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
          docSourceType: nomenclatures.docSourceType[0],
          docDestinationType: nomenclatures.docDestinationType[1],
          assignmentType: nomenclatures.assignmentType[1],
          assignmentDate: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
          assignmentDeadline: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
          accessCode: '56DY77ICXP',
          caseRegUri: undefined,
          docFormatTypeId: 1,
          docCasePartTypeId: 1,
          docCasePartTypeName: 'Публичен',
          docTypeGroupId: 1,
          docCorrespondents: [],
          docUnitsFrom: [],
          docUnitsTo: [],
          docUnitsCCopy: [],
          docUnitsImportedBy: [],
          docUnitsInCharge: [],
          docUnitsControlling: [],
          docUnitsReaders: [],
          docUnitsEditors: [],
          docUnitsRegistrators: [],
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
          isDocIncoming: true,
          isDocInternal: false,
          isDocInternalOutgoing: false,
          isDocOutgoing: false,
          isRemark: false,
          isResolution: false,
          isTask: false,
          docBody: '',
          docFiles: [],
          docWorkflows: _.filter(docWorkflows, { docId: 2 }),
          isVisibleDocWorkflows: true,
          docElectronicServiceStages: _.filter(docStages, { docId: 2 }),
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
          docStatusAlias: 'Processed',
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
          docSourceType: nomenclatures.docSourceType[0],
          docDestinationType: nomenclatures.docDestinationType[1],
          assignmentType: nomenclatures.assignmentType[1],
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
          docFiles: [],
          docWorkflows: _.filter(docWorkflows, { docId: 3 }),
          isVisibleDocWorkflows: true,
          docElectronicServiceStages: _.filter(docStages, { docId: 3 }),
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

