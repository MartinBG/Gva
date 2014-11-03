/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocumentMedicalCtrl($scope, scFormParams, Persons) {
    $scope.personLin = scFormParams.personLin;
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.hideCaseType = scFormParams.hideCaseType;
    $scope.appId = scFormParams.appId;

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

  PersonDocumentMedicalCtrl.$inject = ['$scope', 'scFormParams', 'Persons'];

  angular.module('gva').controller('PersonDocumentMedicalCtrl', PersonDocumentMedicalCtrl);
}(angular));
