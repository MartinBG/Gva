/*global angular*/
(function (angular) {
  'use strict';

  function DocumentIdsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentId,
    docId
  ) {
    $scope.personDocumentId = docId;

    $scope.save = function () {
      return $scope.newDocumentIdForm.$validate()
        .then(function () {
          if ($scope.newDocumentIdForm.$valid) {
            return PersonDocumentId
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
    'PersonDocumentId',
    'docId'
  ];

  DocumentIdsNewCtrl.$resolve = {
    docId: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {},
            files: [{ isAdded: true, applications: [application] }]
          };
        }
        else {
          return {
            part: {},
            files: []
          };
        }
      }
    ]
  };

  angular.module('gva').controller('DocumentIdsNewCtrl', DocumentIdsNewCtrl);
}(angular));
