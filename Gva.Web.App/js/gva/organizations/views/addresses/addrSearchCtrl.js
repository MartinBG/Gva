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

    $scope.editAddress = function (address) {
      return $state.go('root.organizations.view.addresses.edit', {
        id: $stateParams.id,
        ind: address.partIndex
      });
    };

    $scope.newAddress = function () {
      return $state.go('root.organizations.view.addresses.new');
    };
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