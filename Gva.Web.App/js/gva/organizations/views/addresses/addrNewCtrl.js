/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationAddressesNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAddresses,
    organizationAddress
    ) {
    $scope.organizationAddress = organizationAddress;

    $scope.save = function () {
      return $scope.newAddressForm.$validate()
        .then(function () {
          if ($scope.newAddressForm.$valid) {
            return OrganizationAddresses
              .save({ id: $stateParams.id }, $scope.organizationAddress).$promise
              .then(function () {
                return $state.go('root.organizations.view.addresses.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.addresses.search');
    };
  }

  OrganizationAddressesNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationAddresses',
    'organizationAddress'
  ];

  OrganizationAddressesNewCtrl.$resolve = {
    organizationAddress: function () {
      return {};
    }
  };

  angular.module('gva').controller('OrganizationAddressesNewCtrl', OrganizationAddressesNewCtrl);
}(angular));
