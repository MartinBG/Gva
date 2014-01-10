﻿/*global angular*/
(function (angular) {
  'use strict';

  function AddressesSearchCtrl($scope, $state, $stateParams, PersonAddress) {
    PersonAddress.query($stateParams).$promise.then(function (addresses) {
      $scope.addresses = addresses;
    });

    $scope.editAddress = function (address) {
      return $state.go('persons.addresses.edit', { id: $stateParams.id, ind: address.partIndex });
    };

    $scope.deleteAddress = function (address) {
      return PersonAddress.remove({ id: $stateParams.id, ind: address.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newAddress = function () {
      return $state.go('persons.addresses.new');
    };
  }

  AddressesSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonAddress'];

  angular.module('gva').controller('AddressesSearchCtrl', AddressesSearchCtrl);
}(angular));