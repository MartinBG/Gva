/*global angular*/
(function (angular) {
  'use strict';

  function DocumentMedicalsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentMedical,
    med
  ) {
    $scope.personDocumentMedical = med;

    $scope.save = function () {
      return $scope.newDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.newDocumentMedicalForm.$valid) {
            return PersonDocumentMedical
              .save({ id: $stateParams.id }, $scope.personDocumentMedical)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.medicals.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.medicals.search');
    };
  }

  DocumentMedicalsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentMedical',
    'med'
  ];

  DocumentMedicalsNewCtrl.$resolve = {
    med: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('DocumentMedicalsNewCtrl', DocumentMedicalsNewCtrl);
}(angular));