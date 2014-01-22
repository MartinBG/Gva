/*global angular*/
(function (angular) {
  'use strict';

  function MedicalsEditCtrl($scope, $state, $stateParams, PersonMedical) {
    PersonMedical
      .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
      .then(function (medical) {
        $scope.personMedical = medical;
      });

    $scope.save = function () {
      return PersonMedical
        .save({id: $stateParams.id, ind: $stateParams.ind}, $scope.personMedical).$promise
        .then(function () {
          return $state.go('persons.medicals.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.medicals.search');
    };
  }

  MedicalsEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonMedical'];

  angular.module('gva').controller('MedicalsEditCtrl', MedicalsEditCtrl);
}(angular));