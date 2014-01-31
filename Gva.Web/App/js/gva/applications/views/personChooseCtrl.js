/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonChooseCtrl($scope, $state, $stateParams, Person) {
    $scope.filters = {
      lin: null,
      uin: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    Person.query($stateParams).$promise.then(function (persons) {
      $scope.persons = persons;
    });

    $scope.search = function () {
      $state.go('applications/new/personChoose', {
        lin: $scope.filters.lin,
        uin: $scope.filters.uin,
        names: $scope.filters.names,
        licences: $scope.filters.licences,
        ratings: $scope.filters.ratings,
        organization: $scope.filters.organization
      });
    };

    $scope.cancel = function () {
      goToPreviousState();
    };

    $scope.choosePerson = function (person) {
      $scope.$parent.person = {
          nomTypeValueId: person.id,
          name: person.names,
          content: person
        };

      goToPreviousState();
    };

    function goToPreviousState() {
      var previousState;
      if ($state.$current.parent.self.name === 'applications/new') {
        previousState = 'applications/new/doc';
      }
      else if ($state.$current.parent.self.name === 'applications/link') {
        previousState = 'applications/link/common';
      }

      return $state.go(previousState);
    }
  }

  PersonChooseCtrl.$inject = ['$scope', '$state', '$stateParams', 'Person'];

  angular.module('gva').controller('PersonChooseCtrl', PersonChooseCtrl);
}(angular, _));
