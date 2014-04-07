/*global angular,_*/
(function (angular) {
  'use strict';

  function AddressesEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonAddress,
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
      return $scope.personAddressForm.$validate()
        .then(function () {
          if ($scope.personAddressForm.$valid) {
            return PersonAddress
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personAddress)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.addresses.search');
              });
          }
        });
    };

    $scope.deleteAddress = function () {
      return PersonAddress.remove({ id: $stateParams.id, ind: address.partIndex })
          .$promise.then(function () {
        return $state.go('root.persons.view.addresses.search');
      });
    };
  }

  AddressesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonAddress',
    'address'
  ];

  AddressesEditCtrl.$resolve = {
    address: [
      '$stateParams',
      'PersonAddress',
      function ($stateParams, PersonAddress) {
        return PersonAddress.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AddressesEditCtrl', AddressesEditCtrl);
}(angular));
