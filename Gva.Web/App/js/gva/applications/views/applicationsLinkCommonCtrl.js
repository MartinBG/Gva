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
      $state.go('applications/link/docChoose', { hasLot: false });
    };

    $scope.cancel = function () {
      $state.go('docs/search');
    };

    $scope.clear = function () {
      $scope.$parent.doc = undefined;
    };

    $scope.save = function () {
      $scope.docForm.$validate()
      .then(function () {
        if ($scope.docForm.$valid) {
          var newApplication = {
            applicationId: null,
            lotId: $scope.$parent.person.nomTypeValueId,
            doc: {
              docId: $scope.$parent.doc.docId
            }
          };

          Application.createNew(newApplication).$promise.then(function (result) {
            return $state.go('applications/edit/case', { id: result.applicationId });
          });
        }
      });
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
