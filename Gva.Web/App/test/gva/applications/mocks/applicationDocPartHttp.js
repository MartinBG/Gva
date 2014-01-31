/*global angular, _*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    $httpBackendConfiguratorProvider
      .when('POST', '/api/apps/:id/docParts',
        function ($params, $jsonData, personLots, applicationsFactory, docs, applicationLotFiles) {
          var person = _(personLots)
                .filter({ lotId: parseInt($jsonData.personId, 10) }).first(),
              application = applicationsFactory.getApplication(parseInt($params.id, 10)),
              doc = _(docs).filter({ docId: parseInt($jsonData.currentDocId, 10) }).first(),
              docPart = {
            applications: [],
            file: [],
            part: null,
            partIndex: null
          },
          applicationLotFile = {},
          nextApplicationLotFileId = _(applicationLotFiles)
            .pluck('applicationLotFileId').max().value() + 1;

          docPart.applications.push({
            applicationId: parseInt($params.id, 10),
            applicationName: application.doc.docTypeName
          });
          docPart.part = $jsonData.part;
          docPart.partIndex = person.nextIndex++;

          if (parseInt($jsonData.setPartId, 10) === 1) {
            person.personDocumentIds.push(docPart);
            applicationLotFile.setPartName = 'Документ за самоличност';
          }
          else if (parseInt($jsonData.setPartId, 10) === 2) {
            person.personDocumentEducations.push(docPart);
            applicationLotFile.setPartName = 'Образования';
          }
          else if (parseInt($jsonData.setPartId, 10) === 3) {
            person.personDocumentEmployments.push(docPart);
            applicationLotFile.setPartName = 'Месторабота';
          }
          else if (parseInt($jsonData.setPartId, 10) === 4) {
            person.personDocumentMedicals.push(docPart);
            applicationLotFile.setPartName = 'Медицински';
          }
          else if (parseInt($jsonData.setPartId, 10) === 5) {
            person.personDocumentChecks.push(docPart);
            applicationLotFile.setPartName = 'Проверка';
          }
          else if (parseInt($jsonData.setPartId, 10) === 6) {
            person.personDocumentTrainings.push(docPart);
            applicationLotFile.setPartName = '*';
          }
          else if (parseInt($jsonData.setPartId, 10) === 7) {
            person.personDocumentOthers.push(docPart);
            applicationLotFile.setPartName = '*';
          }

          //todo add files !
          if (!!$jsonData.file) {
            doc.publicDocFiles.push($jsonData.file[0]);

            applicationLotFile.applicationLotFileId = nextApplicationLotFileId;
            applicationLotFile.docFileId = $jsonData.file[0].key;
            applicationLotFile.lotId = parseInt($jsonData.personId, 10);
            applicationLotFile.partIndex = docPart.partIndex;
            applicationLotFile.part = docPart.part;
            applicationLotFile.setPartId = $jsonData.setPartId;

            applicationLotFiles.push(applicationLotFile);
          }

          return [200];
        })
      .when('POST', '/api/apps/:id/docParts/:setPartId/linkNew',
        function ($params, $jsonData, docs, personLots, applicationsFactory, applicationLotFiles) {
          var application = applicationsFactory.getApplication(parseInt($params.id, 10)),
              person = _(personLots)
                .filter({ lotId: parseInt($jsonData.personId, 10) }).first(),
              doc = _(docs).filter({ docId: parseInt($jsonData.currentDocId, 10) }).first(),
              docFile = _(doc.publicDocFiles)
                .filter({ key: $jsonData.docFileId }).first(),
              docPart = {
            applications: [],
            file: [],
            part: null,
            partIndex: null
          },
            applicationLotFile = {},
            nextApplicationLotFileId = _(applicationLotFiles)
              .pluck('applicationLotFileId').max().value() + 1;

          docPart.applications.push({
            applicationId: parseInt($params.id, 10),
            applicationName: application.doc.docTypeName
          });
          docPart.part = $jsonData.part;
          docPart.partIndex = person.nextIndex++;

          if (parseInt($params.setPartId, 10) === 1) {
            person.personDocumentIds.push(docPart);
            applicationLotFile.setPartName = 'Документ за самоличност';
          }
          else if (parseInt($params.setPartId, 10) === 2) {
            person.personDocumentEducations.push(docPart);
            applicationLotFile.setPartName = 'Образования';
          }
          else if (parseInt($params.setPartId, 10) === 3) {
            person.personDocumentEmployments.push(docPart);
            applicationLotFile.setPartName = 'Месторабота';
          }
          else if (parseInt($params.setPartId, 10) === 4) {
            person.personDocumentMedicals.push(docPart);
            applicationLotFile.setPartName = 'Медицински';
          }
          else if (parseInt($params.setPartId, 10) === 5) {
            person.personDocumentChecks.push(docPart);
            applicationLotFile.setPartName = 'Проверка';
          }
          else if (parseInt($params.setPartId, 10) === 6) {
            person.personDocumentTrainings.push(docPart);
            applicationLotFile.setPartName = '*';
          }
          else if (parseInt($params.setPartId, 10) === 7) {
            person.personDocumentOthers.push(docPart);
            applicationLotFile.setPartName = '*';
          }

          applicationLotFile.applicationLotFileId = nextApplicationLotFileId;
          applicationLotFile.docFileId = docFile.key;
          applicationLotFile.lotId = parseInt($jsonData.personId, 10);
          applicationLotFile.partIndex = docPart.partIndex;
          applicationLotFile.part = $jsonData.part;
          applicationLotFile.setPartId = parseInt($params.setPartId, 10);

          applicationLotFiles.push(applicationLotFile);

          return [200];
        })
      .when('POST', '/api/apps/:id/docParts/:setPartId/linkExisting',
        function ($params, $jsonData, applicationsFactory, personLots, docs, applicationLotFiles) {
          var application = applicationsFactory.getApplication(parseInt($params.id, 10)),
              person = _(personLots)
                .filter({ lotId: parseInt($jsonData.personId, 10) }).first(),
              doc = _(docs).filter({ docId: parseInt($jsonData.currentDocId, 10) }).first(),
              docFile = _(doc.publicDocFiles)
                .filter({ key: $jsonData.docFileId }).first(),
              docPart = null,
              applicationLotFile = {},
              nextApplicationLotFileId = _(applicationLotFiles)
                .pluck('applicationLotFileId').max().value() + 1;

          if (parseInt($params.setPartId, 10) === 1) {
            docPart = _(person.personDocumentIds)
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Документ за самоличност';
          }
          else if (parseInt($params.setPartId, 10) === 2) {
            docPart = _(person.personDocumentEducations)
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Образования';
          }
          else if (parseInt($params.setPartId, 10) === 3) {
            docPart = _(person.personDocumentEmployments)
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Месторабота';
          }
          else if (parseInt($params.setPartId, 10) === 4) {
            docPart = _(person.personDocumentMedicals)
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Медицински';
          }
          else if (parseInt($params.setPartId, 10) === 5) {
            docPart = _(person.personDocumentChecks)
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = 'Проверка';
          }
          else if (parseInt($params.setPartId, 10) === 6) {
            docPart = _(person.personDocumentTrainings)
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = '*';
          }
          else if (parseInt($params.setPartId, 10) === 7) {
            docPart = _(person.personDocumentOthers)
              .filter({ partIndex: $jsonData.partIndex }).first();
            applicationLotFile.setPartName = '*';
          }

          if (docPart) {
            docPart.applications.push(
            {
              applicationId: parseInt($params.id, 10),
              applicationName: application.doc.docTypeName
            });

            applicationLotFile.applicationLotFileId = nextApplicationLotFileId;
            applicationLotFile.docFileId = docFile.key;
            applicationLotFile.lotId = parseInt($jsonData.personId, 10);
            applicationLotFile.part = docPart.part;
            applicationLotFile.partIndex = docPart.partIndex;
            applicationLotFile.setPartId = parseInt($params.setPartId, 10);

            applicationLotFiles.push(applicationLotFile);
          }

          return [200];
        });
  });
}(angular, _));
