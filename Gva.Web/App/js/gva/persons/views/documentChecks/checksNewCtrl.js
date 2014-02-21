﻿/*global angular*/
(function (angular) {
  'use strict';

  function DocumentChecksNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentCheck,
    personDocumentCheck,
    selectedPublisher
  ) {
    $scope.isEdit = false;

    $scope.personDocumentCheck = personDocumentCheck;
    $scope.personDocumentCheck.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentCheck.part.documentPublisher;
    $scope.choosePublisher = function () {
      return $state.go('root.persons.view.checks.new.choosePublisher');
    };

    $scope.save = function () {
      $scope.personDocumentCheckForm.$validate()
         .then(function () {
            if ($scope.personDocumentCheckForm.$valid) {
              return PersonDocumentCheck
              .save({ id: $stateParams.id }, $scope.personDocumentCheck).$promise
              .then(function () {
                return $state.go('root.persons.view.checks.search');
              });
            }
          });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.checks.search');
    };
  }

  DocumentChecksNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentCheck',
    'personDocumentCheck',
    'selectedPublisher'
  ];
  DocumentChecksNewCtrl.$resolve = {
    personDocumentCheck: function () {
      return {
        part: {}
      };
    },
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentChecksNewCtrl', DocumentChecksNewCtrl);
}(angular));