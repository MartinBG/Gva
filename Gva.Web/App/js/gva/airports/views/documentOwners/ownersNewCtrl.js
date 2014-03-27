/*global angular*/
(function (angular) {
  'use strict';

  function AirportOwnersNewCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentOwner,
    airportDocumentOwner
  ) {
    $scope.save = function () {
      return $scope.airportDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.airportDocumentOwnerForm.$valid) {
            return AirportDocumentOwner
              .save({ id: $stateParams.id }, $scope.airportDocumentOwner).$promise
              .then(function () {
                return $state.go('root.airports.view.owners.search');
              });
          }
        });
    };

    $scope.airportDocumentOwner = airportDocumentOwner;

    $scope.cancel = function () {
      return $state.go('root.airports.view.owners.search');
    };
  }

  AirportOwnersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportDocumentOwner',
    'airportDocumentOwner'
  ];

  AirportOwnersNewCtrl.$resolve = {
    airportDocumentOwner: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('AirportOwnersNewCtrl', AirportOwnersNewCtrl);
}(angular));