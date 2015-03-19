/*global angular*/
(function (angular) {
  'use strict';

  function PersonStatusCtrl($scope, Persons) {
    $scope.isUniqueDocData = function () {
      return Persons
        .isUniqueDocData({
          documentNumber: $scope.model.part.documentNumber,
          partIndex: $scope.model.partIndex,
          dateValidFrom: $scope.model.part.documentDateValidFrom
        })
      .$promise
      .then(function (result) {
        return result.isUnique;
      });
    };
  }

  PersonStatusCtrl.$inject = ['$scope', 'Persons'];

  angular.module('gva').controller('PersonStatusCtrl', PersonStatusCtrl);
}(angular));
