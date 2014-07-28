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
    'medicals',
    'person',
    'includedMedicals'
  ];

  angular.module('gva').controller('ChooseMedicalsModalCtrl', ChooseMedicalsModalCtrl);
}(angular, _, $));
