/*global angular*/
(function (angular) {
  'use strict';

  function CommonDocumentApplicationCtrl($scope, scFormParams, Persons) {
    $scope.lotId = scFormParams.lotId;
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.set = scFormParams.set;
    $scope.hideCaseType = scFormParams.hideCaseType;

    $scope.isUniqueDocData = function () {
      if($scope.set === 'person') {
        return Persons
          .isUniqueDocData({
            partIndex: $scope.model.partIndex,
            documentNumber: $scope.model.part.documentNumber,
            dateValidFrom: $scope.model.part.documentDate
          })
          .$promise
          .then(function (result) {
            return result.isUnique;
          });
      }
      else {
        return true;
      }
    };
  }

  CommonDocumentApplicationCtrl.$inject = ['$scope', 'scFormParams', 'Persons'];

  angular.module('gva')
    .controller('CommonDocumentApplicationCtrl', CommonDocumentApplicationCtrl);
}(angular));
