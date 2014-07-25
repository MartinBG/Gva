/*global angular,_*/
(function (angular) {
  'use strict';

  function AircraftOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOthers,
    aircraftDocumentOther,
    scMessage
  ) {
    var originalDocument = _.cloneDeep(aircraftDocumentOther);

    $scope.aircraftDocumentOther = aircraftDocumentOther;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aircraftDocumentOther = _.cloneDeep(originalDocument);
    };

    $scope.save = function () {
      return $scope.editDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.editDocumentOtherForm.$valid) {
            return AircraftDocumentOthers
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftDocumentOther)
              .$promise
              .then(function () {
                return $state.go('root.aircrafts.view.others.search');
              });
          }
        });
    };

    $scope.deleteDocumentOther = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftDocumentOthers.remove({
            id: $stateParams.id,
            ind: aircraftDocumentOther.partIndex
          }).$promise.then(function () {
            return $state.go('root.aircrafts.view.others.search');
          });
        }
      });
    };
  }

  AircraftOthersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOthers',
    'aircraftDocumentOther',
    'scMessage'
  ];

  AircraftOthersEditCtrl.$resolve = {
    aircraftDocumentOther: [
      '$stateParams',
      'AircraftDocumentOthers',
      function ($stateParams, AircraftDocumentOthers) {
        return AircraftDocumentOthers.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftOthersEditCtrl', AircraftOthersEditCtrl);
}(angular));
