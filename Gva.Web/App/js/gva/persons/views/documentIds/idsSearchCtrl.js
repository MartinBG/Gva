/*global angular*/
(function (angular) {
  'use strict';

  function DocumentIdsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentId,
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
    'PersonDocumentId',
    'docIds'
  ];

  DocumentIdsSearchCtrl.$resolve = {
    docIds: [
      '$stateParams',
      'PersonDocumentId',
      function ($stateParams, PersonDocumentId) {
        return PersonDocumentId.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentIdsSearchCtrl', DocumentIdsSearchCtrl);
}(angular));
