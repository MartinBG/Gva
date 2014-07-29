/*global angular*/
(function (angular) {
  'use strict';

  function NewMedicalModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentMedicals,
    personDocumentMedical,
    person,
    caseTypeId
  ) {
    $scope.form = {};
    $scope.personDocumentMedical = personDocumentMedical;
    $scope.personLin = person.lin;
    $scope.lotId = person.id;
    $scope.caseTypeId = caseTypeId;

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
    'person',
    'caseTypeId'
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
