﻿/*global angular*/
(function (angular) {
  'use strict';

  function DocumentMedicalsNewCtrl($scope, $state, $stateParams, PersonDocumentMedical) {
    $scope.save = function () {
      $scope.personDocumentMedicalForm.$validate()
        .then(function () {
          if ($scope.personDocumentMedicalForm.$valid) {
            return PersonDocumentMedical
              .save({ id: $stateParams.id }, $scope.personDocumentMedical).$promise
              .then(function () {
                return $state.go('persons.medicals.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.medicals.search');
    };
  }

  DocumentMedicalsNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentMedical'];

  angular.module('gva').controller('DocumentMedicalsNewCtrl', DocumentMedicalsNewCtrl);
}(angular));