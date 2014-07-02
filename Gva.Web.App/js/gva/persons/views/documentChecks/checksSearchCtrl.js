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

    $scope.editDocumentCheck = function (check) {
      return $state.go('root.persons.view.checks.edit', {
        id: $stateParams.id,
        ind: check.partIndex
      });
    };

    $scope.newDocumentCheck = function () {
      return $state.go('root.persons.view.checks.new');
    };
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
