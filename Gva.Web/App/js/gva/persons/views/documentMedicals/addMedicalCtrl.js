/*global angular*/
(function (angular) {
  'use strict';

  function AddMedicalCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentMedical,
    personDocumentMedical
  ) {
    $scope.save = function () {
      $scope.personDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.personDocumentMedicalForm.$valid) {
            return PersonDocumentMedical
              .save({ id: $stateParams.id }, $scope.personDocumentMedical).$promise
              .then(function (savedMedical) {
                return $state.go('^', {}, {}, {
                  selectedMedicals: [savedMedical.partIndex]
                });
              });
          }
        });
    };
    $scope.personDocumentMedical = personDocumentMedical;

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  AddMedicalCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentMedical',
    'personDocumentMedical'
  ];

  AddMedicalCtrl.$resolve = {
    personDocumentMedical: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('AddMedicalCtrl', AddMedicalCtrl);
}(angular));