/*global angular*/
(function (angular) {
  'use strict';

  function DocumentIdsNewCtrl($scope, $state, $stateParams, PersonDocumentId) {
    $scope.save = function () {
      $scope.newDocumentIdForm.$validate()
        .then(function () {
          if ($scope.newDocumentIdForm.$valid) {
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

  DocumentIdsNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentId'];

  angular.module('gva').controller('DocumentIdsNewCtrl', DocumentIdsNewCtrl);
}(angular));
