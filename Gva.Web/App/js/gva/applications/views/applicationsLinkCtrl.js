/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsLinkCtrl(
    $q,
    $scope,
    $state,
    Application,
    application,
    selectedPerson,
    selectedDoc
    ) {

    if (selectedPerson.length > 0) {
      application.person = {
        id: selectedPerson.pop()
      };
    }

    if (selectedDoc.length > 0) {
      application.doc = selectedDoc.pop();
    }

    $scope.application = application;

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
      $scope.application.doc = null;
    };

    $scope.save = function () {
      $scope.appForm.$validate()
      .then(function () {
        if ($scope.appForm.$valid) {

          var newApplication = {
            lotId: $scope.application.person.id,
            docId: $scope.application.doc.docId
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
    'application',
    'selectedPerson',
    'selectedDoc'
  ];

  ApplicationsLinkCtrl.$resolve = {
    application: function () {
      return {};
    },
    selectedPerson: function () {
      return [];
    },
    selectedDoc: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsLinkCtrl', ApplicationsLinkCtrl);
}(angular, _));
