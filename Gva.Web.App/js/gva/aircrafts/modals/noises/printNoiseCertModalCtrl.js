/*global angular*/
(function (angular) {
  'use strict';

  function PrintNoiseCertModalCtrl(
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

  PrintNoiseCertModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams'
  ];

  angular.module('gva').controller('PrintNoiseCertModalCtrl', PrintNoiseCertModalCtrl);
}(angular));
