/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentChecksEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentChecks,
    personDocumentCheck,
    selectedPublisher
  ) {
    var originalCheck = _.cloneDeep(personDocumentCheck);
    $scope.editMode = null;
    $scope.backFromChild = false;

    $scope.personDocumentCheck = personDocumentCheck;
    $scope.personDocumentCheck.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentCheck.part.documentPublisher;

    if ($state.previous && $state.previous.includes[$state.current.name]) {
      $scope.backFromChild = true;
    }

    $scope.choosePublisher = function () {
      return $state.go('root.persons.view.checks.edit.choosePublisher');
    };

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.personDocumentCheck = _.cloneDeep(originalCheck);
    };

    $scope.save = function () {
      return $scope.editDocumentCheckForm.$validate()
        .then(function () {
          if ($scope.editDocumentCheckForm.$valid) {
            return PersonDocumentChecks
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentCheck)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.checks.search');
              });
          }
        });
    };

    $scope.deleteCheck = function () {
      return PersonDocumentChecks.remove({
        id: $stateParams.id,
        ind: personDocumentCheck.partIndex
      }).$promise.then(function () {
        return $state.go('root.persons.view.checks.search');
      });
    };
  }

  DocumentChecksEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentChecks',
    'personDocumentCheck',
    'selectedPublisher'
  ];

  DocumentChecksEditCtrl.$resolve = {
    personDocumentCheck: [
      '$stateParams',
      'PersonDocumentChecks',
      function ($stateParams, PersonDocumentChecks) {
        return PersonDocumentChecks.get($stateParams).$promise;
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentChecksEditCtrl', DocumentChecksEditCtrl);
}(angular));