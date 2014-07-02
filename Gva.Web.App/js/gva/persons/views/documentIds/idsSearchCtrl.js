/*global angular*/
(function (angular) {
  'use strict';

  function DocumentIdsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    docIds
  ) {
    $scope.documentIds = docIds;

    $scope.editDocumentId = function (documentId) {
      return $state.go('root.persons.view.documentIds.edit',
        {
          id: $stateParams.id,
          ind: documentId.partIndex
        });
    };

    $scope.newDocumentId = function () {
      return $state.go('root.persons.view.documentIds.new');
    };
  }

  DocumentIdsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'docIds'
  ];

  DocumentIdsSearchCtrl.$resolve = {
    docIds: [
      '$stateParams',
      'PersonDocumentIds',
      function ($stateParams, PersonDocumentIds) {
        return PersonDocumentIds.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentIdsSearchCtrl', DocumentIdsSearchCtrl);
}(angular));
