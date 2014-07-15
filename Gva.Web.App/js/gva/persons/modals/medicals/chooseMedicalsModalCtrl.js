/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseMedicalsModalCtrl(
    $scope,
    $modalInstance,
    medicals,
    person,
    includedMedicals
  ) {
    $scope.person = person;
    $scope.selectedMedicals = [];

    $scope.medicals = _.filter(medicals, function (medical) {
      return !_.contains(includedMedicals, medical.partIndex);
    });

    $scope.addMedicals = function () {
      return $modalInstance.close($scope.selectedMedicals);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectMedical = function (event, medicalId) {
      if ($(event.target).is(':checked')) {
        $scope.selectedMedicals.push(medicalId);
      }
      else {
        $scope.selectedMedicals = _.without($scope.selectedMedicals, medicalId);
      }
    };
  }

  ChooseMedicalsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'medicals',
    'person',
    'includedMedicals'
  ];

  ChooseMedicalsModalCtrl.$resolve = {
    medicals: [
      '$stateParams',
      'PersonDocumentMedicals',
      function ($stateParams, PersonDocumentMedicals) {
        return PersonDocumentMedicals.query({ id: $stateParams.id }).$promise;
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

  angular.module('gva').controller('ChooseMedicalsModalCtrl', ChooseMedicalsModalCtrl);
}(angular, _, $));
