/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsCaseCtrl(
    $scope,
    $state,
    $sce,
    doc
  ) {
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
    'doc'
  ];

  angular.module('ems').controller('DocsCaseCtrl', DocsCaseCtrl);
}(angular, _));
