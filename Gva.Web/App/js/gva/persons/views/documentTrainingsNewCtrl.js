/*global angular*/
(function (angular) {
  'use strict';

  function DocumentTrainingsNewCtrl($scope, $state, $stateParams, PersonDocumentTraining) {
    $scope.save = function () {
      return PersonDocumentTraining
        .save({ id: $stateParams.id }, $scope.personDocumentTraining).$promise
        .then(function () {
          return $state.go('persons.documentTrainings.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.documentTrainings.search');
    };
  }

  DocumentTrainingsNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentTraining'];

  angular.module('gva').controller('DocumentTrainingsNewCtrl', DocumentTrainingsNewCtrl);
}(angular));
