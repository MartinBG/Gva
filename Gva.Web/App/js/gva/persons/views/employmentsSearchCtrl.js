/*global angular*/
(function (angular) {
  'use strict';

  function EmploymentsSearchCtrl($scope, $state, $stateParams, PersonEmployment) {
    PersonEmployment.query($stateParams).$promise.then(function (employments) {
      $scope.employments = employments;
    });

    $scope.editEmployment = function (employment) {
      return $state.go('persons.employments.edit',
        {
          id: $stateParams.id,
          ind: employment.partIndex
        });
    };

    $scope.deleteEmployment = function (employment) {
      return PersonEmployment.remove({ id: $stateParams.id, ind: employment.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newEmployment = function () {
      return $state.go('persons.employments.new');
    };
  }

  EmploymentsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonEmployment'];

  angular.module('gva').controller('EmploymentsSearchCtrl', EmploymentsSearchCtrl);
}(angular));
