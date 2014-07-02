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
    $scope.editCertAirworthiness = function (aw) {
      return $state.go('root.aircrafts.view.airworthinesses.edit', {
        id: $stateParams.id,
        ind: aw.partIndex
      });
    };

    $scope.newCertAirworthiness = function () {
      return $state.go('root.aircrafts.view.airworthinesses.new');
    };
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
