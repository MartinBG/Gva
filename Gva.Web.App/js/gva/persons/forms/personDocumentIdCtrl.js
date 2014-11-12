/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocIdCtrl($scope, scFormParams, Persons) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.appId = scFormParams.appId;
    $scope.hideCaseType = scFormParams.hideCaseType;

    $scope.isUniqueDocNumber = function () {
      if($scope.model.part.documentNumber) {
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

  PersonDocIdCtrl.$inject = ['$scope', 'scFormParams', 'Persons'];

  angular.module('gva').controller('PersonDocIdCtrl', PersonDocIdCtrl);
}(angular));
