/*global angular*/
(function (angular) {
  'use strict';

  function DocumentMedicalsEditCtrl($scope, $state, $stateParams, PersonDocumentMedical) {
    PersonDocumentMedical
      .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
      .then(function (medical) {
        $scope.personDocumentMedical = medical;
      });

    $scope.save = function () {
      $scope.personDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.personDocumentMedicalForm.$valid) {
            return PersonDocumentMedical
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentMedical)
              .$promise
              .then(function () {
                return $state.go('persons.medicals.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.medicals.search');
    };
  }

  DocumentMedicalsEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentMedical'];

  angular.module('gva').controller('DocumentMedicalsEditCtrl', DocumentMedicalsEditCtrl);
}(angular));