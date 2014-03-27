/*global angular*/
(function (angular) {
  'use strict';

  function AirportOwnersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentOwner,
    airportDocumentOwner
  ) {

    $scope.airportDocumentOwner = airportDocumentOwner;

    $scope.save = function () {
      return $scope.airportDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.airportDocumentOwnerForm.$valid) {
            return AirportDocumentOwner
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.airportDocumentOwner)
              .$promise
              .then(function () {
                return $state.go('root.airports.view.owners.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.airports.view.owners.search');
    };
  }

  AirportOwnersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportDocumentOwner',
    'airportDocumentOwner'
  ];

  AirportOwnersEditCtrl.$resolve = {
    airportDocumentOwner: [
      '$stateParams',
      'AirportDocumentOwner',
      function ($stateParams, AirportDocumentOwner) {
        return AirportDocumentOwner.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOwnersEditCtrl', AirportOwnersEditCtrl);
}(angular));