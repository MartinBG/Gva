/*global angular*/
(function (angular) {
  'use strict';

  function DocumentChecksEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentCheck,
    personDocumentCheck,
    selectedPublisher
  ) {
    $scope.isEdit = true;

    $scope.personDocumentCheck = personDocumentCheck;
    $scope.personDocumentCheck.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentCheck.part.documentPublisher;
    $scope.choosePublisher = function () {
      return $state.go('root.persons.view.checks.edit.choosePublisher');
    };

    $scope.save = function () {
      $scope.personDocumentCheckForm.$validate()
      .then(function () {
        if ($scope.personDocumentCheckForm.$valid) {
          return PersonDocumentCheck
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentCheck)
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

  DocumentChecksEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentCheck',
    'personDocumentCheck',
    'selectedPublisher'
  ];

  DocumentChecksEditCtrl.$resolve = {
    personDocumentCheck: [
      '$stateParams',
      'PersonDocumentCheck',
      function ($stateParams, PersonDocumentCheck) {
        return PersonDocumentCheck.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentChecksEditCtrl', DocumentChecksEditCtrl);
}(angular));