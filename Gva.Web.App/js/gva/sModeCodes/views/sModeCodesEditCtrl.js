/*global angular, _*/
(function (angular, _) {
  'use strict';

  function SModeCodesEditCtrl(
    $scope, 
    $state,
    $stateParams,
    SModeCodes,
    sModeCode) {
    var originalSModeCode = _.cloneDeep(sModeCode);
    $scope.sModeCode = sModeCode;
    $scope.editMode = null;

    $scope.save = function () {
      return $scope.editSModeCodeForm.$validate()
      .then(function () {
        if ($scope.editSModeCodeForm.$valid) {
          return SModeCodes.save({ id: $stateParams.id }, $scope.sModeCode)
            .$promise
            .then(function () {
              return $state.go('root.sModeCodes.search');
            });
        }
      });
    };

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.sModeCode = _.cloneDeep(originalSModeCode);
      $scope.editMode = null;
    };
  }

  SModeCodesEditCtrl.$inject = [
    '$scope',
    '$state', 
    '$stateParams',
    'SModeCodes',
    'sModeCode'
  ];

  SModeCodesEditCtrl.$resolve = {
    sModeCode: [
      'SModeCodes',
      '$stateParams',
      function (SModeCodes, $stateParams) {
        return SModeCodes.get({id: $stateParams.id}).$promise;
      }
    ]
  };

  angular.module('gva').controller('SModeCodesEditCtrl', SModeCodesEditCtrl);
}(angular, _));
