/*global angular,_*/
(function (angular) {
  'use strict';

  function AddressesEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonAddresses,
    address
  ) {
    var originalAddress = _.cloneDeep(address);

    $scope.personAddress = address;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personAddress = _.cloneDeep(originalAddress);
    };

    $scope.save = function () {
      return $scope.editAddressForm.$validate()
        .then(function () {
          if ($scope.editAddressForm.$valid) {
            return PersonAddresses
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personAddress)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.addresses.search');
              });
          }
        });
    };

    $scope.deleteAddress = function () {
      return PersonAddresses.remove({ id: $stateParams.id, ind: address.partIndex })
          .$promise.then(function () {
        return $state.go('root.persons.view.addresses.search');
      });
    };
  }

  AddressesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonAddresses',
    'address'
  ];

  AddressesEditCtrl.$resolve = {
    address: [
      '$stateParams',
      'PersonAddresses',
      function ($stateParams, PersonAddresses) {
        return PersonAddresses.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AddressesEditCtrl', AddressesEditCtrl);
}(angular));
