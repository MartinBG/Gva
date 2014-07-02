﻿/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationNewCtrl(
    $scope,
    $state,
    Organizations,
    selectedOrganization
    ) {
    var organizationData = {
      name: $state.payload ? $state.payload.name : null,
      uin: $state.payload ? $state.payload.uin : null
    };

    $scope.organization = {
      organizationData: organizationData
    };

    $scope.save = function () {
      return $scope.newOrganizationForm.$validate()
      .then(function () {
        if ($scope.newOrganizationForm.$valid) {
          return Organizations.save($scope.organization).$promise
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

  OrganizationNewCtrl.$inject = [
    '$scope',
    '$state',
    'Organizations',
    'selectedOrganization'
  ];

  angular.module('gva').controller(
    'OrganizationNewCtrl', OrganizationNewCtrl);
}(angular));
