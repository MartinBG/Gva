/*global angular*/
(function (angular) {
  'use strict';

  function DocumentIdsSearchCtrl($scope, $state, $stateParams, PersonDocumentId) {
    PersonDocumentId.query($stateParams).$promise.then(function (documentIds) {
      $scope.documentIds = documentIds;
    });

    $scope.editDocumentId = function (documentId) {
      return $state.go('root.persons.view.documentIds.edit',
        {
          id: $stateParams.id,
          ind: documentId.partIndex
        });
    };

    $scope.deleteDocumentId = function (documentId) {
      return PersonDocumentId.remove({ id: $stateParams.id, ind: documentId.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newDocumentId = function () {
      return $state.go('root.persons.view.documentIds.new');
    };
  }

  DocumentIdsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentId'];

  angular.module('gva').controller('DocumentIdsSearchCtrl', DocumentIdsSearchCtrl);
}(angular));
