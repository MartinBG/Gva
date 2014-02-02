/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsNewDocCtrl(
    $q,
    $scope,
    $state,
    Application,
    Doc
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
      $scope.saveClicked = true;

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
        Doc.save(newDoc).$promise.then(function (savedDoc) {
          var newApplication = {
            applicationId: null,
            lotId: $scope.$parent.person.nomTypeValueId,
            docId: savedDoc.docId
          };
          Application.save(newApplication).$promise.then(function (application) {
            return $state.go('applications/edit/case', { id: application.applicationId });
          });
        });
      }
    };
  }

  ApplicationsNewDocCtrl.$inject = [
    '$q',
    '$scope',
    '$state',
    'Application',
    'Doc'
  ];

  angular.module('gva').controller('ApplicationsNewDocCtrl', ApplicationsNewDocCtrl);
}(angular, _));
