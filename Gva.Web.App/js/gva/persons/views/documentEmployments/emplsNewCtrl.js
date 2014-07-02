/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEmploymentsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentEmployments,
    employment
  ) {
    $scope.personDocumentEmployment = employment;

    $scope.save = function () {
      return $scope.newDocumentEmploymentForm.$validate()
        .then(function () {
          if ($scope.newDocumentEmploymentForm.$valid) {
            return PersonDocumentEmployments
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
    'PersonDocumentEmployments',
    'employment'
  ];

  DocumentEmploymentsNewCtrl.$resolve = {
    employment: [
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

  angular.module('gva').controller('DocumentEmploymentsNewCtrl', DocumentEmploymentsNewCtrl);
}(angular));
