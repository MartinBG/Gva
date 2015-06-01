/*global angular*/
(function (angular) {
  'use strict';

  function CertSmodsSearchCtrl(
    $scope,
    sModeCodes
  ) {
    $scope.sModeCodes = sModeCodes;
  }

  CertSmodsSearchCtrl.$inject = [
    '$scope',
    'sModeCodes'
  ];

  CertSmodsSearchCtrl.$resolve = {
    sModeCodes: [
      '$stateParams',
      'SModeCodes',
      function ($stateParams, SModeCodes) {
        return SModeCodes
          .getSModeCodesPerAircraft({aircraftId: $stateParams.id})
          .$promise;
      }
    ]
  };

  angular.module('gva').controller('CertSmodsSearchCtrl', CertSmodsSearchCtrl);
}(angular));
