/*global angular*/
(function (angular) {
  'use strict';

  function NewMedicalModalCtrl(
    $scope,
    $modalInstance,
    $stateParams,
    PersonDocumentMedicals,
    personDocumentMedical
  ) {
    $scope.form = {};
    $scope.personDocumentMedical = personDocumentMedical;

    $scope.save = function () {
      return $scope.form.newDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentMedicalForm.$valid) {
            return PersonDocumentMedicals
              .save({ id: $stateParams.id }, $scope.personDocumentMedical)
              .$promise
              .then(function (savedMedical) {
                return $modalInstance.close(savedMedical.partIndex);
              });
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewMedicalModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    '$stateParams',
    'PersonDocumentMedicals',
    'personDocumentMedical'
  ];

  NewMedicalModalCtrl.$resolve = {
    personDocumentMedical: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('NewMedicalModalCtrl', NewMedicalModalCtrl);
}(angular));
