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
      uin: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.persons = persons;

    $scope.search = function () {
      $state.go('root.persons.search', {
        lin: $scope.filters.lin,
        uin: $scope.filters.uin,
        names: $scope.filters.names,
        licences: $scope.filters.licences,
        ratings: $scope.filters.ratings,
        organization: $scope.filters.organization
      });
    };

    $scope.newPerson = function () {
      return $state.go('root.persons.new');
    };

    $scope.viewPerson = function (person) {
      return $state.go('root.persons.view.licences.search', { id: person.id });
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
        return Persons.query($stateParams).$promise.then(function (persons) {
          return _(persons)
          .forEach(function (person) {
            /*jshint -W052*/
            person.age = ~~((Date.now() - new Date(person.birtDate)) / 31557600000);
            /*jshint +W052*/
            person.licences = _.map(person.licences, function (licence) {
              return licence.licenceType.name;
            }).join(' ,<br />');
            person.ratings = _.map(person.ratings, function (rating) {
              return rating.ratingType.name;
            }).join(' ,<br />');
          }).value();
        });
      }
    ]
  };

  angular.module('gva').controller('PersonsSearchCtrl', PersonsSearchCtrl);
}(angular, _));
