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
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
    };

    $scope.save = function () {
      return $scope.editDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.editDocumentMedicalForm.$valid) {
            return PersonDocumentMedical
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentMedical)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.medicals.search');
              });
          }
        });
    };

    $scope.deleteMedical = function () {
      return PersonDocumentMedical.remove({ id: $stateParams.id, ind: med.partIndex })
        .$promise.then(function () {
          return $state.go('root.persons.view.medicals.search');
        });
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
        return PersonDocumentMedical.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentMedicalsEditCtrl', DocumentMedicalsEditCtrl);
}(angular));