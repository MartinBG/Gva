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

      Nomenclatures.query({ alias: 'electronicServiceProvider' }).$promise.then(function (result) {
        var senderProviderNomId = null;

        if ($scope.model.jObject.senderProvider.electronicServiceProviderType) {
          senderProviderNomId = _(result).filter({
            code: $scope.model.jObject.senderProvider.electronicServiceProviderType
          }).first().nomValueId;
        }

        $scope.senderServiceProvider = {
          obj: {},
          id: senderProviderNomId
        };

        var receiverProviderNomId = null;

        if (!!$scope.model.jObject.receiverProvider.electronicServiceProviderType) {
          receiverProviderNomId = _(result).filter({
            code: $scope.model.jObject.receiverProvider.electronicServiceProviderType
          }).first().nomValueId;
        }

        $scope.receiverServiceProvider = {
          obj: {},
          id: receiverProviderNomId
        };

        _($scope.model.jObject.fileTransferredJurisdiction.documentFileCompetenceCollection)
          .forEach(function (doc) {
            if (doc.structuredDocumentFileCompetence.electronicDocumentXml
              .electronicDocumentXmlContent.any.attachedDocumentUniqueIdentifier) {
            doc.downloadUri =
              'api/abbcdnFile?fileKey=' +
              doc.structuredDocumentFileCompetence.electronicDocumentXml
              .electronicDocumentXmlContent.any.attachedDocumentUniqueIdentifier +
              '&fileName=' +
              doc.structuredDocumentFileCompetence.electronicDocumentXml
              .electronicDocumentXmlContent.any.attachedDocumentFileName +
              '&mimeType=' +
              doc.structuredDocumentFileCompetence.electronicDocumentXml
              .electronicDocumentXmlContent.any.attachedDocumentFileType;
          }
        });

        $scope.isLoaded = true;
      });
    });

    $scope.senderServiceProviderChange = function () {
      $scope.model.jObject.senderProvider.electronicServiceProviderType =
        $scope.senderServiceProvider.obj.code;
      $scope.model.jObject.senderProvider.entityBasicData.identifier =
        $scope.senderServiceProvider.obj.bulstat;
      $scope.model.jObject.senderProvider.entityBasicData.name =
        $scope.senderServiceProvider.obj.name;
    };

    $scope.receiverServiceProviderChange = function () {
      $scope.model.jObject.receiverProvider.electronicServiceProviderType =
        $scope.receiverServiceProvider.obj.code;
      $scope.model.jObject.receiverProvider.entityBasicData.identifier =
        $scope.receiverServiceProvider.obj.bulstat;
      $scope.model.jObject.receiverProvider.entityBasicData.name =
        $scope.receiverServiceProvider.obj.name;
    };

    $scope.viewRioObject = function viewRioObject(registerIndex, batchNumber, abbcdnKey) {
      return Docs
        .createAbbcdnTicket({
          id: $scope.model.docId,
          docTypeUri: registerIndex + '-' + batchNumber,
          abbcdnKey: abbcdnKey
        }, {}).$promise.then(function (data) {
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
