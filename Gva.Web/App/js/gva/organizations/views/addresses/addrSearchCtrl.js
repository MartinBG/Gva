/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationAddressesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAddress,
    organizationAddresses
  ) {

    $scope.organizationAddresses = organizationAddresses;

    $scope.editAddress = function (address) {
      return $state.go('root.organizations.view.addresses.edit', {
        id: $stateParams.id,
        ind: address.partIndex
      });
    };

    $scope.deleteAddress = function (address) {
      return OrganizationAddress.remove({ id: $stateParams.id, ind: address.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'OrganizationAddress',
    'organizationAddresses'
  ];

  OrganizationAddressesSearchCtrl.$resolve = {
    organizationAddresses: [
      '$stateParams',
      'OrganizationAddress',
      function ($stateParams, OrganizationAddress) {
        return OrganizationAddress.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationAddressesSearchCtrl', OrganizationAddressesSearchCtrl);
}(angular));