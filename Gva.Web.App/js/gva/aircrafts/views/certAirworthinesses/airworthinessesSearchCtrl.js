/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    aws
  ) {
    $scope.aws = aws;
  }

  CertAirworthinessesSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'marks'
  ];

  CertAirworthinessesSearchCtrl.$resolve = {
    marks: [
      '$stateParams',
      'AircraftCertAirworthinesses',
      function ($stateParams, AircraftCertAirworthinesses) {
        return AircraftCertAirworthinesses.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirworthinessesSearchCtrl', CertAirworthinessesSearchCtrl);
}(angular));
