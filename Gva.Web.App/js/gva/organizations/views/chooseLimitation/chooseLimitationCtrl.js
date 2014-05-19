/*global angular*/
(function (angular) {
  'use strict';

  function ChooseLimitationCtrl(
    $state,
    $stateParams,
    $scope,
    Nomenclature,
    selectedLimitation,
    limitations
  ) {

    $scope.limitations = limitations;

    $scope.selectLimitation = function (limitation) {
      selectedLimitation.push({
        name: limitation.name,
        index: $stateParams.index,
        limitationAlias: $stateParams.limitationAlias
      });
      return $state.go('^');
    };

    $scope.goBack = function () {
      return $state.go('^');
    };

  }

  ChooseLimitationCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'Nomenclature',
    'selectedLimitation',
    'limitations'
  ];

  ChooseLimitationCtrl.$resolve = {
    limitations: [
      '$stateParams',
      'Nomenclature',
      function ($stateParams, Nomenclature) {
        return Nomenclature.query({
          alias: $stateParams.limitationAlias
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseLimitationCtrl', ChooseLimitationCtrl);
}(angular));
