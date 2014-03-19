/*global angular*/
(function (angular) {
  'use strict';
  function OrganizationCertOperatorCtrl($scope) {

    $scope.deleteDocument = function removeDocument(document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.addDocument = function () {
      $scope.model.includedDocuments.push({});
    };

  }

  angular.module('gva')
    .controller('OrganizationCertOperatorCtrl', OrganizationCertOperatorCtrl);
}(angular));