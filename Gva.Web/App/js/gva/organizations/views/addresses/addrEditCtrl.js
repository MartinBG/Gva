/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationAddressesEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAddress,
    organizationAddress
  ) {
    $scope.organizationAddress = organizationAddress;

    $scope.save = function () {
      return $scope.organizationAddressForm.$validate()
      .then(function () {
        if ($scope.organizationAddressForm.$valid) {
          return OrganizationAddress
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.organizationAddress)
            .$promise
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

  OrganizationAddressesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationAddress',
    'organizationAddress'
  ];

  OrganizationAddressesEditCtrl.$resolve = {
    organizationAddress: [
      '$stateParams',
      'OrganizationAddress',
      function ($stateParams, OrganizationAddress) {
        return OrganizationAddress.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('OrganizationAddressesEditCtrl', OrganizationAddressesEditCtrl);
}(angular));
