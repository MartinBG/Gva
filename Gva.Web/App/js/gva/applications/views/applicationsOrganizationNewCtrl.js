/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsOrganizationNewCtrl(
    $scope,
    $state,
    Organization,
    selectedOrganization
    ) {

    $scope.save = function () {
      return $scope.organizationDataForm.$validate()
      .then(function () {
        if ($scope.organizationDataForm.$valid) {
          return Organization.save($scope.newOrganization).$promise
            .then(function (organization) {
              selectedOrganization.push(organization.id);
              return $state.go('^');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  ApplicationsOrganizationNewCtrl.$inject = [
    '$scope',
    '$state',
    'Organization',
    'selectedOrganization'
  ];

  angular.module('gva').controller(
    'ApplicationsOrganizationNewCtrl', ApplicationsOrganizationNewCtrl);
}(angular));
