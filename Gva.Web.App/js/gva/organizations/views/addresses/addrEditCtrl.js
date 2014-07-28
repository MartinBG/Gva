/*global angular,_*/
(function (angular) {
  'use strict';

  function OrganizationAddressesEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAddresses,
    organizationAddress,
    scMessage
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
          return OrganizationAddresses
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.organizationAddress)
            .$promise
            .then(function () {
              return $state.go('root.organizations.view.addresses.search');
            });
        }
      });
    };

    $scope.deleteAddress = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return OrganizationAddresses.remove({
            id: $stateParams.id,
            ind: organizationAddress.partIndex
          }).$promise.then(function () {
            return $state.go('root.organizations.view.addresses.search');
          });
        }
      });
    };
  }

  OrganizationAddressesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationAddresses',
    'organizationAddress',
    'scMessage'
  ];

  OrganizationAddressesEditCtrl.$resolve = {
    organizationAddress: [
      '$stateParams',
      'OrganizationAddresses',
      function ($stateParams, OrganizationAddresses) {
        return OrganizationAddresses.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('OrganizationAddressesEditCtrl', OrganizationAddressesEditCtrl);
}(angular));
