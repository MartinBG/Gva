/*global angular, _*/
(function (angular) {
  'use strict';

  function ApplicationsLinkCtrl(
    $q,
    $scope,
    $state,
    Application,
    appModel,
    selectedPerson,
    selectedDoc
    ) {

    if (selectedPerson.length > 0) {
      appModel.person = {
        id: selectedPerson.pop()
      };
    }

    if (selectedDoc.length > 0) {
      appModel.doc = selectedDoc.pop();
    }

    $scope.appModel = appModel;

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
      return $state.go('root.applications.search');
    };

    $scope.clear = function () {
      $scope.appModel.doc = null;
    };

    $scope.link = function () {
      $scope.appForm.$validate()
      .then(function () {
        if ($scope.appForm.$valid) {

          var newApplication = {
            lotId: $scope.appModel.person.id,
            docId: $scope.appModel.doc.docId
          };

          Application.linkNew(newApplication).$promise.then(function (result) {
            return $state.go('root.applications.edit.case', { id: result.gvaApplicationId });
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
    'appModel',
    'selectedPerson',
    'selectedDoc'
  ];

  ApplicationsLinkCtrl.$resolve = {
    appModel: function () {
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
