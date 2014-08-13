/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CompetenceTransferCtrl(
    $scope,
    $modal,
    Docs,
    Nomenclatures,
    scModal
  ) {

    Docs.getRioEditableFile({
      id: $scope.model.docId
    }).$promise.then(function (result) {

      $scope.model.jObject = result.content;

      Nomenclatures.query({ alias: 'electronicServiceProvider' }).$promise
        .then(function (result) {
          var senderProviderNomId = null;

          if (!!$scope.model.jObject
            .SenderProvider.ElectronicServiceProviderType) {
            senderProviderNomId = _(result).filter({
              code: $scope.model.jObject
                .SenderProvider.ElectronicServiceProviderType
            }).first().nomValueId;
          }

          $scope.senderServiceProvider = {
            obj: {},
            id: senderProviderNomId
          };

          var receiverProviderNomId = null;

          if (!!$scope.model.jObject
            .ReceiverProvider.ElectronicServiceProviderType) {
            receiverProviderNomId = _(result).filter({
              code: $scope.model.jObject
                .ReceiverProvider.ElectronicServiceProviderType
            }).first().nomValueId;
          }

          $scope.receiverServiceProvider = {
            obj: {},
            id: receiverProviderNomId
          };

          _($scope.model.jObject.FileTransferredJurisdiction.DocumentFileCompetenceCollection)
            .forEach(function (doc) {
              if (!!doc.StructuredDocumentFileCompetence.ElectronicDocumentXml
                .ElectronicDocumentXmlContent.Any.AttachedDocumentUniqueIdentifier) {
              doc.downloadUri =
                'api/abbcdnFile?fileKey=' +
                doc.StructuredDocumentFileCompetence.ElectronicDocumentXml
                .ElectronicDocumentXmlContent.Any.AttachedDocumentUniqueIdentifier +
                '&fileName=' +
                doc.StructuredDocumentFileCompetence.ElectronicDocumentXml
                .ElectronicDocumentXmlContent.Any.AttachedDocumentFileName +
                '&mimeType=' +
                doc.StructuredDocumentFileCompetence.ElectronicDocumentXml
                .ElectronicDocumentXmlContent.Any.AttachedDocumentFileType;
            }
          });

          $scope.isLoaded = true;
        });
      });

    $scope.senderServiceProviderChange = function () {
      $scope.model.jObject.SenderProvider.ElectronicServiceProviderType =
        $scope.senderServiceProvider.obj.code;
      $scope.model.jObject.SenderProvider.EntityBasicData.Identifier =
        $scope.senderServiceProvider.obj.bulstat;
      $scope.model.jObject.SenderProvider.EntityBasicData.Name =
        $scope.senderServiceProvider.obj.name;
    };

    $scope.receiverServiceProviderChange = function () {
      $scope.model.jObject.ReceiverProvider.ElectronicServiceProviderType =
        $scope.receiverServiceProvider.obj.code;
      $scope.model.jObject.ReceiverProvider.EntityBasicData.Identifier =
        $scope.receiverServiceProvider.obj.bulstat;
      $scope.model.jObject.ReceiverProvider.EntityBasicData.Name =
        $scope.receiverServiceProvider.obj.name;
    };

    $scope.viewRioObject = function viewRioObject(registerIndex, batchNumber, abbcdnKey) {
      return Docs
        .createAbbcdnTicket({
          id: $scope.model.docId,
          docTypeUri: registerIndex + '-' + batchNumber,
          abbcdnKey: abbcdnKey
        }, {})
        .$promise
        .then(function (data) {
          $modal.open({
            templateUrl: 'js/ems/docs/views/portalModal.html',
            controller: 'PortalModalCtrl',
            backdrop: 'static',
            keyboard: false,
            windowClass: 'ems-portal-modal-window',
            resolve: {
              iframeSrc: function () {
                return data.url;
              }
            }
          });
        });
    };

    $scope.sendDoc = function () {
      var modalInstance = scModal.open('sendTransferDoc', {
        docId: $scope.model.docId
      });

      modalInstance.result.then(function () {
        //var newCorr = $scope.docModel.doc.correspondents.slice();
        //newCorr.push(nomItem.nomValueId);
        //$scope.docModel.doc.correspondents = newCorr;
      });

      return modalInstance.opened;
    };
  }

  CompetenceTransferCtrl.$inject = [
    '$scope',
    '$modal',
    'Docs',
    'Nomenclatures',
    'scModal'
  ];

  angular.module('ems').controller('CompetenceTransferCtrl', CompetenceTransferCtrl);
}(angular, _));
