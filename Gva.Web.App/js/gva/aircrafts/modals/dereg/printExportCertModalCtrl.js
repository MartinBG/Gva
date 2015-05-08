/*global angular*/
(function (angular) {
  'use strict';

  function PrintExportCertModalCtrl(
    $scope,
    $modalInstance,
    scModalParams
  ) {
    $scope.model = {
      lotId: scModalParams.lotId,
      partIndex: scModalParams.partIndex
    };

    $scope.close = function () {
      return $modalInstance.close();
    };
  }

  PrintExportCertModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams'
  ];

  angular.module('gva').controller('PrintExportCertModalCtrl', PrintExportCertModalCtrl);
}(angular));
