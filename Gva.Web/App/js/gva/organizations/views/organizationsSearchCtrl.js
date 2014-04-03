﻿/*global angular, _*/
(function (angular, _) {
  'use strict';

  function OrganizationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizations
    ) {

    $scope.filters = {
      name: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.organizations = organizations;

    $scope.search = function () {
      $state.go('root.organizations.search', {
        CAO: $scope.filters.CAO,
        uin: $scope.filters.uin,
        dateValidTo: $scope.filters.dateValidTo,
        dateCAOValidTo: $scope.filters.dateCAOValidTo,
        name: $scope.filters.name
      });
    };

    $scope.newOrganization = function () {
      return $state.go('root.organizations.new');
    };

    $scope.viewOrganization = function (organization) {
      return $state.go('root.organizations.view', { id: organization.id });
    };
  }

  OrganizationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organizations'
  ];

  OrganizationsSearchCtrl.$resolve = {
    organizations: [
      '$stateParams',
      'Organization',
      function ($stateParams, Organization) {
        return Organization.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('OrganizationsSearchCtrl', OrganizationsSearchCtrl);
}(angular, _));