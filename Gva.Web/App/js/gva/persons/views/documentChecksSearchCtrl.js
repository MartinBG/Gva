/*global angular*/
(function (angular) {
  'use strict';

  function DocumentChecksSearchCtrl($scope, $state, $stateParams, PersonDocumentCheck) {
    PersonDocumentCheck.query($stateParams).$promise.then(function (checks) {
      $scope.checks = checks;
    });

    $scope.editDocumentCheck = function (check) {
      return $state.go('persons.checks.edit', { id: $stateParams.id, ind: check.partIndex });
    };

    $scope.deleteDocumentCheck = function (check) {
      return PersonDocumentCheck.remove({ id: $stateParams.id, ind: check.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newDocumentCheck = function () {
      return $state.go('persons.checks.new');
    };
  }

  DocumentChecksSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentCheck'];

  angular.module('gva').controller('DocumentChecksSearchCtrl', DocumentChecksSearchCtrl);
}(angular));
