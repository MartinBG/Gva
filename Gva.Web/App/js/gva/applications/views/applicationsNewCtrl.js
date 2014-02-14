/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsNewCtrl(
    $q,
    $scope,
    $state,
    Application,
    person
    ) {
    $scope.person = person;

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
            lotId: $scope.person.id,
            doc: {
              docFormatTypeId: 3,
              docFormatTypeName: 'Хартиен',
              docCasePartTypeId: 1,
              docCasePartTypeName: 'Публичен',
              docDirectionId: 1,
              docDirectionName: 'Входящ',
              docTypeGroupId: $scope.docTypeGroup.nomTypeValueId,
              docTypeGroupName: $scope.docTypeGroup.name,
              docTypeId: $scope.docType.nomTypeValueId,
              docTypeName: $scope.docType.name,
              docSubject: $scope.docSubject
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
    'person'
  ];

  ApplicationsNewCtrl.$resolve = {
    person: function () {
      return {};
    }
  };

  angular.module('gva').controller('ApplicationsNewCtrl', ApplicationsNewCtrl);
}(angular, _));
