/*global angular*/
(function (angular) {
  'use strict';

  function AddressesNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonAddress,
    address
  ) {
    $scope.personAddress = address;

    $scope.save = function () {
      return $scope.personAddressForm.$validate()
        .then(function () {
          if ($scope.personAddressForm.$valid) {
            return PersonAddress
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
    'PersonAddress',
    'address'
  ];

  AddressesNewCtrl.$resolve = {
    address: function () {
      return {};
    }
  };

  angular.module('gva').controller('AddressesNewCtrl', AddressesNewCtrl);
}(angular));
