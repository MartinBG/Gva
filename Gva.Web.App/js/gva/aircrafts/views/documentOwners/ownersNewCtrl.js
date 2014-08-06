/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOwnersNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOwners,
    aircraftDocumentOwner
  ) {
    $scope.aircraftDocumentOwner = aircraftDocumentOwner;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.newDocumentOwnerForm.$valid) {
            return AircraftDocumentOwners
              .save({ id: $stateParams.id }, $scope.aircraftDocumentOwner).$promise
              .then(function () {
                return $state.go('root.aircrafts.view.owners.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.owners.search');
    };
  }

  DocumentOwnersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOwners',
    'aircraftDocumentOwner'
  ];

  DocumentOwnersNewCtrl.$resolve = {
    aircraftDocumentOwner: [
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

  angular.module('gva').controller('DocumentOwnersNewCtrl', DocumentOwnersNewCtrl);
}(angular));
