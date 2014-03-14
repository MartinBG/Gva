/*global angular, _, require, moment*/
(function (angular, _, moment) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    var nomenclatures = require('./nomenclatures.sample');

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
      .when('GET', '/api/apps?fromDate&toDate&lin&regUri',
        function ($params, $filter, applicationsFactory) {
          var applications = applicationsFactory.getAll();

          if ($params.fromDate) {
            applications = _(applications).filter(function (app) {
              return app.doc.regDate &&
                moment(app.doc.regDate).startOf('day') >= moment($params.fromDate);

            }).value();
          }

          if ($params.toDate) {
            applications = _(applications).filter(function (app) {
              return app.doc.regDate &&
                moment(app.doc.regDate).startOf('day') <= moment($params.toDate);
            }).value();
          }

          if ($params.lin) {
            applications = _(applications).filter(function (app) {
              return app.personData.part.lin &&
                _(app.personData.part.lin).contains($params.lin);
            }).value();
          }

          if ($params.regUri) {
            applications = _(applications).filter(function (app) {
              return app.doc.regUri &&
                _(app.doc.regUri).contains($params.regUri);
            }).value();
          }

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
        function ($jsonData,docs, docCases, personLots,
          applicationLotFiles, docsFactory, applicationsFactory) {
          if (!$jsonData || !$jsonData.doc || !$jsonData.lotId) {
            return [400];
          }

          var newDoc = docsFactory.registerDoc($jsonData.doc);

          var nextDocFileId = 0;
          _(docs).map(function (doc) {
            var id = _(doc.docFiles).pluck('docFileId').max().value();
            if (id > nextDocFileId) {
              nextDocFileId = id;
            }
          });
          nextDocFileId++;

          var docFile = {
            docFileId: nextDocFileId,
            docId: newDoc.docId,
            docFileKindId: $jsonData.appFile.docFileKindId,
            docFileKind: nomenclatures.getById('docFileKinds', $jsonData.appFile.docFileKindId),
            docFileTypeId: $jsonData.appFile.docFileTypeId,
            docFileType: nomenclatures.getById('docFileTypes', $jsonData.appFile.docFileTypeId),
            docFile: $jsonData.appFile.docFile,
            isNew: false,
            isDirty: false,
            isDeleted: false,
            isInEdit: false
          };

          newDoc.docFiles.push(docFile);

          var newApplication = {
            applicationId: applicationsFactory.getNextApplicationId(),
            docId: newDoc.docId,
            lotId: $jsonData.lotId
          };

          applicationsFactory.saveApplication(newApplication);

          var person = _(personLots).filter({ lotId: newApplication.lotId }).first();

          var docPart = {};
          docPart.applications = [];
          docPart.file = [];
          docPart.part = $jsonData.appPart;
          docPart.partIndex = person.nextIndex++;
          docPart.applications.push({
            applicationId: newApplication.applicationId,
            applicationName: newDoc.docTypeName
          });

          person.personDocumentApplications = person.personDocumentApplications || [];
          person.personDocumentApplications.push(docPart);

          var applicationLotFile = {};
          applicationLotFile.setPartName = 'Заявление';
          applicationLotFile.applicationLotFileId = _(applicationLotFiles)
            .pluck('applicationLotFileId').max().value() + 1;
          applicationLotFile.docFileKey = $jsonData.appFile.docFile.key;
          applicationLotFile.lotId = newApplication.lotId;
          applicationLotFile.partIndex = docPart.partIndex;
          applicationLotFile.part = docPart.part;
          applicationLotFile.setPartAlias = 'DocumentApplication';
          applicationLotFiles.push(applicationLotFile);

          return [200, { applicationId: newApplication.applicationId }];
        })
      .when('POST', '/api/apps/link',
        function ($jsonData, applicationsFactory, docs) {
          if (!$jsonData || !$jsonData.docId || !$jsonData.lotId) {
            return [400];
          }

          var newApplication = {
            applicationId: applicationsFactory.getNextApplicationId(),
            docId: $jsonData.docId,
            lotId: $jsonData.lotId
          };

          var doc = _(docs).filter({ docId: newApplication.docId }).first();
          doc.applicationId = newApplication.applicationId;

          applicationsFactory.saveApplication(newApplication);

          return [200, { applicationId: newApplication.applicationId }];
        })
      .when('POST', '/api/apps/:id/parts/new',
        function ($params, $jsonData, personLots, applicationsFactory, docs, applicationLotFiles) {
          var application = applicationsFactory.getApplication(parseInt($params.id, 10));
          var person = _(personLots).filter({ lotId: application.lotId }).first();
          var doc = _(docs).filter({ docId: parseInt($jsonData.docId, 10) }).first();
          var applicationLotFile = {},
              docPart = {},
              nextApplicationLotFileId = _(applicationLotFiles)
                .pluck('applicationLotFileId').max().value() + 1;

          docPart.applications = [];
          docPart.file = [];
          docPart.part = $jsonData.part;
          docPart.partIndex = person.nextIndex++;
          //docPart.applications.push({
          //  applicationId: parseInt($params.id, 10),
          //  applicationName: application.doc.docTypeName
          //});

          if ($jsonData.setPartAlias === 'DocumentId') {
            person.personDocumentIds = person.personDocumentIds || [];
            person.personDocumentIds.push(docPart);
            applicationLotFile.setPartName = 'Документ за самоличност';
          }
          else if ($jsonData.setPartAlias === 'DocumentEducation') {
            person.personDocumentEducations = person.personDocumentEducations || [];
            person.personDocumentEducations.push(docPart);
            applicationLotFile.setPartName = 'Образования';
          }
          else if ($jsonData.setPartAlias === 'DocumentEmployment') {
            person.personDocumentEmployments = person.personDocumentEmployments || [];
            person.personDocumentEmployments.push(docPart);
            applicationLotFile.setPartName = 'Месторабота';
          }
          else if ($jsonData.setPartAlias === 'DocumentMed') {
            person.personDocumentMedicals = person.personDocumentMedicals || [];
            person.personDocumentMedicals.push(docPart);
            applicationLotFile.setPartName = 'Медицински';
          }
          else if ($jsonData.setPartAlias === 'DocumentCheck') {
            person.personDocumentChecks = person.personDocumentChecks || [];
            person.personDocumentChecks.push(docPart);
            applicationLotFile.setPartName = 'Проверка';
          }
          else if ($jsonData.setPartAlias === 'DocumentTraining') {
            person.personDocumentTrainings = person.personDocumentTrainings || [];
            person.personDocumentTrainings.push(docPart);
            applicationLotFile.setPartName = 'Обучение';
          }
          else if ($jsonData.setPartAlias === 'DocumentOther') {
            person.personDocumentOthers = person.personDocumentOthers || [];
            person.personDocumentOthers.push(docPart);
            applicationLotFile.setPartName = 'Друг документ';
          }
          else if ($jsonData.setPartAlias === 'DocumentApplication') {
            person.personDocumentApplications = person.personDocumentApplications || [];
            person.personDocumentApplications.push(docPart);
            applicationLotFile.setPartName = 'Заявление';
          }

          if (!!$jsonData.file) {
            var nextDocFileId = 0;
            _(docs).map(function (doc) {
              var id = _(doc.docFiles).pluck('docFileId').max().value();
              if (id > nextDocFileId) {
                nextDocFileId = id;
              }
            });
            nextDocFileId++;

            var docFile = {
              docFileId: nextDocFileId,
              docId: doc.docId,
              docFileKindId: $jsonData.file.docFileKindId,
              docFileKind: nomenclatures.getById('docFileKinds', $jsonData.file.docFileKindId),
              docFileTypeId: $jsonData.file.docFileTypeId,
              docFileType: nomenclatures.getById('docFileTypes', $jsonData.file.docFileTypeId),
              docFile: $jsonData.file.docFile,
              isNew: false,
              isDirty: false,
              isDeleted: false,
              isInEdit: false
            };

            doc.docFiles.push(docFile);

            applicationLotFile.applicationLotFileId = nextApplicationLotFileId;
            applicationLotFile.docFileKey = $jsonData.file.docFile.key;
            applicationLotFile.lotId = application.lotId;
            applicationLotFile.partIndex = docPart.partIndex;
            applicationLotFile.part = docPart.part;
            applicationLotFile.setPartAlias = $jsonData.setPartAlias;
            applicationLotFiles.push(applicationLotFile);
          }

          return [200];
        })
      .when('POST', '/api/apps/:id/parts/linkNew',
        function ($params, $jsonData, docs, personLots, applicationsFactory, applicationLotFiles) {
          var application = applicationsFactory.getApplication(parseInt($params.id, 10));
          var person = _(personLots).filter({ lotId: application.lotId }).first();
          var doc = _(docs).filter(function (doc) {
            return _(doc.docFiles).filter(function (docFile) {
              return _(docFile).filter({ key: $jsonData.docFileKey }).first();
            });
          }).first();
          var docFile = _(doc.docFiles).filter(function (docFile) {
            return _(docFile).filter({ key: $jsonData.docFileKey }).first();
          }).first();
          var applicationLotFile = {},
              docPart = {},
              nextApplicationLotFileId = _(applicationLotFiles)
                .pluck('applicationLotFileId').max().value() + 1;

          docPart.applications = [];
          docPart.file = [];
          docPart.part = $jsonData.part;
          docPart.partIndex = person.nextIndex++;
          //docPart.applications.push({
          //  applicationId: parseInt($params.id, 10),
          //  applicationName: application.doc.docTypeName
          //});

          if ($jsonData.setPartAlias === 'DocumentId') {
            person.personDocumentIds = person.personDocumentIds || [];
            person.personDocumentIds.push(docPart);
            applicationLotFile.setPartName = 'Документ за самоличност';
          }
          else if ($jsonData.setPartAlias === 'DocumentEducation') {
            person.personDocumentEducations = person.personDocumentEducations || [];
            person.personDocumentEducations.push(docPart);
            applicationLotFile.setPartName = 'Образования';
          }
          else if ($jsonData.setPartAlias === 'DocumentEmployment') {
            person.personDocumentEmployments = person.personDocumentEmployments || [];
            person.personDocumentEmployments.push(docPart);
            applicationLotFile.setPartName = 'Месторабота';
          }
          else if ($jsonData.setPartAlias === 'DocumentMed') {
            person.personDocumentMedicals = person.personDocumentMedicals || [];
            person.personDocumentMedicals.push(docPart);
            applicationLotFile.setPartName = 'Медицински';
          }
          else if ($jsonData.setPartAlias === 'DocumentCheck') {
            person.personDocumentChecks = person.personDocumentChecks || [];
            person.personDocumentChecks.push(docPart);
            applicationLotFile.setPartName = 'Проверка';
          }
          else if ($jsonData.setPartAlias === 'DocumentTraining') {
            person.personDocumentTrainings = person.personDocumentTrainings || [];
            person.personDocumentTrainings.push(docPart);
            applicationLotFile.setPartName = 'Обучение';
          }
          else if ($jsonData.setPartAlias === 'DocumentOther') {
            person.personDocumentOthers = person.personDocumentOthers || [];
            person.personDocumentOthers.push(docPart);
            applicationLotFile.setPartName = 'Друг документ';
          }
          else if ($jsonData.setPartAlias === 'DocumentApplication') {
            person.personDocumentApplications = person.personDocumentApplications || [];
            person.personDocumentApplications.push(docPart);
            applicationLotFile.setPartName = 'Заявление';
          }

          applicationLotFile.applicationLotFileId = nextApplicationLotFileId;
          applicationLotFile.docFileKey = docFile.docFile.key;
          applicationLotFile.lotId = person.lotId;
          applicationLotFile.partIndex = docPart.partIndex;
          applicationLotFile.part = $jsonData.part;
          applicationLotFile.setPartAlias = $jsonData.setPartAlias;
          applicationLotFiles.push(applicationLotFile);

          return [200];
        })
      .when('POST', '/api/apps/:id/parts/linkExisting',
        function ($params, $jsonData, applicationsFactory, personLots, docs, applicationLotFiles) {
          var application = applicationsFactory.getApplication(parseInt($params.id, 10));
          var person = _(personLots).filter({ lotId: application.lotId }).first();
          var doc = _(docs).filter(function (doc) {
            return _(doc.docFiles).filter(function (docFile) {
              return _(docFile).filter({ key: $jsonData.docFileKey }).first();
            });
          }).first();
          var docFile = _(doc.docFiles).filter(function (docFile) {
            return _(docFile).filter({ key: $jsonData.docFileKey }).first();
          }).first();
          var docPart = null,
              applicationLotFile = {},
              nextApplicationLotFileId = _(applicationLotFiles)
                .pluck('applicationLotFileId').max().value() + 1;

          if ($jsonData.setPartAlias === 'DocumentId') {
            docPart = _(person.personDocumentIds || [])
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Документ за самоличност';
          }
          else if ($jsonData.setPartAlias === 'DocumentEducation') {
            docPart = _(person.personDocumentEducations || [])
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Образования';
          }
          else if ($jsonData.setPartAlias === 'DocumentEmployment') {
            docPart = _(person.personDocumentEmployments || [])
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Месторабота';
          }
          else if ($jsonData.setPartAlias === 'DocumentMed') {
            docPart = _(person.personDocumentMedicals || [])
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Медицински';
          }
          else if ($jsonData.setPartAlias === 'DocumentCheck') {
            docPart = _(person.personDocumentChecks || [])
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Проверка';
          }
          else if ($jsonData.setPartAlias === 'DocumentTraining') {
            docPart = _(person.personDocumentTrainings || [])
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Обучение';
          }
          else if ($jsonData.setPartAlias === 'DocumentOther') {
            docPart = _(person.personDocumentOthers || [])
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Друг документ';
          }
          else if ($jsonData.setPartAlias === 'DocumentApplication') {
            docPart = _(person.personDocumentApplications || [])
            .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Заявление';
          }

          if (docPart) {
            //docPart.applications.push(
            //{
            //  applicationId: parseInt($params.id, 10),
            //  applicationName: application.doc.docTypeName
            //});

            applicationLotFile.applicationLotFileId = nextApplicationLotFileId;
            applicationLotFile.docFileKey = docFile.docFile.key;
            applicationLotFile.lotId = person.lotId;
            applicationLotFile.part = docPart.part;
            applicationLotFile.partIndex = docPart.partIndex;
            applicationLotFile.setPartAlias = $jsonData.setPartAlias;
            applicationLotFiles.push(applicationLotFile);
          }

          return [200];
        });
  });
}(angular, _, moment));
