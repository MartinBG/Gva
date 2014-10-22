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
    $scope.appId = scModalParams.appId;
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
    personDocumentMedical: [
      'PersonDocumentMedicals',
      'scModalParams',
      function (PersonDocumentMedicals, scModalParams) {
        return PersonDocumentMedicals.newMedical({
          id: scModalParams.person.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('NewMedicalModalCtrl', NewMedicalModalCtrl);
}(angular));
