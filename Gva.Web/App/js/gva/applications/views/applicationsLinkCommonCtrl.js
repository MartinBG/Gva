/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsLinkCommonCtrl(
    $q,
    $scope,
    $state,
    Application
    ) {

    $scope.newPerson = function () {
      $state.go('applications/link/personNew');
    };

    $scope.choosePerson = function () {
      $state.go('applications/link/personChoose');
    };

    $scope.chooseDoc = function () {
      $state.go('applications/link/docChoose');
    };

    $scope.cancel = function () {
      $state.go('docs/search');
    };

    $scope.clear = function () {
      $scope.$parent.doc = undefined;
    };

    $scope.save = function () {
      $scope.saveClicked = true;

      if ($scope.docForm.$valid) {
        var newApplication = {
          applicationId: null,
          lotId: $scope.$parent.person.nomTypeValueId,
          docId: $scope.$parent.doc.docId
        };

        Application.validateExist(newApplication).$promise.then(function (result) {
          if (result.applicationExist === false) {
            Application.save(newApplication).$promise.then(function () {
              $state.go('docs/search');
            });
          }
        });
      }
    };
  }

  ApplicationsLinkCommonCtrl.$inject = [
    '$q',
    '$scope',
    '$state',
    'Application'
  ];

  angular.module('gva').controller('ApplicationsLinkCommonCtrl', ApplicationsLinkCommonCtrl);
}(angular, _));
