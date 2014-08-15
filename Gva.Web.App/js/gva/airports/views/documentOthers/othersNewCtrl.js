/*global angular*/
(function (angular) {
  'use strict';

  function AirportOthersNewCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentOthers,
    airportDocumentOther
  ) {
    $scope.airportDocumentOther = airportDocumentOther;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.newDocumentOtherForm.$valid) {
            return AirportDocumentOthers
              .save({ id: $stateParams.id }, $scope.airportDocumentOther).$promise
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

  AirportOthersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportDocumentOthers',
    'airportDocumentOther'
  ];

  AirportOthersNewCtrl.$resolve = {
    airportDocumentOther: [
      '$stateParams',
      'AirportDocumentOthers',
      function ($stateParams, AirportDocumentOthers) {
        return AirportDocumentOthers.newDocument({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOthersNewCtrl', AirportOthersNewCtrl);
}(angular));
