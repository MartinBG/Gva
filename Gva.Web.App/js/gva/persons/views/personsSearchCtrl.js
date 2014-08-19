/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    persons) {
    $scope.filters = {
      lin: null,
      uin: null,
      caseType: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.persons = persons;

    $scope.search = function () {
      return $state.go('root.persons.search', {
        lin: $scope.filters.lin,
        uin: $scope.filters.uin,
        names: $scope.filters.names,
        licences: $scope.filters.licences,
        ratings: $scope.filters.ratings,
        organization: $scope.filters.organization,
        caseType: $scope.filters.caseType
      });
    };

    $scope.newPerson = function () {
      return $state.go('root.persons.new');
    };

  }

  PersonsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'persons'
  ];

  PersonsSearchCtrl.$resolve = {
    persons: [
      '$stateParams',
      'Persons',
      function ($stateParams, Persons) {
        return Persons.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('PersonsSearchCtrl', PersonsSearchCtrl);
}(angular, _));
