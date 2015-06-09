/*global angular, _*/
(function (angular, _) {
  'use strict';

  function SModeCodesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    sModeCodes) {
    $scope.filters = {
      codeHex: null,
      typeId: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.sModeCodes = sModeCodes;

    $scope.search = function () {
      return $state.go('root.sModeCodes.search', {
        codeHex: $scope.filters.codeHex,
        typeId: $scope.filters.typeId
      });
    };

    $scope.newSModeCode = function () {
      return $state.go('root.sModeCodes.new');
    };

  }

  SModeCodesSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'sModeCodes'
  ];

  SModeCodesSearchCtrl.$resolve = {
    sModeCodes: [
      '$stateParams',
      'SModeCodes',
      function ($stateParams, SModeCodes) {
        return SModeCodes.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('SModeCodesSearchCtrl', SModeCodesSearchCtrl);
}(angular, _));
