/*global angular*/
(function (angular) {
  'use strict';

  function DocumentMedicalsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentMedical,
    med
  ) {
    $scope.personDocumentMedical = med;

    $scope.save = function () {
      $scope.personDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.personDocumentMedicalForm.$valid) {
            return PersonDocumentMedical
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentMedical)
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

  DocumentMedicalsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentMedical',
    'med'
  ];

  DocumentMedicalsEditCtrl.$resolve = {
    med: [
      '$stateParams',
      'PersonDocumentMedical',
      function ($stateParams, PersonDocumentMedical) {
        return PersonDocumentMedical.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentMedicalsEditCtrl', DocumentMedicalsEditCtrl);
}(angular));