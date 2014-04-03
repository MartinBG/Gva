/*global angular*/
(function (angular) {
  'use strict';
  function OrganizationCertOperatorCtrl($scope, $state) {

    $scope.deleteDocument = function (document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.chooseDocuments = function () {
      $state.go($state.current.name + '.chooseDocuments');
    };
  }

  OrganizationCertOperatorCtrl.$inject = ['$scope','$state'];

  angular.module('gva')
    .controller('OrganizationCertOperatorCtrl', OrganizationCertOperatorCtrl);
}(angular));