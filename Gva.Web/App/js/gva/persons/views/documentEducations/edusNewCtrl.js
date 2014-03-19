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
    $scope.personDocumentEducation = edu;

    $scope.save = function () {
      $scope.newDocumentEducationForm.$validate()
        .then(function () {
          if ($scope.newDocumentEducationForm.$valid) {
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
