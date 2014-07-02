/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentMedicalsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentMedicals,
    med
  ) {
    var originalMedical = _.cloneDeep(med);

    $scope.personDocumentMedical = med;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentMedical = _.cloneDeep(originalMedical);
    };

    $scope.save = function () {
      return $scope.editDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.editDocumentMedicalForm.$valid) {
            return PersonDocumentMedicals
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentMedical)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.medicals.search');
              });
          }
        });
    };

    $scope.deleteMedical = function () {
      return PersonDocumentMedicals.remove({ id: $stateParams.id, ind: med.partIndex })
        .$promise.then(function () {
          return $state.go('root.persons.view.medicals.search');
        });
    };
  }

  DocumentMedicalsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentMedicals',
    'med'
  ];

  DocumentMedicalsEditCtrl.$resolve = {
    med: [
      '$stateParams',
      'PersonDocumentMedicals',
      function ($stateParams, PersonDocumentMedicals) {
        return PersonDocumentMedicals.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentMedicalsEditCtrl', DocumentMedicalsEditCtrl);
}(angular));