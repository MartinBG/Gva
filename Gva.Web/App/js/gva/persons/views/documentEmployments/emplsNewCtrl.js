/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEmploymentsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentEmployment,
    employment
  ) {
    $scope.personDocumentEmployment = employment;

    $scope.save = function () {
      $scope.personDocumentEmploymentForm.$validate()
        .then(function () {
          if ($scope.personDocumentEmploymentForm.$valid) {
            return PersonDocumentEmployment
              .save({ id: $stateParams.id }, $scope.personDocumentEmployment).$promise
              .then(function () {
                return $state.go('root.persons.view.employments.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.employments.search');
    };
  }

  DocumentEmploymentsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEmployment',
    'employment'
  ];

  DocumentEmploymentsNewCtrl.$resolve = {
    employment: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('DocumentEmploymentsNewCtrl', DocumentEmploymentsNewCtrl);
}(angular));