/*global angular*/
(function (angular) {
  'use strict';

  function NewMedicalModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentMedicals,
    personDocumentMedical,
    person
  ) {
    $scope.form = {};
    $scope.personDocumentMedical = personDocumentMedical;
    $scope.personLin = person.lin;

    $scope.save = function () {
      return $scope.form.newDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentMedicalForm.$valid) {
            return PersonDocumentMedicals
              .save({ id: person.id }, $scope.personDocumentMedical)
              .$promise
              .then(function (savedMedical) {
                return $modalInstance.close(savedMedical);
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
    'PersonDocumentMedicals',
    'personDocumentMedical',
    'person'
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
