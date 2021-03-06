﻿/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentEmploymentsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentEmployments,
    employment,
    scMessage
  ) {
    var originalEmpl = _.cloneDeep(employment);

    $scope.personDocumentEmployment = employment;
    $scope.editMode = null;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentEmployment = _.cloneDeep(originalEmpl);
    };

    $scope.save = function () {
      return $scope.editDocumentEmploymentForm.$validate()
        .then(function () {
          if ($scope.editDocumentEmploymentForm.$valid) {
            return PersonDocumentEmployments
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.personDocumentEmployment)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.employments.search', {}, {reload: true});
              });
          }
        });
    };

    $scope.deleteEmployment = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonDocumentEmployments
            .remove({ id: $stateParams.id, ind: $stateParams.ind })
            .$promise.then(function () {
              return $state.go('root.persons.view.employments.search', {}, {reload: true});
            });
        }
      });
    };
  }

  DocumentEmploymentsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentEmployments',
    'employment',
    'scMessage'
  ];

  DocumentEmploymentsEditCtrl.$resolve = {
    employment: [
      '$stateParams',
      'PersonDocumentEmployments',
      function ($stateParams, PersonDocumentEmployments) {
        return PersonDocumentEmployments.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentEmploymentsEditCtrl', DocumentEmploymentsEditCtrl);
}(angular));
