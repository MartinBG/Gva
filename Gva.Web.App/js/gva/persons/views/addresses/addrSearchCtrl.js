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
