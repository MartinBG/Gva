/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentOther,
    airportDocumentOther
  ) {
    var originalDoc = _.cloneDeep(airportDocumentOther);

    $scope.airportDocumentOther = airportDocumentOther;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.airportDocumentOther = _.cloneDeep(originalDoc);
    };

    $scope.save = function () {
      return $scope.editDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.editDocumentOtherForm.$valid) {
            return AirportDocumentOther
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.airportDocumentOther)
              .$promise
              .then(function () {
                return $state.go('root.airports.view.others.search');
              });
          }
        });
    };

    $scope.deleteOther = function () {
      return AirportDocumentOther.remove({
        id: $stateParams.id,
        ind: airportDocumentOther.partIndex
      }).$promise.then(function () {
        return $state.go('root.airports.view.others.search');
      });
    };
  }

  AirportOthersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportDocumentOther',
    'airportDocumentOther'
  ];

  AirportOthersEditCtrl.$resolve = {
    airportDocumentOther: [
      '$stateParams',
      'AirportDocumentOther',
      function ($stateParams, AirportDocumentOther) {
        return AirportDocumentOther.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOthersEditCtrl', AirportOthersEditCtrl);
}(angular, _));