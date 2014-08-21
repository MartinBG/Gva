/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsNewCtrl($scope, $state, Organizations, organization, caseType) {
    $scope.organization = organization;
    if (caseType) {
      $scope.organization.caseTypes = [caseType];
    }

    $scope.save = function () {
      return $scope.newOrganizationForm.$validate()
      .then(function () {
        if ($scope.newOrganizationForm.$valid) {
          return Organizations.save($scope.organization).$promise
            .then(function () {
              return $state.go('root.organizations.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.search');
    };
  }

  OrganizationsNewCtrl.$inject = ['$scope', '$state', 'Organizations', 'organization', 'caseType'];

  OrganizationsNewCtrl.$resolve = {
    organization: [
      'Organizations',
      function (Organizations) {
        return Organizations.newOrganization().$promise;
      }
    ],
    caseType: [
      '$stateParams',
      'Nomenclatures',
      function ($stateParams, Nomenclatures) {
        if ($stateParams.caseTypeId) {
          return Nomenclatures.get({
            alias: 'caseTypes',
            id: $stateParams.caseTypeId
          }).$promise;
        }

        return null;
      }
    ]
  };

  angular.module('gva').controller('OrganizationsNewCtrl', OrganizationsNewCtrl);
}(angular));