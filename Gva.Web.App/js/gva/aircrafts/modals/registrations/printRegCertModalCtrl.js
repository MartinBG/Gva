/*global angular*/
(function (angular) {
  'use strict';

  function PrintRegCertModalCtrl(
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

  PrintRegCertModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams'
  ];

  angular.module('gva').controller('PrintRegCertModalCtrl', PrintRegCertModalCtrl);
}(angular));
