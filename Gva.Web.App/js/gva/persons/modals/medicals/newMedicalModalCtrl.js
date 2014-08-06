/*global angular*/
(function (angular) {
  'use strict';

  function NewMedicalModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentMedicals,
    scModalParams,
    personDocumentMedical
  ) {
    $scope.form = {};
    $scope.personDocumentMedical = personDocumentMedical;
    $scope.personLin = scModalParams.person.lin;
    $scope.lotId = scModalParams.person.id;
    $scope.caseTypeId = scModalParams.caseTypeId;

    $scope.save = function () {
      return $scope.form.newDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentMedicalForm.$valid) {
            return PersonDocumentMedicals
              .save({ id: $scope.lotId }, $scope.personDocumentMedical)
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
    'scModalParams',
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
