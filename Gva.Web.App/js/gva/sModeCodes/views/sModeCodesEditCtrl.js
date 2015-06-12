/*global angular, _*/
(function (angular, _) {
  'use strict';

  function SModeCodesEditCtrl(
    $scope, 
    $state,
    $stateParams,
    SModeCodes,
    sModeCode,
    aircraftRegistration) {
    var originalSModeCode = _.cloneDeep(sModeCode);
    $scope.sModeCode = sModeCode;
    $scope.aircraftRegistration = [aircraftRegistration];
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
    'sModeCode',
    'aircraftRegistration'
  ];

  SModeCodesEditCtrl.$resolve = {
    sModeCode: [
      'SModeCodes',
      '$stateParams',
      function (SModeCodes, $stateParams) {
        return SModeCodes.get({id: $stateParams.id}).$promise;
      }
    ],
    aircraftRegistration: [
      '$stateParams',
      'SModeCodes',
      function ($stateParams, SModeCodes) {
        return SModeCodes.getConnectedRegistration({id: $stateParams.id})
          .$promise
          .then(function (result) {
            return result.registration;
          });
      }
    ]
  };

  angular.module('gva').controller('SModeCodesEditCtrl', SModeCodesEditCtrl);
}(angular, _));
