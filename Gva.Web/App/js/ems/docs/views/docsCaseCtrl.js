/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsCaseCtrl(
    $scope,
    $state,
    $sce,
    Application,
    application,
    doc
  ) {
    $scope.appId = application.id;
    $scope.docId = doc.docId;
    $scope.docRelations = _.map(_.cloneDeep(doc.docRelations), function (docRelation) {
      docRelation.docDataHtml = $sce.trustAsHtml(docRelation.docDataHtml);
      docRelation.docDescriptionHtml = $sce.trustAsHtml(docRelation.docDescriptionHtml);

      return docRelation;
    });

    $scope.viewDoc = function (docId) {
      return $state.go('root.docs.edit.view', { id: docId });
    };

    $scope.viewApplication = function (applicationId) {
      return $state.go('root.applications.edit.case', { id: applicationId });
    };
  }

  DocsCaseCtrl.$inject = [
    '$scope',
    '$state',
    '$sce',
    'Application',
    'application',
    'doc'
  ];

  DocsCaseCtrl.$resolve = {
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
