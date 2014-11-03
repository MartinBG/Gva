/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentEducationCtrl($scope, scFormParams, Persons) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.hideCaseType = scFormParams.hideCaseType;
    $scope.appId = scFormParams.appId;

    $scope.isUniqueDocNumber = function () {
      return Persons
        .isUniqueDocNumber({
          documentNumber: $scope.model.part.documentNumber,
          partIndex: $scope.model.partIndex
        })
      .$promise
      .then(function (result) {
        return result.isUnique;
      });
    };
  }

  PersonDocumentEducationCtrl.$inject = ['$scope', 'scFormParams', 'Persons'];

  angular.module('gva').controller('PersonDocumentEducationCtrl', PersonDocumentEducationCtrl);
}(angular));
