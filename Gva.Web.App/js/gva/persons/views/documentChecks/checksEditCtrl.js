/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentChecksEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentChecks,
    personDocumentCheck,
    reports,
    scMessage
  ) {
    var originalCheck = _.cloneDeep(personDocumentCheck);

    $scope.editMode = null;
    $scope.personDocumentCheck = personDocumentCheck;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.reports = reports;

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
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return PersonDocumentChecks.remove({
            id: $stateParams.id,
            ind: personDocumentCheck.partIndex
          }).$promise.then(function () {
            return $state.go('root.persons.view.checks.search');
          });
        }
      });
    };
  }

  DocumentChecksEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentChecks',
    'personDocumentCheck',
    'reports',
    'scMessage'
  ];

  DocumentChecksEditCtrl.$resolve = {
    personDocumentCheck: [
      '$stateParams',
      'PersonDocumentChecks',
      function ($stateParams, PersonDocumentChecks) {
        return PersonDocumentChecks.get($stateParams).$promise;
      }
    ],
    reports: [
      '$stateParams',
      'PersonDocumentChecks',
      function ($stateParams, PersonDocumentChecks) {
        return  PersonDocumentChecks
          .getReports($stateParams)
          .$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentChecksEditCtrl', DocumentChecksEditCtrl);
}(angular));
