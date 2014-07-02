/*global angular*/
(function (angular) {
  'use strict';

  function AirportOpersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    certOpers
  ) {
    $scope.certOpers = certOpers;

    $scope.editCertOper = function (certOper) {
      return $state.go('root.airports.view.opers.edit',
        {
          id: $stateParams.id,
          ind: certOper.partIndex
        });
    };

    $scope.newCertOper = function () {
      return $state.go('root.airports.view.opers.new');
    };
  }

  AirportOpersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'certOpers'
  ];

  AirportOpersSearchCtrl.$resolve = {
    certOpers: [
      '$stateParams',
      'AirportCertOperationals',
      function ($stateParams, AirportCertOperationals) {
        return AirportCertOperationals.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOpersSearchCtrl', AirportOpersSearchCtrl);
}(angular));
