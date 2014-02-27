/*global angular*/
(function (angular) {
  'use strict';

  function CertPermitsToFlySearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertPermitToFly,
    permits
  ) {
    $scope.permits = permits;


    $scope.editCertPermit = function (permit) {
      return $state.go('root.aircrafts.view.permits.edit', {
        id: $stateParams.id,
        ind: permit.partIndex
      });
    };

    $scope.deleteCertPermit = function (permit) {
      return AircraftCertPermitToFly.remove({ id: $stateParams.id, ind: permit.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newCertPermit = function () {
      return $state.go('root.aircrafts.view.permits.new');
    };
  }

  CertPermitsToFlySearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertPermitToFly',
    'permits'
  ];

  CertPermitsToFlySearchCtrl.$resolve = {
    permits: [
      '$stateParams',
      'AircraftCertPermitToFly',
      function ($stateParams, AircraftCertPermitToFly) {
        return AircraftCertPermitToFly.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertPermitsToFlySearchCtrl', CertPermitsToFlySearchCtrl);
}(angular));
