/*global angular*/
(function (angular) {
  'use strict';

  function MedicalsNewCtrl($scope, $state, $stateParams, PersonMedical) {
    $scope.save = function () {
      return PersonMedical
        .save({ id: $stateParams.id }, $scope.personMedical).$promise
        .then(function () {
          return $state.go('persons.medicals.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.medicals.search');
    };
  }

  MedicalsNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonMedical'];

  angular.module('gva').controller('MedicalsNewCtrl', MedicalsNewCtrl);
}(angular));