/*global angular, _*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    $httpBackendConfiguratorProvider
      .when('POST', '/api/applications/:id/docFiles',
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

          if ($jsonData.fileType.alias === 'DocumentId') {
            person.personDocumentIds.push(docPart);
          }
          else if ($jsonData.fileType.alias === 'DocumentEducation') {
            person.personDocumentEducations.push(docPart);
          }
          else if ($jsonData.fileType.alias === 'DocumentEmployment') {
            person.personDocumentEmployments.push(docPart);
          }
          else if ($jsonData.fileType.alias === 'DocumentMed') {
            person.personDocumentMedicals.push(docPart);
          }
          else if ($jsonData.fileType.alias === 'DocumentCheck') {
            person.personDocumentChecks.push(docPart);
          }
          else if ($jsonData.fileType.alias === 'DocumentTraining') {
            person.personDocumentTrainings.push(docPart);
          }
          else if ($jsonData.fileType.alias === 'DocumentOther') {
            person.personDocumentOthers.push(docPart);
          }

          //todo add files !
          if (!!$jsonData.file) {
            docCase.docFiles.push(
              {
                docFileId: $jsonData.file[0].key,
                docFileTypeId: $jsonData.fileType.nomTypeValueId,
                docFileTypeName: $jsonData.fileType.name,
                docFileTypeAlias: $jsonData.fileType.alias,
                //todo get lot part in gvaApplications constant
                part: $jsonData.part,
                partIndex: docPart.partIndex,
                gvaLotFileId: 1 //todo ???
              }
            );
          }

          return [200];
        })
      .when('POST', '/api/applications/:id/docFiles/:docFileId/linkNew',
        function ($params, $jsonData, gvaApplications, personLots) {
          var gvaApplication = _(gvaApplications)
            .filter({ gvaApplicationId: parseInt($params.id, 10) }).first(),
            person = _(personLots)
            .filter({ lotId: parseInt($jsonData.personId, 10) }).first(),
            docCase = _(gvaApplication.docCase)
            .filter({ docId: parseInt($jsonData.currentDocId, 10) }).first(),
            docFile = _(docCase.docFiles)
            .filter({ docFileId: parseInt($params.docFileId, 10) }).first(),
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

          if (docFile.docFileTypeAlias === 'DocumentId') {
            person.personDocumentIds.push(docPart);
          }
          else if (docFile.docFileTypeAlias === 'DocumentEducation') {
            person.personDocumentEducations.push(docPart);
          }
          else if (docFile.docFileTypeAlias === 'DocumentEmployment') {
            person.personDocumentEmployments.push(docPart);
          }
          else if (docFile.docFileTypeAlias === 'DocumentMed') {
            person.personDocumentMedicals.push(docPart);
          }
          else if (docFile.docFileTypeAlias === 'DocumentCheck') {
            person.personDocumentChecks.push(docPart);
          }
          else if (docFile.docFileTypeAlias === 'DocumentTraining') {
            person.personDocumentTrainings.push(docPart);
          }
          else if (docFile.docFileTypeAlias === 'DocumentOther') {
            person.personDocumentOthers.push(docPart);
          }

          docFile.part = $jsonData.part;
          docFile.partIndex = docPart.partIndex;
          docFile.gvaLotFileId = 1;

          return [200];
        })
      .when('POST', '/api/applications/:id/docFiles/:docFileId/linkExisting',
        function ($params, $jsonData, gvaApplications, personLots) {
          var gvaApplication = _(gvaApplications)
            .filter({ gvaApplicationId: parseInt($params.id, 10) }).first(),
            person = _(personLots)
            .filter({ lotId: parseInt($jsonData.personId, 10) }).first(),
            docCase = _(gvaApplication.docCase)
            .filter({ docId: parseInt($jsonData.currentDocId, 10) }).first(),
            docFile = _(docCase.docFiles)
            .filter({ docFileId: parseInt($params.docFileId, 10) }).first(),
            docPart = null;

          if (docFile.docFileTypeAlias === 'DocumentId') {
            docPart = _(person.personDocumentIds)
            .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (docFile.docFileTypeAlias === 'DocumentEducation') {
            docPart = _(person.personDocumentEducations)
            .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (docFile.docFileTypeAlias === 'DocumentEmployment') {
            docPart = _(person.personDocumentEmployments)
            .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (docFile.docFileTypeAlias === 'DocumentMed') {
            docPart = _(person.personDocumentMedicals)
            .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (docFile.docFileTypeAlias === 'DocumentCheck') {
            docPart = _(person.personDocumentChecks)
            .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (docFile.docFileTypeAlias === 'DocumentTraining') {
            docPart = _(person.personDocumentTrainings)
            .filter({ partIndex: $jsonData.partIndex }).first();
          }
          else if (docFile.docFileTypeAlias === 'DocumentOther') {
            docPart = _(person.personDocumentOthers)
            .filter({ partIndex: $jsonData.partIndex }).first();
          }

          docPart.applications.push(
            {
              applicationId: parseInt($params.id, 10),
              applicationName: 'application' + parseInt($params.id, 10)
            });

          docFile.part = docPart.part;
          docFile.partIndex = docPart.partIndex;
          docFile.gvaLotFileId = 1; //todo ?!

          return [200];
        });
  });
}(angular, _));
