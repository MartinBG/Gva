/*global angular*/
(function (angular) {
  'use strict';

  function AirportOthersNewCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentOther,
    airportDocumentOther
  ) {
    $scope.save = function () {
      return $scope.airportDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.airportDocumentOtherForm.$valid) {
            return AirportDocumentOther
              .save({ id: $stateParams.id }, $scope.airportDocumentOther).$promise
              .then(function () {
                return $state.go('root.airports.view.others.search');
              });
          }
        });
    };

    $scope.airportDocumentOther = airportDocumentOther;

    $scope.cancel = function () {
      return $state.go('root.airports.view.others.search');
    };
  }

  AirportOthersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportDocumentOther',
    'airportDocumentOther'
  ];

  AirportOthersNewCtrl.$resolve = {
    airportDocumentOther: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('AirportOthersNewCtrl', AirportOthersNewCtrl);
}(angular));