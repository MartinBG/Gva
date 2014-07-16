/*global angular*/
(function (angular) {
  'use strict';
  function AppSelectOrganizationCtrl($scope, namedModal) {
    $scope.chooseOrganization = function () {
      var params = {
        uin: null,
        name: null
      };

      if ($scope.model.doc && $scope.model.doc.docCorrespondents.length > 0) {
        params.name = $scope.model.doc.docCorrespondents[0].legalEntityName;
        params.uin = $scope.model.doc.docCorrespondents[0].legalEntityBulstat;
      }

      var modalInstance = namedModal.open('chooseOrganization', params, {
        organizations: [
          'Organizations',
          function (Organizations) {
            return Organizations.query(params).$promise;
          }
        ]
      });

      modalInstance.result.then(function (organizationId) {
        $scope.model.lot.id = organizationId;
      });

      return modalInstance.opened;
    };

    $scope.newOrganization = function () {
      var org = {
        organizationData: {}
      };

      if ($scope.model.doc && $scope.model.doc.docCorrespondents.length > 0) {
        org.organizationData.uin = $scope.model.doc.docCorrespondents[0].legalEntityBulstat;
        org.organizationData.name = $scope.model.doc.docCorrespondents[0].legalEntityName;
      }

      var modalInstance = namedModal.open('newOrganization', { organization: org });

      modalInstance.result.then(function (organizationId) {
        $scope.model.lot.id = organizationId;
      });

      return modalInstance.opened;
    };
  }

  AppSelectOrganizationCtrl.$inject = ['$scope', 'namedModal'];

  angular.module('gva').controller('AppSelectOrganizationCtrl', AppSelectOrganizationCtrl);
}(angular));
