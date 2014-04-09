﻿/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessFM,
    aws
  ) {
    $scope.aws = aws;


    $scope.editCertAirworthiness = function (aw) {
      return $state.go('root.aircrafts.view.airworthinessesFM.edit', {
        id: $stateParams.id,
        ind: aw.partIndex
      });
    };

    $scope.newCertAirworthiness = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.new');
    };
  }

  CertAirworthinessesFMSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessFM',
    'marks'
  ];

  CertAirworthinessesFMSearchCtrl.$resolve = {
    marks: [
      '$stateParams',
      'AircraftCertAirworthinessFM',
      function ($stateParams, AircraftCertAirworthinessFM) {
        return AircraftCertAirworthinessFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
  .controller('CertAirworthinessesFMSearchCtrl', CertAirworthinessesFMSearchCtrl);
}(angular));
