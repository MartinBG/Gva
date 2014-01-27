/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEmploymentsNewCtrl($scope, $state, $stateParams, PersonDocumentEmployment) {
    $scope.save = function () {
      return PersonDocumentEmployment
        .save({ id: $stateParams.id }, $scope.personDocumentEmployment).$promise
        .then(function () {
          return $state.go('persons.employments.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.employments.search');
    };
  }

  DocumentEmploymentsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEmployment'
  ];

  angular.module('gva').controller('DocumentEmploymentsNewCtrl', DocumentEmploymentsNewCtrl);
}(angular));