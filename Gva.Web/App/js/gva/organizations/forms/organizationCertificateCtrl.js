/*global angular*/
(function (angular) {
  'use strict';
  function OrganizationCertificateCtrl($scope, $state, $stateParams) {

    $scope.lotId = $stateParams.id;

    $scope.deleteDocument = function (document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.chooseDocuments = function () {
      $state.go('.chooseDocuments');
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

  OrganizationCertificateCtrl.$inject = ['$scope','$state', '$stateParams'];

  angular.module('gva')
    .controller('OrganizationCertificateCtrl', OrganizationCertificateCtrl);
}(angular));
