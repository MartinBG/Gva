/*global angular*/
(function (angular) {
  'use strict';

  function AddressesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonAddress,
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
    'PersonAddress',
    'adresses'
  ];

  AddressesSearchCtrl.$resolve = {
    adresses: [
      '$stateParams',
      'PersonAddress',
      function ($stateParams, PersonAddress) {
        return PersonAddress.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AddressesSearchCtrl', AddressesSearchCtrl);
}(angular));