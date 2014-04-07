/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApplicationsOrganizationSelectCtrl(
    $scope,
    $state,
    $stateParams,
    Organization,
    selectedOrganization
    ) {
    $scope.filters = {
      name: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    Organization.query($stateParams).$promise.then(function (organizations) {
      $scope.organizations = organizations;
    });

    $scope.search = function () {
      $state.go('root.organizations.search', {
        CAO: $scope.filters.CAO,
        uin: $scope.filters.uin,
        dateValidTo: $scope.filters.dateValidTo,
        dateCAOValidTo: $scope.filters.dateCAOValidTo,
        name: $scope.filters.name
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.selectOrganization = function (result) {
      selectedOrganization.push(result.id);
      return $state.go('^');
    };
  }

  ApplicationsOrganizationSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Organization',
    'selectedOrganization'
  ];

  angular.module('gva').controller(
    'ApplicationsOrganizationSelectCtrl', ApplicationsOrganizationSelectCtrl);
}(angular, _));
