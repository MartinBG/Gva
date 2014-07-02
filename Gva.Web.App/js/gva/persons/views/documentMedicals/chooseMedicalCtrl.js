/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseMedicalCtrl(
    $state,
    $stateParams,
    $scope,
    medicals
  ) {
    if (!($state.payload && $state.payload.selectedMedicals)) {
      $state.go('^');
      return;
    }

    $scope.selectedMedicals = [];

    $scope.medicals = _.filter(medicals, function (medical) {
      return !_.contains($state.payload.selectedMedicals, medical.partIndex);
    });

    $scope.addMedicals = function () {
      return $state.go('^', {}, {}, {
        selectedMedicals: _.pluck($scope.selectedMedicals, 'partIndex')
      });
    };

    $scope.goBack = function () {
      return $state.go('^');
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

  ChooseMedicalCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'medicals'
  ];

  ChooseMedicalCtrl.$resolve = {
    medicals: [
      '$stateParams',
      'PersonDocumentMedicals',
      function ($stateParams, PersonDocumentMedicals) {
        return PersonDocumentMedicals.query({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseMedicalCtrl', ChooseMedicalCtrl);
}(angular, _, $));
