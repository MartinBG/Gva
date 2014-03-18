/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOwnersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOwner,
    aircraftDocumentOwner
  ) {

    $scope.aircraftDocumentOwner = aircraftDocumentOwner;

    $scope.save = function () {
      $scope.aircraftDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.aircraftDocumentOwnerForm.$valid) {
            return AircraftDocumentOwner
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftDocumentOwner)
              .$promise
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

  DocumentOwnersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOwner',
    'aircraftDocumentOwner'
  ];

  DocumentOwnersEditCtrl.$resolve = {
    aircraftDocumentOwner: [
      '$stateParams',
      'AircraftDocumentOwner',
      function ($stateParams, AircraftDocumentOwner) {
        return AircraftDocumentOwner.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentOwnersEditCtrl', DocumentOwnersEditCtrl);
}(angular));