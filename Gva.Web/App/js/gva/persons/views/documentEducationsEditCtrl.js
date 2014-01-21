/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEducationsEditCtrl($scope, $state, $stateParams, PersonDocumentEducation) {
    PersonDocumentEducation
    .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
    .then(function (documentEducation) {
      $scope.personDocumentEducation = documentEducation;
    });

    $scope.save = function () {
      return PersonDocumentEducation
        .save(
        { id: $stateParams.id, ind: $stateParams.ind },
        $scope.personDocumentEducation).$promise
        .then(function () {
          return $state.go('persons.documentEducations.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.documentEducations.search');
    };
  }

  DocumentEducationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEducation'
  ];

  angular.module('gva').controller('DocumentEducationsEditCtrl', DocumentEducationsEditCtrl);
}(angular));
