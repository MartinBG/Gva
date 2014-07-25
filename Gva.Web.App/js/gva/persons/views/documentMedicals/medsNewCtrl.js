/*global angular*/
(function (angular) {
  'use strict';

  function DocumentMedicalsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentMedicals,
    med,
    person
  ) {
    $scope.personDocumentMedical = med;
    $scope.personLin = person.lin;

    $scope.save = function () {
      return $scope.newDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.newDocumentMedicalForm.$valid) {
            return PersonDocumentMedicals
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
    'PersonDocumentMedicals',
    'med',
    'person'
  ];

  DocumentMedicalsNewCtrl.$resolve = {
    med: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {},
            files: [{ isAdded: true, applications: [application] }]
          };
        }
        else {
          return {
            part: {},
            files: []
          };
        }
      }
    ],
    person: [
      '$stateParams',
      'Persons',
      function ($stateParams, Persons) {
        return Persons.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentMedicalsNewCtrl', DocumentMedicalsNewCtrl);
}(angular));
