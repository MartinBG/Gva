/*global angular, _*/
(function (angular, _) {
  'use strict';
  function OrganizationAmendmentCtrl($scope) {

    $scope.deleteDocument = function removeDocument(document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.addDocument = function () {
      $scope.model.includedDocuments.push({});
    };

    $scope.deleteLimitation147 = function (limitation) {
      var index = $scope.model.lims147.indexOf(limitation);
      $scope.model.lims147.splice(index, 1);
    };

    $scope.addLimitation147 = function () {
      var sortOder = Math.max(0, _.max(_.pluck($scope.model.lims147, 'sortOrder'))) + 1;

      $scope.model.lims147.push({
        sortOrder: sortOder
      });
    };

    $scope.deleteLimitation145= function (limitation) {
      var index = $scope.model.lims145.indexOf(limitation);
      $scope.model.lims145.splice(index, 1);
    };

    $scope.addLimitation145 = function () {
      $scope.model.lims145.push({});
    };

    $scope.deleteLimitationMG = function (limitation) {
      var index = $scope.model.limsMG.indexOf(limitation);
      $scope.model.limsMG.splice(index, 1);
    };

    $scope.addLimitationMG = function () {
      $scope.model.limsMG.push({});
    };
  }

  angular.module('gva').controller('OrganizationAmendmentCtrl', OrganizationAmendmentCtrl);
}(angular, _));
