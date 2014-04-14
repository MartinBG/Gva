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
      return $scope.newDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.newDocumentOtherForm.$valid) {
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
    airportDocumentOther: [
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

  angular.module('gva').controller('AirportOthersNewCtrl', AirportOthersNewCtrl);
}(angular));
