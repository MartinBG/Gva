/*global angular*/
(function (angular) {
  'use strict';

  function ChooseLimitationCtrl(
    $state,
    $stateParams,
    $scope,
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
    'selectedLimitation',
    'limitations'
  ];

  ChooseLimitationCtrl.$resolve = {
    limitations: [
      '$stateParams',
      'Nomenclatures',
      function ($stateParams, Nomenclatures) {
        return Nomenclatures.query({
          alias: $stateParams.limitationAlias
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseLimitationCtrl', ChooseLimitationCtrl);
}(angular));
