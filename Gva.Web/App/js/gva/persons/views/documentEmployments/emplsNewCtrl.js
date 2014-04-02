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
      return $scope.newDocumentEmploymentForm.$validate()
        .then(function () {
          if ($scope.newDocumentEmploymentForm.$valid) {
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