/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsNewDocCtrl(
    $q,
    $scope,
    $state,
    Application
    ) {
    $scope.newPerson = function () {
      $state.go('applications/new/personNew');
    };

    $scope.choosePerson = function () {
      $state.go('applications/new/personChoose');
    };

    $scope.cancel = function () {
      $state.go('docs/search');
    };

    $scope.save = function () {
      $scope.docForm.$validate()
      .then(function () {
        if ($scope.docForm.$valid) {
          var newDoc = {
            docFormatTypeId: 3,
            docFormatTypeName: 'Хартиен',
            docCasePartTypeId: 1,
            docCasePartTypeName: 'Публичен',
            docDirectionId: 1,
            docDirectionName: 'Входящ',
            docTypeGroupId: $scope.$parent.docTypeGroup.nomTypeValueId,
            docTypeGroupName: $scope.$parent.docTypeGroup.name,
            docTypeId: $scope.$parent.docType.nomTypeValueId,
            docTypeName: $scope.$parent.docType.name,
            docSubject: $scope.$parent.docSubject
          };

          var newApplication = {
            applicationId: null,
            lotId: $scope.$parent.person.nomTypeValueId,
            doc: newDoc
          };

          Application.createNew(newApplication).$promise.then(function (result) {
            return $state.go('applications/edit/case', { id: result.applicationId });
          });
        }
      });
    };
  }

  ApplicationsNewDocCtrl.$inject = [
    '$q',
    '$scope',
    '$state',
    'Application'
  ];

  angular.module('gva').controller('ApplicationsNewDocCtrl', ApplicationsNewDocCtrl);
}(angular, _));
