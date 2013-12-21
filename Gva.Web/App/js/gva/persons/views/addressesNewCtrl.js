/*global angular*/
(function (angular) {
  'use strict';

  function AddressesNewCtrl($scope, $state, $stateParams, PersonAddress) {
    $scope.personAddress = {
      part: {}
    };

    $scope.save = function () {
      return PersonAddress
        .save({ id: $stateParams.id }, $scope.personAddress).$promise
        .then(function () {
          return $state.go('persons.addresses.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.addresses.search');
    };
  }

  AddressesNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonAddress'];

  angular.module('gva').controller('AddressesNewCtrl', AddressesNewCtrl);
}(angular));
