/*global angular*/
(function (angular) {
  'use strict';

  function ChooseLimitationModalCtrl(
    $scope,
    $modalInstance,
    limitations
  ) {
    $scope.limitations = limitations;

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectLimitation = function (limitation) {
      return $modalInstance.close(limitation.name);
    };
  }

  ChooseLimitationModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'limitations'
  ];

  angular.module('gva').controller('ChooseLimitationModalCtrl', ChooseLimitationModalCtrl);
}(angular));
