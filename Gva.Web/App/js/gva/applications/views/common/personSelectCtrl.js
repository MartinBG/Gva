/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonSelectCtrl($scope, $state, $stateParams, Person, selectedPerson) {
    $scope.filters = {
      lin: null,
      uin: null,
      names: null
    };

    _.forOwn(_.pick($stateParams, ['lin', 'uin', 'names', 'licences', 'ratings', 'organization']),
      function (value, param) {
        if (value !== null && value !== undefined) {
          $scope.filters[param] = value;
        }
      });

    Person.query($scope.filters).$promise.then(function (persons) {
      $scope.persons = persons;
    });

    $scope.search = function () {
      $state.go($state.current, _.assign($scope.filters, {
        stamp: new Date().getTime()
      }));
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.selectPerson = function (result) {
      selectedPerson.push(result.id);
      return $state.go('^');
    };

    $scope.viewPerson = function (result) {
      return $state.go('root.persons.view.edit', { id: result.id });
    };
  }

  PersonSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Person',
    'selectedPerson'
  ];

  angular.module('gva').controller('PersonSelectCtrl', PersonSelectCtrl);
}(angular, _));
