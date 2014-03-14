/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEducationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentEducation,
    edu
  ) {
    $scope.personDocumentCheck = edu;

    $scope.save = function () {
      $scope.personDocumentEducationForm.$validate()
        .then(function () {
          if ($scope.personDocumentEducationForm.$valid) {
            return PersonDocumentEducation
              .save({ id: $stateParams.id }, $scope.personDocumentEducation).$promise
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

  DocumentEducationsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEducation',
    'edu'
  ];

  DocumentEducationsNewCtrl.$resolve = {
    edu: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('DocumentEducationsNewCtrl', DocumentEducationsNewCtrl);
}(angular));
