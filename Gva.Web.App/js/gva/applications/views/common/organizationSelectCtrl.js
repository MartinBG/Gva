/*global angular, _*/
(function (angular, _) {
  'use strict';

  function OrganizationSelectCtrl(
    $scope,
    $state,
    $stateParams,
    Organizations,
    selectedOrganization
    ) {
    $scope.filters = {
      name: null,
      uin: null
    };

    _.forOwn(_.pick($stateParams, ['CAO', 'uin', 'dateValidTo', 'dateCAOValidTo', 'name']),
      function (value, param) {
        if (value !== null && value !== undefined) {
          $scope.filters[param] = value;
        }
      });

    Organizations.query($scope.filters).$promise.then(function (organizations) {
      $scope.organizations = organizations;
    });

    $scope.search = function () {
      $state.go($state.current, _.assign($scope.filters, {
        stamp: new Date().getTime()
      }));
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.selectOrganization = function (result) {
      selectedOrganization.push(result.id);
      return $state.go('^');
    };

    $scope.viewOrganization = function (result) {
      return $state.go('root.organizations.view.edit', { id: result.id });
    };
  }

  OrganizationSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Organizations',
    'selectedOrganization'
  ];

  angular.module('gva').controller(
    'OrganizationSelectCtrl', OrganizationSelectCtrl);
}(angular, _));
