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
    $scope.lotId = $stateParams.id;
    $scope.appId = $stateParams.appId;
    $scope.caseTypeId = $stateParams.caseTypeId;

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
      '$stateParams',
      'PersonDocumentEmployments',
      function ($stateParams, PersonDocumentEmployments) {
        return PersonDocumentEmployments.newEmployment({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentEmploymentsNewCtrl', DocumentEmploymentsNewCtrl);
}(angular));
