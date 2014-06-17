/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsCaseCtrl(
    $scope,
    $state,
    Application,
    application,
    doc
  ) {
    $scope.doc = doc;
    $scope.appId = application.id;
    $scope.docId = doc.docId;
    $scope.isCase = doc.isCase;
    $scope.docRelations = _.cloneDeep(doc.docRelations);

    $scope.viewDoc = function (docId) {
      return $state.go('root.docs.edit.view', { id: docId });
    };

    $scope.viewApplication = function (applicationId) {
      return $state.go('root.applications.edit.case', { id: applicationId });
    };

    $scope.linkApplication = function () {
      return $state.go('root.docs.edit.case.linkApp');
    };
  }

  DocsCaseCtrl.$inject = [
    '$scope',
    '$state',
    'Application',
    'application',
    'doc'
  ];

  DocsCaseCtrl.$resolve = {
    doc: [
      'doc',
      function (doc) {
        return doc;
      }
    ],
    application: [
      '$stateParams',
      'Application',
      function resolveApplication($stateParams, Application) {
        return Application.getApplicationByDocId({ docId: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('ems').controller('DocsCaseCtrl', DocsCaseCtrl);
}(angular, _));
