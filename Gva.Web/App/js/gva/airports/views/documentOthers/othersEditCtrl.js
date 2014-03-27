/*global angular*/
(function (angular) {
  'use strict';

  function AirportOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentOther,
    airportDocumentOther
  ) {

    $scope.airportDocumentOther = airportDocumentOther;

    $scope.save = function () {
      return $scope.airportDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.airportDocumentOtherForm.$valid) {
            return AirportDocumentOther
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.airportDocumentOther)
              .$promise
              .then(function () {
                return $state.go('root.airports.view.others.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.airports.view.others.search');
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
}(angular));