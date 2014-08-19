/*global angular*/
(function (angular) {
  'use strict';

  function AirportOwnersNewCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentOwners,
    airportDocumentOwner
  ) {
    $scope.airportDocumentOwner = airportDocumentOwner;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.newDocumentOwnerForm.$valid) {
            return AirportDocumentOwners
              .save({ id: $stateParams.id }, $scope.airportDocumentOwner).$promise
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

  AirportOwnersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportDocumentOwners',
    'airportDocumentOwner'
  ];

  AirportOwnersNewCtrl.$resolve = {
    airportDocumentOwner: [
      '$stateParams',
      'AirportDocumentOwners',
      function ($stateParams, AirportDocumentOwners) {
        return AirportDocumentOwners.newOwner({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOwnersNewCtrl', AirportOwnersNewCtrl);
}(angular));
