/*global angular*/
(function (angular) {
  'use strict';

  function AddressesEditCtrl($scope, $state, $stateParams, PersonAddress) {
    PersonAddress
      .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
      .then(function (address) {
        $scope.personAddress = address;
      });

    $scope.save = function () {
      $scope.personAddressForm.$validate()
      .then(function () {
        if ($scope.personAddressForm.$valid) {
          return PersonAddress
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personAddress).$promise
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

  AddressesEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonAddress'];

  angular.module('gva').controller('AddressesEditCtrl', AddressesEditCtrl);
}(angular));
