/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    aws
  ) {
    $scope.aws = aws;
  }

  CertAirworthinessesFMSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'aws'
  ];

  CertAirworthinessesFMSearchCtrl.$resolve = {
    aws: [
      '$stateParams',
      'AircraftCertAirworthinessesFM',
      function ($stateParams, AircraftCertAirworthinessesFM) {
        return AircraftCertAirworthinessesFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
  .controller('CertAirworthinessesFMSearchCtrl', CertAirworthinessesFMSearchCtrl);
}(angular));
