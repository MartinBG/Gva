/*global angular*/
(function (angular) {
  'use strict';

  function AddressesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    addresses
  ) {

    $scope.addresses = addresses;

    $scope.editAddress = function (address) {
      return $state.go('root.persons.view.addresses.edit', {
        id: $stateParams.id,
        ind: address.partIndex
      });
    };

    $scope.newAddress = function () {
      return $state.go('root.persons.view.addresses.new');
    };
  }

  AddressesSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'adresses'
  ];

  AddressesSearchCtrl.$resolve = {
    adresses: [
      '$stateParams',
      'PersonAddresses',
      function ($stateParams, PersonAddresses) {
        return PersonAddresses.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AddressesSearchCtrl', AddressesSearchCtrl);
}(angular));