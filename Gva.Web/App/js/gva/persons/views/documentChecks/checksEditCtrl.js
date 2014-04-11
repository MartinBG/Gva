/*global angular,_*/
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
    var originalCheck = _.cloneDeep(personDocumentCheck);

    $scope.isEdit = true;
    $scope.editMode = null;

    $scope.personDocumentCheck = personDocumentCheck;
    $scope.personDocumentCheck.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentCheck.part.documentPublisher;
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
            return PersonDocumentCheck
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentCheck)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.checks.search');
              });
          }
        });
    };

    $scope.deleteCheck = function () {
      return PersonDocumentCheck.remove({
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
    'PersonDocumentCheck',
    'personDocumentCheck',
    'selectedPublisher'
  ];

  DocumentChecksEditCtrl.$resolve = {
    personDocumentCheck: [
      '$stateParams',
      'PersonDocumentCheck',
      function ($stateParams, PersonDocumentCheck) {
        return PersonDocumentCheck.get($stateParams).$promise;
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentChecksEditCtrl', DocumentChecksEditCtrl);
}(angular));