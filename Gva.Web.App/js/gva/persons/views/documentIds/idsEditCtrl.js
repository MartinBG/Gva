/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentIdsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentIds,
    docId,
    scMessage
  ) {
    var originalDocId = _.cloneDeep(docId);

    $scope.personDocumentId = docId;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentId = _.cloneDeep(originalDocId);
    };

    $scope.save = function () {
      return $scope.editDocumentIdForm.$validate()
        .then(function () {
          if ($scope.editDocumentIdForm.$valid) {
            return PersonDocumentIds
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentId)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentIds.search');
              });
          }
        });
    };

    $scope.deleteDocId = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonDocumentIds.remove({ id: $stateParams.id, ind: $stateParams.ind })
              .$promise.then(function () {
            return $state.go('root.persons.view.documentIds.search');
          });
        }
      });
    };
  }

  DocumentIdsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentIds',
    'docId',
    'scMessage'
  ];

  DocumentIdsEditCtrl.$resolve = {
    docId: [
      '$stateParams',
      'PersonDocumentIds',
      function ($stateParams, PersonDocumentIds) {
        return PersonDocumentIds.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentIdsEditCtrl', DocumentIdsEditCtrl);
}(angular));
