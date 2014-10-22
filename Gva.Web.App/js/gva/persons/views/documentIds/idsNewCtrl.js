/*global angular*/
(function (angular) {
  'use strict';

  function DocumentIdsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentIds,
    docId
  ) {
    $scope.personDocumentId = docId;
    $scope.lotId = $stateParams.id;
    $scope.appId = $stateParams.appId;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newDocumentIdForm.$validate()
        .then(function () {
          if ($scope.newDocumentIdForm.$valid) {
            return PersonDocumentIds
              .save({ id: $stateParams.id }, $scope.personDocumentId)
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

  DocumentIdsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentIds',
    'docId'
  ];

  DocumentIdsNewCtrl.$resolve = {
    docId: [
      '$stateParams',
      'PersonDocumentIds',
      function ($stateParams, PersonDocumentIds) {
        return PersonDocumentIds.newDocumentId({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentIdsNewCtrl', DocumentIdsNewCtrl);
}(angular));
