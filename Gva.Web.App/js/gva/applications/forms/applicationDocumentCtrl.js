/*global angular*/

(function (angular) {
  'use strict';

  function AppDocumentCtrl($scope, GvaParts, scFormParams) {
    $scope.caseReadonly = scFormParams.readonly;

    $scope.isUniqueBPN = function () {
      return function () {
        if (!$scope.model.bookPageNumber) {
          return true;
        }

        return GvaParts.isUniqueBPN({
          lotId: scFormParams.lotId,
          caseTypeId: $scope.model.caseType.nomValueId,
          bookPageNumber: $scope.model.bookPageNumber,
          fileId: null
        }).$promise.then(function (data) {
          return data.isUnique;
        });
      };
    };
  }

  AppDocumentCtrl.$inject = ['$scope', 'GvaParts', 'scFormParams'];

  angular.module('gva').controller('AppDocumentCtrl', AppDocumentCtrl);
}(angular));
