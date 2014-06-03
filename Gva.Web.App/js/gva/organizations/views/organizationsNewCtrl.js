﻿/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsNewCtrl($scope, $state, Organization, organization, caseType) {
    $scope.organization = organization;
    if (caseType) {
      $scope.organization.organizationData.caseTypes = [caseType];
    }

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

  OrganizationsNewCtrl.$inject = ['$scope', '$state', 'Organization', 'organization', 'caseType'];

  OrganizationsNewCtrl.$resolve = {
    organization: function () {
      return {
        organizationData: {}
      };
    },
    caseType: [
      '$stateParams',
      'Nomenclature',
      function ($stateParams, Nomenclature) {
        if ($stateParams.caseTypeId) {
          return Nomenclature.get({
            alias: 'organizationCaseTypes',
            id: $stateParams.caseTypeId
          }).$promise;
        }

        return null;
      }
    ]
  };

  angular.module('gva').controller('OrganizationsNewCtrl', OrganizationsNewCtrl);
}(angular));