﻿/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessFM,
    aircraftCertAirworthiness
  ) {
    $scope.isEdit = false;

    $scope.aw = aircraftCertAirworthiness;

    $scope.save = function () {
      return $scope.newCertAirworthinessForm.$validate()
        .then(function () {
          if ($scope.newCertAirworthinessForm.$valid) {
            return AircraftCertAirworthinessFM
                .save({ id: $stateParams.id }, $scope.aw).$promise
                .then(function () {
              return $state.go('root.aircrafts.view.airworthinessesFM.search');
            });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.search');
    };
  }

  CertAirworthinessesFMNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessFM',
    'aircraftCertAirworthiness'
  ];
  CertAirworthinessesFMNewCtrl.$resolve = {
    aircraftCertAirworthiness: [
      '$stateParams',
      function ($stateParams) {
        return {
          part: {
            lotId: $stateParams.id,
            inspector: {
              inspectorType: {
                name: 'Проверяващ',
                alias: 'examiner'
              }
            },
            reviews: [{
              inspector: {
                inspectorType: {
                  name: 'Проверяващ',
                  alias: 'examiner'
                }
              },
              airworthinessReviewType: {
                id: 7777773,
                name: 'Удостоверение за преглед за ЛГ (15a)',
                code: 'AV'
              },
              amendment1: null,
              amendment2: null
            }]
          },
          files: []
        };
      }
    ]
  };

  angular.module('gva').controller('CertAirworthinessesFMNewCtrl', CertAirworthinessesFMNewCtrl);
}(angular));
