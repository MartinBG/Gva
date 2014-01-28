/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEmploymentsSearchCtrl($scope, $state, $stateParams, PersonDocumentEmployment) {
    PersonDocumentEmployment.query($stateParams).$promise.then(function (employments) {
      $scope.employments = employments;
    });

    $scope.editDocumentEmployment = function (employment) {
      return $state.go('persons.employments.edit',
        {
          id: $stateParams.id,
          ind: employment.partIndex
        });
    };

    $scope.deleteDocumentEmployment = function (employment) {
      return PersonDocumentEmployment.remove({ id: $stateParams.id, ind: employment.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newDocumentEmployment = function () {
      return $state.go('persons.employments.new');
    };
  }

  DocumentEmploymentsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEmployment'
  ];

  angular.module('gva').controller('DocumentEmploymentsSearchCtrl', DocumentEmploymentsSearchCtrl);
}(angular));
