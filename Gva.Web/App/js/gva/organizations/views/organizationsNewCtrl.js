/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsNewCtrl($scope, $state, Organization, organization) {
    $scope.organization = organization;

    $scope.save = function () {
      return $scope.newOrganizationForm.$validate()
      .then(function () {
        if ($scope.newOrganizationForm.$valid) {
          return Organization.save($scope.organization).$promise
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

  OrganizationsNewCtrl.$inject = ['$scope', '$state', 'Organization', 'organization'];

  OrganizationsNewCtrl.$resolve = {
    organization: function () {
      return {
        organizationData: {}
      };
    }
  };

  angular.module('gva').controller('OrganizationsNewCtrl', OrganizationsNewCtrl);
}(angular));