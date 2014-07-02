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

    $scope.airportDocumentOwner = airportDocumentOwner;

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
      'application',
      function (application) {
        if (application) {
          return {
            part: {},
            files: [{ isAdded: true, applications: [application] }]
          };
        }
        else {
          return {
            part: {},
            files: []
          };
        }
      }
    ]
  };

  angular.module('gva').controller('AirportOwnersNewCtrl', AirportOwnersNewCtrl);
}(angular));
