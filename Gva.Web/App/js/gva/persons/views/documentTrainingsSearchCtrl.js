/*global angular*/
(function (angular) {
  'use strict';

  function DocumentTrainingsSearchCtrl($scope, $state, $stateParams, PersonDocumentTraining) {
    PersonDocumentTraining.query($stateParams).$promise.then(function (documentTrainings) {
      $scope.documentTrainings = documentTrainings;
    });

    $scope.editDocumentTraining = function (documentTraining) {
      return $state.go('persons.documentTrainings.edit',
        {
          id: $stateParams.id,
          ind: documentTraining.partIndex
        });
    };

    $scope.deleteDocumentTraining = function (documentTraining) {
      return PersonDocumentTraining.remove({ id: $stateParams.id, ind: documentTraining.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newDocumentTraining = function () {
      return $state.go('persons.documentTrainings.new');
    };
  }

  DocumentTrainingsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentTraining'
  ];

  angular.module('gva').controller('DocumentTrainingsSearchCtrl', DocumentTrainingsSearchCtrl);
}(angular));
