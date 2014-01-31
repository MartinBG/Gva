/*global angular*/
(function (angular) {
  'use strict';

  function DocumentIdsEditCtrl($scope, $state, $stateParams, PersonDocumentId) {
    PersonDocumentId
    .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
    .then(function (documentId) {
      $scope.personDocumentId = documentId;
    });

    $scope.save = function () {
      $scope.editDocumentIdForm.$validate()
        .then(function () {
          if ($scope.editDocumentIdForm.$valid) {
            return PersonDocumentId
              .save({ id: $stateParams.id }, $scope.personDocumentId)
              .$promise
              .then(function () {
                return $state.go('persons.documentIds.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.documentIds.search');
    };
  }

  DocumentIdsEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentId'];

  angular.module('gva').controller('DocumentIdsEditCtrl', DocumentIdsEditCtrl);
}(angular));
