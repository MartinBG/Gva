/*global angular*/
(function (angular) {
  'use strict';

  function DocumentMedicalsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentMedical,
    person,
    meds
  ) {
    $scope.person = person;
    $scope.medicals = meds;

    $scope.editDocumentMedical = function (medical) {
      return $state.go('root.persons.view.medicals.edit', {
        id: $stateParams.id,
        ind: medical.partIndex
      });
    };

    $scope.newDocumentMedical = function () {
      return $state.go('root.persons.view.medicals.new');
    };
  }

  DocumentMedicalsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentMedical',
    'person',
    'meds'
  ];

  DocumentMedicalsSearchCtrl.$resolve = {
    meds: [
      '$stateParams',
      'PersonDocumentMedical',
      function ($stateParams, PersonDocumentMedical) {
        return PersonDocumentMedical.query($stateParams).$promise;
      }
    ]
  };
  angular.module('gva').controller('DocumentMedicalsSearchCtrl', DocumentMedicalsSearchCtrl);
}(angular));
