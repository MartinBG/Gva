/*global angular, _*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
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
      .when('GET', '/api/applications',
        function ($params, $filter, applicationsFactory) {
          var applications = _(applicationsFactory.getAll()).map(function (application) {
            if (application.lotId) { //todo change person data better
              application.person = personMapper(application.personData);
            }
            return application;
          }).value();

          return [200, applications];
        })
      .when('GET', '/api/applications/:id',
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
      .when('POST', '/api/applications',
        function ($jsonData, applicationsFactory, docs) {
          if (!$jsonData || !$jsonData.docId || !$jsonData.lotId) {
            return [400];
          }

          $jsonData.applicationId = applicationsFactory.getNextApplicationId();
          applicationsFactory.saveApplication($jsonData);

          var doc = _(docs).filter({ docId: $jsonData.docId }).first();

          doc.applicationId = $jsonData.applicationId;

          return [200, $jsonData];
        })
      .when('GET', '/api/nomenclatures/persons',
        function (personLots) {
          var noms = [],
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
        })
      .when('POST', '/api/applications/validate/exist',
        function ($jsonData, applicationsFactory) {
          var exist = false;

          if ($jsonData && $jsonData.docId && $jsonData.lotId) {
            exist =
              !!applicationsFactory.getApplicationByDocAndPerson($jsonData.docId, $jsonData.lotId);
          }

          return [200, { applicationExist: exist }];
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
          docPart.applications.push({
            applicationId: parseInt($params.id, 10),
            applicationName: application.doc.docTypeName
          });

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
            applicationLotFile.setPartName = '*';
          }
          else if ($jsonData.setPartAlias === 'DocumentOther') {
            person.personDocumentOthers = person.personDocumentOthers || [];
            person.personDocumentOthers.push(docPart);
            applicationLotFile.setPartName = '*';
          }

          if (!!$jsonData.files.length) {
            doc.publicDocFiles.push($jsonData.files[0]);

            applicationLotFile.applicationLotFileId = nextApplicationLotFileId;
            applicationLotFile.docFileKey = $jsonData.files[0].key;
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
            return _(doc.publicDocFiles).filter({ key: $jsonData.docFileKey }).first();
          }).first();
          var docFile = _(doc.publicDocFiles).filter({ key: $jsonData.docFileKey }).first();
          var applicationLotFile = {},
              docPart = {},
              nextApplicationLotFileId = _(applicationLotFiles)
                .pluck('applicationLotFileId').max().value() + 1;

          docPart.applications = [];
          docPart.file = [];
          docPart.part = $jsonData.part;
          docPart.partIndex = person.nextIndex++;
          docPart.applications.push({
            applicationId: parseInt($params.id, 10),
            applicationName: application.doc.docTypeName
          });

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
            applicationLotFile.setPartName = '*';
          }
          else if ($jsonData.setPartAlias === 'DocumentOther') {
            person.personDocumentOthers = person.personDocumentOthers || [];
            person.personDocumentOthers.push(docPart);
            applicationLotFile.setPartName = '*';
          }

          applicationLotFile.applicationLotFileId = nextApplicationLotFileId;
          applicationLotFile.docFileKey = docFile.key;
          applicationLotFile.lotId = application.lotId;
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
            return _(doc.publicDocFiles).filter({ key: $jsonData.docFileKey }).first();
          }).first();
          var docFile = _(doc.publicDocFiles).filter({ key: $jsonData.docFileKey }).first();
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
            applicationLotFile.setPartName = '*';
          }
          else if ($jsonData.setPartAlias === 'DocumentOther') {
            docPart = _(person.personDocumentOthers || [])
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
            applicationLotFile.docFileKey = docFile.key;
            applicationLotFile.lotId = parseInt($jsonData.personId, 10);
            applicationLotFile.part = docPart.part;
            applicationLotFile.partIndex = docPart.partIndex;
            applicationLotFile.setPartAlias = $jsonData.setPartAlias;
            applicationLotFiles.push(applicationLotFile);
          }

          return [200];
        });
  });
}(angular, _));
