/*global angular*/
(function (angular) {
  'use strict';
  function AppSelectOrganizationCtrl($scope, scModal) {
    $scope.chooseOrganization = function () {
      var params = {
        uin: null,
        name: null
      };

      if ($scope.model.doc && $scope.model.doc.docCorrespondents.length > 0) {
        params.name = $scope.model.doc.docCorrespondents[0].legalEntityName;
        params.uin = $scope.model.doc.docCorrespondents[0].legalEntityBulstat;
      }

      var modalInstance = scModal.open('chooseOrganization', params);

      modalInstance.result.then(function (organizationId) {
        $scope.model.lot.id = organizationId;
      });

      return modalInstance.opened;
    };

    $scope.newOrganization = function () {
      var params = {
        uin: null,
        name: null
      };

      if ($scope.model.doc && $scope.model.doc.docCorrespondents.length > 0) {
        params.uin = $scope.model.doc.docCorrespondents[0].legalEntityBulstat;
        params.name = $scope.model.doc.docCorrespondents[0].legalEntityName;
      }

      var modalInstance = scModal.open('newOrganization', params);

      modalInstance.result.then(function (organizationId) {
        $scope.model.lot.id = organizationId;
      });

      return modalInstance.opened;
    };
  }

  AppSelectOrganizationCtrl.$inject = ['$scope', 'scModal'];

  angular.module('gva').controller('AppSelectOrganizationCtrl', AppSelectOrganizationCtrl);
}(angular));
