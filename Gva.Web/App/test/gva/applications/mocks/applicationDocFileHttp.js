/*global angular, _*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    $httpBackendConfiguratorProvider
      .when('POST', '/api/applications/:id/docFiles',
        function ($params, $jsonData, gvaApplications, personLots) {
          var gvaApplication = _(gvaApplications)
            .filter({ gvaApplicationId: parseInt($params.id, 10) }).first();

          var person = _(personLots).filter({ lotId: parseInt($jsonData.personId, 10) }).first();
          var personDocumentId = {
            applications: [
              {
                applicationId: parseInt($params.id, 10),
                applicationName: 'application' + parseInt($params.id, 10)
              }
            ],
            file: [],
            part: $jsonData.part,
            partIndex: person.nextIndex++
          };

          person.personDocumentIds.push(personDocumentId);

          var docCase = _(gvaApplication.docCase)
            .filter({ docId: parseInt($jsonData.currentDocId, 10) }).first();

          //todo add files !
          docCase.docFiles.push(
            {
              docFileId: $jsonData.file[0].key,
              docFileTypeId: $jsonData.fileType.nomTypeValueId,
              docFileTypeName: $jsonData.fileType.name,
              docFileTypeAlias: $jsonData.fileType.alias,
              //todo get lot part in gvaApplications constant
              part: $jsonData.part,
              partIndex: personDocumentId.partIndex,
              gvaLotFileId: 1 //todo ???
            }
          );

          return [200];
        })
      .when('POST', '/api/applications/:id/docFiles/:docFileId/linkNew',
        function ($params, $jsonData, gvaApplications, personLots) {
          var gvaApplication = _(gvaApplications)
            .filter({ gvaApplicationId: parseInt($params.id, 10) }).first();

          var person = _(personLots).filter({ lotId: parseInt($jsonData.personId, 10) }).first();
          var personDocumentId = {
            applications: [
              {
                applicationId: parseInt($params.id, 10),
                applicationName: 'application' + parseInt($params.id, 10)
              }
            ],
            file: [],
            part: $jsonData.part,
            partIndex: person.nextIndex++
          };

          person.personDocumentIds.push(personDocumentId);

          var docCase = _(gvaApplication.docCase)
            .filter({ docId: parseInt($jsonData.currentDocId, 10) }).first();

          var docFile = _(docCase.docFiles)
            .filter({ docFileId: parseInt($params.docFileId, 10) }).first();

          docFile.part = $jsonData.part;
          docFile.partIndex = personDocumentId.partIndex;
          docFile.gvaLotFileId = 1;

          return [200];
        })
      .when('POST', '/api/applications/:id/docFiles/:docFileId/linkExisting',
        function ($params, $jsonData, gvaApplications, personLots) {
          var gvaApplication = _(gvaApplications)
            .filter({ gvaApplicationId: parseInt($params.id, 10) }).first();

          var person = _(personLots).filter({ lotId: parseInt($jsonData.personId, 10) }).first();

          var personDocumentId = _(person.personDocumentIds)
            .filter({ partIndex: $jsonData.partIndex }).first();

          personDocumentId.applications.push(
            {
              applicationId: parseInt($params.id, 10),
              applicationName: 'application' + parseInt($params.id, 10)
            });

          var docCase = _(gvaApplication.docCase)
            .filter({ docId: parseInt($jsonData.currentDocId, 10) }).first();

          var docFile = _(docCase.docFiles)
            .filter({ docFileId: parseInt($params.docFileId, 10) }).first();

          docFile.part = personDocumentId.part;
          docFile.partIndex = personDocumentId.partIndex;
          docFile.gvaLotFileId = 1;

          return [200];
        });
  });
}(angular, _));
