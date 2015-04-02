/*global angular, _*/
(function (angular, _) {
  'use strict';

  function InvalidActNumbersSearchCtrl(
    $scope,
    Aircrafts,
    invalidActNumbers) {
    $scope.invalidActNumbers = invalidActNumbers;
    $scope.devalidated = true;

    $scope.$watch('actNumber', function () {
      var exists = _.filter($scope.invalidActNumbers, function (entry) {
        return entry.actNumber === $scope.actNumber;
      }).length > 0;

      if(!$scope.actNumber || 
        ($scope.actNumber && exists)) {
        $scope.disableBtn = true;
      } else {
        $scope.disableBtn = false;
      }
    });
    $scope.devalidateActNumber = function () {
      
      return Aircrafts.devalidateActNumber({
          actNumber: $scope.actNumber,
          reason: $scope.reason
        }).$promise
      .then(function (res) {
        $scope.devalidated = res.devalidated;
      });
    };
  }

  InvalidActNumbersSearchCtrl.$inject = [
    '$scope',
    'Aircrafts',
    'invalidActNumbers'
  ];

  InvalidActNumbersSearchCtrl.$resolve = {
    invalidActNumbers: [
      'Aircrafts',
      function (Aircrafts) {
        return Aircrafts.getInvalidActNumbers().$promise;
      }
    ]
  };

  angular.module('gva').controller('InvalidActNumbersSearchCtrl',
    InvalidActNumbersSearchCtrl);
}(angular, _));
