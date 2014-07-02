/*global angular*/
(function (angular) {
  'use strict';

  function AddCheckCtrl(
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
      return $state.go('.choosePublisher');
    };

    $scope.save = function () {
      return $scope.newDocumentCheckForm.$validate()
        .then(function () {
          if ($scope.newDocumentCheckForm.$valid) {
            return PersonDocumentChecks
              .save({ id: $stateParams.id }, $scope.personDocumentCheck)
              .$promise
              .then(function (savedCheck) {
                return $state.go('^', {}, {}, {
                  selectedChecks: [savedCheck.partIndex]
                });
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  AddCheckCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentChecks',
    'personDocumentCheck',
    'selectedPublisher'
  ];

  AddCheckCtrl.$resolve = {
    personDocumentCheck: function () {
      return {
        part: {},
        files: []
      };
    },
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('AddCheckCtrl', AddCheckCtrl);
}(angular));
