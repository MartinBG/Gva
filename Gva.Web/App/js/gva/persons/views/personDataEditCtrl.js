/*global angular*/
(function (angular) {
  'use strict';

  function PersonDataEditCtrl($scope, $state, $stateParams, PersonData) {
    PersonData.get({ id: $stateParams.id }).$promise.then(function (personData) {
      $scope.personData = personData;
    });

    $scope.save = function () {
      return $scope.personData.$save({ id: $stateParams.id }).then(function () {
        return $state.go('persons.view');
      });
    };

    $scope.cancel = function () {
      return $state.go('persons.view');
    };
  }

  PersonDataEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonData'];

  angular.module('gva').controller('PersonDataEditCtrl', PersonDataEditCtrl);
}(angular));
