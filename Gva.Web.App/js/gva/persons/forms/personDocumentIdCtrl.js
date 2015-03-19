/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocIdCtrl($scope, scFormParams, Persons) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.appId = scFormParams.appId;
    $scope.hideCaseType = scFormParams.hideCaseType;

    $scope.isUniqueDocData = function () {
      return Persons
        .isUniqueDocData({
          documentNumber: $scope.model.part.documentNumber,
          partIndex: $scope.model.partIndex,
          publisher: $scope.model.part.documentPublisher,
          dateValidFrom: $scope.model.part.documentDateValidFrom
        })
      .$promise
      .then(function (result) {
        return result.isUnique;
      });
    };
  }

  PersonDocIdCtrl.$inject = ['$scope', 'scFormParams', 'Persons'];

  angular.module('gva').controller('PersonDocIdCtrl', PersonDocIdCtrl);
}(angular));
