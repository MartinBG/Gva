/*global angular*/
(function (angular) {
  'use strict';

  function DocumentChecksSearchCtrl(
    $scope,
    $state,
    $stateParams,
    checks
  ) {
    $scope.checks = checks;
  }

  DocumentChecksSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'checks'
  ];

  DocumentChecksSearchCtrl.$resolve = {
    checks: [
      '$stateParams',
      'PersonDocumentChecks',
      function ($stateParams, PersonDocumentChecks) {
        return PersonDocumentChecks.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentChecksSearchCtrl', DocumentChecksSearchCtrl);
}(angular));
