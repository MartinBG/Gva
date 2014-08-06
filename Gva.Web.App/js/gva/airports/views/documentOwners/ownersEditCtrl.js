/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportOwnersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentOwners,
    airportDocumentOwner,
    scMessage
  ) {
    var originalDoc = _.cloneDeep(airportDocumentOwner);

    $scope.airportDocumentOwner = airportDocumentOwner;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.airportDocumentOwner = _.cloneDeep(originalDoc);
    };

    $scope.save = function () {
      return $scope.editDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.editDocumentOwnerForm.$valid) {
            return AirportDocumentOwners
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.airportDocumentOwner)
              .$promise
              .then(function () {
                return $state.go('root.airports.view.owners.search');
              });
          }
        });
    };

    $scope.deleteOwner = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AirportDocumentOwners.remove({
            id: $stateParams.id,
            ind: airportDocumentOwner.partIndex
          }).$promise.then(function () {
            return $state.go('root.airports.view.owners.search');
          });
        }
      });
    };
  }

  AirportOwnersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportDocumentOwners',
    'airportDocumentOwner',
    'scMessage'
  ];

  AirportOwnersEditCtrl.$resolve = {
    airportDocumentOwner: [
      '$stateParams',
      'AirportDocumentOwners',
      function ($stateParams, AirportDocumentOwners) {
        return AirportDocumentOwners.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOwnersEditCtrl', AirportOwnersEditCtrl);
}(angular, _));
