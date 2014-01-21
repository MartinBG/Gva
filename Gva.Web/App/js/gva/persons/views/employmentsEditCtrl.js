/*global angular*/
(function (angular) {
  'use strict';

  function EmploymentsEditCtrl($scope, $state, $stateParams, PersonEmployment) {
    PersonEmployment
    .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
    .then(function (employment) {
      $scope.personEmployment = employment;
    });

    $scope.save = function () {
      return PersonEmployment
        .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personEmployment).$promise
        .then(function () {
          return $state.go('persons.employments.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.employments.search');
    };
  }

  EmploymentsEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonEmployment'];

  angular.module('gva').controller('EmploymentsEditCtrl', EmploymentsEditCtrl);
}(angular));
