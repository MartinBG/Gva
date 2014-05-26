/*global angular*/
(function (angular) {
  'use strict';

  function PortalModalCtrl(
    $scope,
    $sce,
    $modalInstance,
    iframeSrc
  ) {
    $scope.iframeSrc = $sce.trustAsResourceUrl(iframeSrc);

    $scope.cancel = function () {
      $modalInstance.dismiss();
    };
  }

  PortalModalCtrl.$inject = [
    '$scope',
    '$sce',
    '$modalInstance',
    'iframeSrc'
  ];

  angular.module('scaffolding').controller('PortalModalCtrl', PortalModalCtrl);
}(angular));