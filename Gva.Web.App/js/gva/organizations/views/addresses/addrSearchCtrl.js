/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationAddressesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationAddresses
  ) {
    $scope.organizationAddresses = organizationAddresses;
  }

  OrganizationAddressesSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organizationAddresses'
  ];

  OrganizationAddressesSearchCtrl.$resolve = {
    organizationAddresses: [
      '$stateParams',
      'OrganizationAddresses',
      function ($stateParams, OrganizationAddresses) {
        return OrganizationAddresses.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationAddressesSearchCtrl', OrganizationAddressesSearchCtrl);
}(angular));