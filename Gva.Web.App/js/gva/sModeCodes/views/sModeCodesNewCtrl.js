/*global angular*/
(function (angular) {
  'use strict';

  function SModeCodesNewCtrl($scope, $state, SModeCodes, sModeCode) {
    $scope.newSModeCode = sModeCode;

    $scope.save = function () {
      return $scope.newSModeCodeForm.$validate()
      .then(function () {
        if ($scope.newSModeCodeForm.$valid) {
          return SModeCodes.save($scope.newSModeCode).$promise
            .then(function (result) {
              return $state.go('root.sModeCodes.edit', { id: result.id });
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.sModeCodes.search');
    };
  }

  SModeCodesNewCtrl.$inject = ['$scope', '$state', 'SModeCodes', 'sModeCode'];

  SModeCodesNewCtrl.$resolve = {
    sModeCode: [
      'SModeCodes',
      function (SModeCodes) {
        return SModeCodes.newSModeCode().$promise;
      }
    ]
  };

  angular.module('gva').controller('SModeCodesNewCtrl', SModeCodesNewCtrl);
}(angular));
