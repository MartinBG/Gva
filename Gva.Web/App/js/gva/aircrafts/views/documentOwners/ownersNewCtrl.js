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
      $scope.aircraftDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.aircraftDocumentOwnerForm.$valid) {
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
    aircraftDocumentOwner: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('DocumentOwnersNewCtrl', DocumentOwnersNewCtrl);
}(angular));