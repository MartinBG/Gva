/*global angular*/
(function (angular) {
  'use strict';

  function DocumentMedicalsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentMedicals,
    med
  ) {
    $scope.personDocumentMedical = med;

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
    'med'
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
    ]
  };

  angular.module('gva').controller('DocumentMedicalsNewCtrl', DocumentMedicalsNewCtrl);
}(angular));
