﻿/*global angular*/
(function (angular) {
  'use strict';

  function DocumentEducationsNewCtrl($scope, $state, $stateParams, PersonDocumentEducation) {
    $scope.save = function () {
      return PersonDocumentEducation
        .save({ id: $stateParams.id }, $scope.personDocumentEducation).$promise
        .then(function () {
          return $state.go('persons.documentEducations.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.documentEducations.search');
    };
  }

  DocumentEducationsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEducation'
  ];

  angular.module('gva').controller('DocumentEducationsNewCtrl', DocumentEducationsNewCtrl);
}(angular));
