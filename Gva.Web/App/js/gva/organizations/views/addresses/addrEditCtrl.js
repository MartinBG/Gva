/*global angular,_*/
(function (angular) {
  'use strict';

  function OrganizationAddressesEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAddress,
    organizationAddress
  ) {
    var originalAddress = _.cloneDeep(organizationAddress);

    $scope.organizationAddress = organizationAddress;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationAddress = _.cloneDeep(originalAddress);
    };

    $scope.save = function () {
      return $scope.editAddressForm.$validate()
      .then(function () {
        if ($scope.editAddressForm.$valid) {
          return OrganizationAddress
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.organizationAddress)
            .$promise
            .then(function () {
              return $state.go('root.organizations.view.addresses.search');
            });
        }
      });
    };

    $scope.deleteAddress = function () {
      return OrganizationAddress.remove({
        id: $stateParams.id,
        ind: organizationAddress.partIndex
      }).$promise.then(function () {
        return $state.go('root.organizations.view.addresses.search');
      });
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
