/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentIdsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentId,
    docId
  ) {
    var originalDocId = _.cloneDeep(docId);

    $scope.personDocumentId = docId;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentId.part = _.cloneDeep(originalDocId.part);
      $scope.$broadcast('cancel', originalDocId);
    };

    $scope.save = function () {
      return $scope.editDocumentIdForm.$validate()
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

    $scope.deleteDocId = function () {
      return PersonDocumentId.remove({ id: $stateParams.id, ind: docId.partIndex })
          .$promise.then(function () {
        return $state.go('root.persons.view.documentIds.search');
      });
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
        return PersonDocumentId.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentIdsEditCtrl', DocumentIdsEditCtrl);
}(angular));
