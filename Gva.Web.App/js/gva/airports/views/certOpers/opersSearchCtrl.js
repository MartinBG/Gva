/*global angular*/
(function (angular) {
  'use strict';

  function AirportOpersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AirportCertOperational,
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

    $scope.deleteCertOper = function (certOper) {
      return AirportCertOperational.remove({ id: $stateParams.id, ind: certOper.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'AirportCertOperational',
    'certOpers'
  ];

  AirportOpersSearchCtrl.$resolve = {
    certOpers: [
      '$stateParams',
      'AirportCertOperational',
      function ($stateParams, AirportCertOperational) {
        return AirportCertOperational.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOpersSearchCtrl', AirportOpersSearchCtrl);
}(angular));
