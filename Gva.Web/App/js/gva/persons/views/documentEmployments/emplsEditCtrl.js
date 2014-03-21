/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEmploymentsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentEmployment,
    employment
  ) {
    $scope.personDocumentEmployment = employment;

    $scope.save = function () {
      $scope.editDocumentEmploymentForm.$validate()
        .then(function () {
          if ($scope.editDocumentEmploymentForm.$valid) {
            return PersonDocumentEmployment
              .save({ id: $stateParams.id, ind: $stateParams.ind },
              $scope.personDocumentEmployment).$promise
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

  DocumentEmploymentsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEmployment',
    'employment'
  ];

  DocumentEmploymentsEditCtrl.$resolve = {
    employment: [
      '$stateParams',
      'PersonDocumentEmployment',
      function ($stateParams, PersonDocumentEmployment) {
        return PersonDocumentEmployment.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentEmploymentsEditCtrl', DocumentEmploymentsEditCtrl);
}(angular));
