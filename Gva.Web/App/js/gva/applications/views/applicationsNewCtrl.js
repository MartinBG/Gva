/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsNewCtrl(
    $q,
    $scope,
    $state,
    Application,
    application,
    selectedPerson
    ) {

    if (selectedPerson.length > 0) {
      application.person = {
        id: selectedPerson.pop()
      };
    }

    $scope.application = application;

    $scope.newPerson = function () {
      return $state.go('root.applications.new.personNew');
    };

    $scope.selectPerson = function () {
      return $state.go('root.applications.new.personSelect');
    };

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.save = function () {
      $scope.appForm.$validate()
      .then(function () {
        if ($scope.appForm.$valid) {

          var newApplication = {
            lotId: $scope.application.person.id,
            doc: {
              docFormatTypeId: 3,
              docFormatTypeName: 'Хартиен',
              docCasePartTypeId: 1,
              docCasePartTypeName: 'Публичен',
              docDirectionId: 1,
              docDirectionName: 'Входящ',
              docTypeGroupId: $scope.application.docTypeGroup.nomValueId,
              docTypeGroupName: $scope.application.docTypeGroup.name,
              docTypeId: $scope.application.docType.nomValueId,
              docTypeName: $scope.application.docType.name,
              docSubject: $scope.application.docSubject
            }
          };

          Application.createNew(newApplication).$promise.then(function (result) {
            return $state.go('root.applications.edit.case', { id: result.applicationId });
          });
        }
      });
    };
  }

  ApplicationsNewCtrl.$inject = [
    '$q',
    '$scope',
    '$state',
    'Application',
    'application',
    'selectedPerson'
  ];

  ApplicationsNewCtrl.$resolve = {
    application: function () {
      return {};
    },
    selectedPerson: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsNewCtrl', ApplicationsNewCtrl);
}(angular, _));
