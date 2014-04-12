/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOwnersNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOwner,
    aircraftDocumentOwner
  ) {
    $scope.save = function () {
      return $scope.newDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.newDocumentOwnerForm.$valid) {
            return AircraftDocumentOwner
              .save({ id: $stateParams.id }, $scope.aircraftDocumentOwner).$promise
              .then(function () {
                return $state.go('root.aircrafts.view.owners.search');
              });
          }
        });
    };

    $scope.aircraftDocumentOwner = aircraftDocumentOwner;

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.owners.search');
    };
  }

  DocumentOwnersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOwner',
    'aircraftDocumentOwner'
  ];

  DocumentOwnersNewCtrl.$resolve = {
    aircraftDocumentOwner: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {},
            files: [{ applications: [application] }]
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