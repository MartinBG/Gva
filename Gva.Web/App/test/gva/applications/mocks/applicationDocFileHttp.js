/*global angular, _*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    $httpBackendConfiguratorProvider
      .when('POST', '/api/apps/:id/docParts',
        function ($params, $jsonData, gvaApplications, personLots) {
          var gvaApplication = _(gvaApplications)
                .filter({ gvaApplicationId: parseInt($params.id, 10) }).first(),
              person = _(personLots)
                .filter({ lotId: parseInt($jsonData.personId, 10) }).first(),
              docCase = _(gvaApplication.docCase)
                .filter({ docId: parseInt($jsonData.currentDocId, 10) }).first(),
              docPart = {
            applications: [],
            file: [],
            part: null,
            partIndex: null
          };

          docPart.applications.push({
            applicationId: parseInt($params.id, 10),
            applicationName: 'application' + parseInt($params.id, 10)
          });
          docPart.part = $jsonData.part;
          docPart.partIndex = person.nextIndex++;

          if (parseInt($jsonData.setPartId, 10) === 1) {
            person.personDocumentIds.push(docPart);
          }
          else if (parseInt($jsonData.setPartId, 10) === 2) {
            person.personDocumentEducations.push(docPart);
          }
          else if (parseInt($jsonData.setPartId, 10) === 3) {
            person.personDocumentEmployments.push(docPart);
          }
          else if (parseInt($jsonData.setPartId, 10) === 4) {
            person.personDocumentMedicals.push(docPart);
          }
          else if (parseInt($jsonData.setPartId, 10) === 5) {
            person.personDocumentChecks.push(docPart);
          }
          else if (parseInt($jsonData.setPartId, 10) === 6) {
            person.personDocumentTrainings.push(docPart);
          }
          else if (parseInt($jsonData.setPartId, 10) === 7) {
            person.personDocumentOthers.push(docPart);
          }

          //todo add files !
          if (!!$jsonData.file) {
            docCase.docFiles.push(
              {
                docFileId: $jsonData.file[0].key,
                docFileTypeId: null,//$jsonData.fileType.nomTypeValueId,
                docFileTypeName: $jsonData.file[0].name,
                docFileTypeAlias: null,//$jsonData.fileType.alias,
                //todo get lot part in gvaApplications constant
                part: $jsonData.part,
                partIndex: docPart.partIndex,
                setPartId: $jsonData.setPartId,
                gvaLotFileId: 1 //todo ???
              }
            );
          }

          return [200];
        })
      .when('POST', '/api/apps/:id/docParts/:setPartId/linkNew',
        function ($params, $jsonData, gvaApplications, personLots) {
          var gvaApplication = _(gvaApplications)
                .filter({ gvaApplicationId: parseInt($params.id, 10) }).first(),
              person = _(personLots)
                .filter({ lotId: parseInt($jsonData.personId, 10) }).first(),
              docCase = _(gvaApplication.docCase)
                .filter({ docId: parseInt($jsonData.currentDocId, 10) }).first(),
              docFile = _(docCase.docFiles)
                .filter({ docFileId: parseInt($jsonData.docFileId, 10) }).first(),
              docPart = {
            applications: [],
            file: [],
            part: null,
            partIndex: null
          };

          docPart.applications.push({
            applicationId: parseInt($params.id, 10),
            applicationName: 'application' + parseInt($params.id, 10)
          });
          docPart.part = $jsonData.part;
          docPart.partIndex = person.nextIndex++;

          if (parseInt($params.setPartId, 10) === 1) {
            person.personDocumentIds.push(docPart);
          }
          else if (parseInt($params.setPartId, 10) === 2) {
            person.personDocumentEducations.push(docPart);
          }
          else if (parseInt($params.setPartId, 10) === 3) {
            person.personDocumentEmployments.push(docPart);
          }
          else if (parseInt($params.setPartId, 10) === 4) {
            person.personDocumentMedicals.push(docPart);
          }
          else if (parseInt($params.setPartId, 10) === 5) {
            person.personDocumentChecks.push(docPart);
          }
          else if (parseInt($params.setPartId, 10) === 6) {
            person.personDocumentTrainings.push(docPart);
          }
          else if (parseInt($params.setPartId, 10) === 7) {
            person.personDocumentOthers.push(docPart);
          }

          docFile.part = $jsonData.part;
          docFile.partIndex = docPart.partIndex;
          docFile.setPartId = parseInt($params.setPartId, 10);
          docFile.gvaLotFileId = 1;

          return [200];
        })
      .when('POST', '/api/apps/:id/docParts/:setPartId/linkExisting',
        function ($params, $jsonData, gvaApplications, personLots) {
          var gvaApplication = _(gvaApplications)
                .filter({ gvaApplicationId: parseInt($params.id, 10) }).first(),
              person = _(personLots)
                .filter({ lotId: parseInt($jsonData.personId, 10) }).first(),
              docCase = _(gvaApplication.docCase)
                .filter({ docId: parseInt($jsonData.currentDocId, 10) }).first(),
              docFile = _(docCase.docFiles)
                .filter({ docFileId: parseInt($jsonData.docFileId, 10) }).first(),
              docPart = null;

          if (parseInt($params.setPartId, 10) === 1) {
            docPart = _(person.personDocumentIds)
              .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (parseInt($params.setPartId, 10) === 2) {
            docPart = _(person.personDocumentEducations)
              .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (parseInt($params.setPartId, 10) === 3) {
            docPart = _(person.personDocumentEmployments)
              .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (parseInt($params.setPartId, 10) === 4) {
            docPart = _(person.personDocumentMedicals)
              .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (parseInt($params.setPartId, 10) === 5) {
            docPart = _(person.personDocumentChecks)
              .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (parseInt($params.setPartId, 10) === 6) {
            docPart = _(person.personDocumentTrainings)
              .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (parseInt($params.setPartId, 10) === 7) {
            docPart = _(person.personDocumentOthers)
              .filter({ partIndex: $jsonData.partIndex }).first();
          }

          if (docPart) {
            docPart.applications.push(
            {
              applicationId: parseInt($params.id, 10),
              applicationName: 'application' + parseInt($params.id, 10)
            });

            docFile.part = docPart.part;
            docFile.partIndex = docPart.partIndex;
            docFile.setPartId = parseInt($params.setPartId, 10);
            docFile.gvaLotFileId = 1; //todo ?!
          }

          return [200];
        });
  });
}(angular, _));
