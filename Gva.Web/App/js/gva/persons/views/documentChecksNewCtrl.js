/*global angular*/
(function (angular) {
  'use strict';

  function DocumentChecksNewCtrl($scope, $state, $stateParams, PersonDocumentCheck) {
    $scope.save = function () {
      return PersonDocumentCheck
        .save({ id: $stateParams.id }, $scope.personDocumentCheck).$promise
        .then(function () {
          return $state.go('persons.checks.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.checks.search');
    };
  }

  DocumentChecksNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentCheck'];

  angular.module('gva').controller('DocumentChecksNewCtrl', DocumentChecksNewCtrl);
}(angular));
