/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsLinkCtrl(
    $q,
    $scope,
    $state,
    Application,
    person,
    doc
    ) {

    $scope.person = person;
    $scope.doc = doc;

    $scope.newPerson = function () {
      return $state.go('root.applications.link.personNew');
    };

    $scope.selectPerson = function () {
      return $state.go('root.applications.link.personSelect');
    };

    $scope.selectDoc = function () {
      return $state.go('root.applications.link.docSelect', { hasLot: false });
    };

    $scope.cancel = function () {
      return $state.go('root.docs.search');
    };

    $scope.clear = function () {
      $scope.doc.docId = null;
      $scope.doc.regUri = null;
      $scope.doc.docTypeName = null;
      $scope.doc.docStatusName = null;
    };

    $scope.save = function () {
      $scope.appForm.$validate()
      .then(function () {
        if ($scope.appForm.$valid) {

          var newApplication = {
            lotId: $scope.person.id,
            docId: $scope.doc.docId
          };

          Application.linkNew(newApplication).$promise.then(function (result) {
            return $state.go('root.applications.edit.case', { id: result.applicationId });
          });
        }
      });
    };
  }

  ApplicationsLinkCtrl.$inject = [
    '$q',
    '$scope',
    '$state',
    'Application',
    'person',
    'doc'
  ];

  ApplicationsLinkCtrl.$resolve = {
    person: function () {
      return {};
    },
    doc: function () {
      return {};
    }
  };

  angular.module('gva').controller('ApplicationsLinkCtrl', ApplicationsLinkCtrl);
}(angular, _));
