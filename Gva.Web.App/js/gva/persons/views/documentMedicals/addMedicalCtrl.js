/*global angular*/
(function (angular) {
  'use strict';

  function AddMedicalCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentMedicals,
    personDocumentMedical
  ) {
    $scope.save = function () {
      $scope.newDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.newDocumentMedicalForm.$valid) {
            return PersonDocumentMedicals
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
    'PersonDocumentMedicals',
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