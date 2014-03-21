/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsViewCtrl(
    $scope,
    $state,
    $stateParams,
    Organization,
    organization
  ) {
    $scope.organization = organization;

    $scope.edit = function () {
      return $state.go('root.organizations.view.edit');
    };
  }

  OrganizationsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Organization',
    'organization'
  ];

  OrganizationsViewCtrl.$resolve = {
    organization: [
      '$stateParams',
      'Organization',
      function ($stateParams, Organization) {
        return Organization.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('OrganizationsViewCtrl', OrganizationsViewCtrl);
}(angular));