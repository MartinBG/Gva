/*global angular*/
(function (angular) {
  'use strict';

  function NewExamModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentTrainings,
    Nomenclatures,
    scModalParams,
    personDocumentExam
  ) {
    $scope.form = {};
    $scope.caseTypeId = scModalParams.caseTypeId;
    $scope.personDocumentExam = personDocumentExam;
    $scope.lotId = scModalParams.lotId;

    $scope.$watch('personDocumentExam.case', function() {
      Nomenclatures
        .get({ alias: 'documentRoles', valueAlias: 'exam' })
        .$promise
        .then(function (theoreticalExamRole) {
          $scope.personDocumentExam.part.documentRoleId = theoreticalExamRole.nomValueId;
        });
    });

    $scope.save = function () {
      return $scope.form.newDocumentExamForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentExamForm.$valid) {
            return PersonDocumentTrainings
              .save({ id: $scope.lotId }, $scope.personDocumentExam)
              .$promise
              .then(function (savedExam) {
                return $modalInstance.close(savedExam);
              });
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewExamModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonDocumentTrainings',
    'Nomenclatures',
    'scModalParams',
    'personDocumentExam'
  ];

  NewExamModalCtrl.$resolve = {
    personDocumentExam: [
      'PersonDocumentTrainings',
      'scModalParams',
      function (PersonDocumentTrainings, scModalParams) {
        return PersonDocumentTrainings.newTraining({
              id: scModalParams.lotId,
              caseTypeId: scModalParams.caseTypeId
            }).$promise;
      }
    ]
  };

  angular.module('gva').controller('NewExamModalCtrl', NewExamModalCtrl);
}(angular));
