/*global angular*/
(function (angular) {
  'use strict';

  function DocumentIdsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentId,
    docId
  ) {
    $scope.personDocumentId = docId;

    $scope.save = function () {
      $scope.editDocumentIdForm.$validate()
        .then(function () {
          if ($scope.editDocumentIdForm.$valid) {
            return PersonDocumentId
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentId)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentIds.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentIds.search');
    };
  }

  DocumentIdsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentId',
    'docId'
  ];

  DocumentIdsEditCtrl.$resolve = {
    docId: [
      '$stateParams',
      'PersonDocumentId',
      function ($stateParams, PersonDocumentId) {
        return PersonDocumentId.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentIdsEditCtrl', DocumentIdsEditCtrl);
}(angular));
