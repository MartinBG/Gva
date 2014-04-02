/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEducationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentEducation,
    edu
  ) {
    $scope.personDocumentEducation = edu;

    $scope.save = function () {
      return $scope.editDocumentEducationForm.$validate()
        .then(function () {
          if ($scope.editDocumentEducationForm.$valid) {
            return PersonDocumentEducation
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.personDocumentEducation)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.documentEducations.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentEducations.search');
    };
  }

  DocumentEducationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEducation',
    'edu'
  ];

  DocumentEducationsEditCtrl.$resolve = {
    edu: [
      '$stateParams',
      'PersonDocumentEducation',
      function ($stateParams, PersonDocumentEducation) {
        return PersonDocumentEducation.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentEducationsEditCtrl', DocumentEducationsEditCtrl);
}(angular));
