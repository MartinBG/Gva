/*global angular*/
(function (angular) {
  'use strict';

  function DocFilesViewCtrl($scope, $modal, Doc) {
    $scope.viewEApplication = function viewEApplication(docFileDO) {
      return Doc
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
    'Doc'
  ];

  angular.module('ems').controller('DocFilesViewCtrl', DocFilesViewCtrl);
}(angular));
