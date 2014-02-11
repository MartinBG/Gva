/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEmploymentsEditCtrl($scope, $state, $stateParams, PersonDocumentEmployment) {
    PersonDocumentEmployment
    .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
    .then(function (employment) {
      $scope.personDocumentEmployment = employment;
    });

    $scope.save = function () {
      $scope.personDocumentEmploymentForm.$validate()
        .then(function () {
          if ($scope.personDocumentEmploymentForm.$valid) {
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
    'PersonDocumentEmployment'
  ];

  angular.module('gva').controller('DocumentEmploymentsEditCtrl', DocumentEmploymentsEditCtrl);
}(angular));
