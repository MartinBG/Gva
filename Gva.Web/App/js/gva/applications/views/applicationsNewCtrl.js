/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsNewCtrl(
    $q,
    $scope,
    $state,
    Application,
    appModel,
    selectedPerson
    ) {

    if (selectedPerson.length > 0) {
      appModel.person = {
        id: selectedPerson.pop()
      };
    }

    $scope.appModel = appModel;

    $scope.newPerson = function () {
      return $state.go('root.applications.new.personNew');
    };

    $scope.selectPerson = function () {
      return $state.go('root.applications.new.personSelect');
    };

    $scope.cancel = function () {
      return $state.go('root.applications.search');
    };

    $scope.save = function () {
      $scope.appForm.$validate()
      .then(function () {
        if ($scope.appForm.$valid) {

          var newApplication = {
            lotId: $scope.appModel.person.id,
            doc: {
              docFormatTypeId: 3,
              docFormatTypeName: 'Хартиен',
              docCasePartTypeId: 1,
              docCasePartTypeName: 'Публичен',
              docDirectionId: 1,
              docDirectionName: 'Входящ',
              docTypeGroupId: $scope.appModel.docTypeGroup.nomValueId,
              docTypeGroupName: $scope.appModel.docTypeGroup.name,
              docTypeId: $scope.appModel.docType.nomValueId,
              docTypeName: $scope.appModel.docType.name,
              docSubject: $scope.appModel.docSubject
            },
            appPart: $scope.appModel.appPart,
            appFile: $scope.appModel.appFile
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
    'appModel',
    'selectedPerson'
  ];

  ApplicationsNewCtrl.$resolve = {
    appModel: function () {
      return {};
    },
    selectedPerson: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsNewCtrl', ApplicationsNewCtrl);
}(angular, _));
