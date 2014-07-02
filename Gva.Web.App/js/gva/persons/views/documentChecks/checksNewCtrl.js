/*global angular*/
(function (angular) {
  'use strict';

  function DocumentChecksNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentChecks,
    personDocumentCheck,
    selectedPublisher
  ) {
    $scope.personDocumentCheck = personDocumentCheck;
    $scope.personDocumentCheck.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentCheck.part.documentPublisher;
    $scope.choosePublisher = function () {
      return $state.go('root.persons.view.checks.new.choosePublisher');
    };

    $scope.save = function () {
      return $scope.newDocumentCheckForm.$validate()
        .then(function () {
          if ($scope.newDocumentCheckForm.$valid) {
            return PersonDocumentChecks
              .save({ id: $stateParams.id }, $scope.personDocumentCheck)
              .$promise
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
    'PersonDocumentChecks',
    'personDocumentCheck',
    'selectedPublisher'
  ];
  DocumentChecksNewCtrl.$resolve = {
    personDocumentCheck: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {},
            files: [{ isAdded: true, applications: [application] }]
          };
        }
        else {
          return {
            part: {},
            files: []
          };
        }
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentChecksNewCtrl', DocumentChecksNewCtrl);
}(angular));
