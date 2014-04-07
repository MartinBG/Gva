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

    $scope.viewDocument = function (document) {
      var state;

      if (document.setPartAlias === 'organizationOther') {
        state = 'root.organizations.view.documentOthers.edit';
      }
      else if (document.setPartAlias === 'organizationApplication') {
        state = 'root.organizations.view.documentApplications.edit';
      }

      return $state.go(state, { ind: document.partIndex });
    };
  }

  OrganizationCertOperatorCtrl.$inject = ['$scope','$state'];

  angular.module('gva')
    .controller('OrganizationCertOperatorCtrl', OrganizationCertOperatorCtrl);
}(angular));
