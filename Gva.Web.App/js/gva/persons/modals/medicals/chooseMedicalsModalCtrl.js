/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseMedicalsModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    medicals
  ) {
    $scope.person = scModalParams.person;
    $scope.selectedMedicals = [];

    $scope.medicals = _.filter(medicals, function (medical) {
      return !_.contains(scModalParams.includedMedicals, medical.partIndex);
    });

    $scope.addMedicals = function () {
      return $modalInstance.close($scope.selectedMedicals);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectMedical = function (event, medical) {
      if ($(event.target).is(':checked')) {
        $scope.selectedMedicals.push(medical);
      }
      else {
        $scope.selectedMedicals = _.without($scope.selectedMedicals, medical);
      }
    };
  }

  ChooseMedicalsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'medicals'
  ];

  ChooseMedicalsModalCtrl.$resolve = {
    medicals: [
      'PersonDocumentMedicals',
      'scModalParams',
      function (PersonDocumentMedicals, scModalParams) {
        return PersonDocumentMedicals.query({ id: scModalParams.lotId }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseMedicalsModalCtrl', ChooseMedicalsModalCtrl);
}(angular, _, $));
