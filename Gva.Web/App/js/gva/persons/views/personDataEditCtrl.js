/*global angular*/
(function (angular) {
  'use strict';

  function PersonDataEditCtrl($scope, $state, $stateParams, PersonData) {
    PersonData.get({ id: $stateParams.id }).$promise.then(function (personData) {
      $scope.personData = personData;
    });

    $scope.save = function () {
      $scope.personDataForm.$validate()
       .then(function () {
          if ($scope.personDataForm.$valid) {
            return PersonData
              .save({ id: $stateParams.id }, $scope.personData)
              .$promise
              .then(function () {
                return $state.go('root.persons.view');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view');
    };
  }

  PersonDataEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonData'];

  angular.module('gva').controller('PersonDataEditCtrl', PersonDataEditCtrl);
}(angular));
