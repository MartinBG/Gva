/*global angular*/
(function (angular) {
  'use strict';

  function DocumentMedicalsSearchCtrl(
    $scope,
    $state,
    $stateParams,
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
    'person',
    'meds'
  ];

  DocumentMedicalsSearchCtrl.$resolve = {
    meds: [
      '$stateParams',
      'PersonDocumentMedicals',
      function ($stateParams, PersonDocumentMedicals) {
        return PersonDocumentMedicals.query($stateParams).$promise;
      }
    ]
  };
  angular.module('gva').controller('DocumentMedicalsSearchCtrl', DocumentMedicalsSearchCtrl);
}(angular));
