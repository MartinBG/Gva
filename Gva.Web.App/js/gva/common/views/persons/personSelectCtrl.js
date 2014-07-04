/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonSelectCtrl($scope, $state, $stateParams, Persons, selectedPerson) {
    $scope.filters = {
      ilin: null,
      iuin: null,
      inames: null
    };
    $scope.params = {};

    _.forOwn(_.pick($stateParams,
      ['ilin', 'iuin', 'inames', 'ilicences', 'iratings', 'iorganization']),
      function (value, param) {
        if (value !== null && value !== undefined) {
          $scope.filters[param] = value;
        }
      });

    _.forOwn(_.pick($scope.filters,
      ['ilin', 'iuin', 'inames', 'ilicences', 'iratings', 'iorganization']),
      function (value, param) {
        if (value !== null && value !== undefined) {
          $scope.params[param.substr(1)] = value;
        }
      });

    Persons.query($scope.params).$promise.then(function (persons) {
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
  }

  PersonSelectCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Persons',
    'selectedPerson'
  ];

  angular.module('gva').controller('PersonSelectCtrl', PersonSelectCtrl);
}(angular, _));
