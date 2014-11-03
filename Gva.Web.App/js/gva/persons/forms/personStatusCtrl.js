/*global angular*/
(function (angular) {
  'use strict';

  function PersonStatusCtrl($scope, Persons) {
    $scope.isUniqueDocNumber = function () {
      if ($scope.model.part.documentNumber) {
        return Persons
          .isUniqueDocNumber({
            documentNumber: $scope.model.part.documentNumber,
            partIndex: $scope.model.partIndex
          })
        .$promise
        .then(function (result) {
          return result.isUnique;
        });
      } else {
        return true;
      }
    };
  }

  PersonStatusCtrl.$inject = ['$scope', 'Persons'];

  angular.module('gva').controller('PersonStatusCtrl', PersonStatusCtrl);
}(angular));
