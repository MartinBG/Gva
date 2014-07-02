/*global angular*/
(function (angular) {
  'use strict';

  function DocFilesViewCtrl($scope, $modal, Docs) {
    $scope.viewEApplication = function viewEApplication(docFileDO) {
      return Docs
        .createTicket({
          id: docFileDO.docId,
          docFileId: docFileDO.docFileId,
          fileKey: docFileDO.file.key
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
  }

  DocFilesViewCtrl.$inject = [
    '$scope',
    '$modal',
    'Docs'
  ];

  angular.module('ems').controller('DocFilesViewCtrl', DocFilesViewCtrl);
}(angular));
