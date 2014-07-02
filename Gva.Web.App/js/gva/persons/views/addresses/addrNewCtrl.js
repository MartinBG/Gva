/*global angular*/
(function (angular) {
  'use strict';

  function AddressesNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonAddresses,
    address
  ) {
    $scope.personAddress = address;

    $scope.save = function () {
      return $scope.newAddressForm.$validate()
        .then(function () {
          if ($scope.newAddressForm.$valid) {
            return PersonAddresses
              .save({ id: $stateParams.id }, $scope.personAddress)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.addresses.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.addresses.search');
    };
  }

  AddressesNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonAddresses',
    'address'
  ];

  AddressesNewCtrl.$resolve = {
    address: function () {
      return {};
    }
  };

  angular.module('gva').controller('AddressesNewCtrl', AddressesNewCtrl);
}(angular));
