/*global angular*/
(function (angular) {
  'use strict';

  function EmploymentsNewCtrl($scope, $state, $stateParams, PersonEmployment) {
    $scope.save = function () {
      return PersonEmployment
        .save({ id: $stateParams.id }, $scope.personEmployment).$promise
        .then(function () {
          return $state.go('persons.employments.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.employments.search');
    };
  }

  EmploymentsNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonEmployment'];

  angular.module('gva').controller('EmploymentsNewCtrl', EmploymentsNewCtrl);
}(angular));