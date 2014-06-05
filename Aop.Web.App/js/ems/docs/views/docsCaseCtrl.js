/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsCaseCtrl(
    $scope,
    $state,
    $sce,
    Aop,
    doc
  ) {
    $scope.aopAppId = doc.aopAppId;
    $scope.docId = doc.docId;

    $scope.isCase = doc.isCase;

    $scope.aopApplicationId = undefined;

    Aop.findAopApp({
      id: $scope.docId
    }).$promise.then(function (result) {
      $scope.aopApplicationId = result.aopApplicationId;
    });

    $scope.docRelations = _.map(_.cloneDeep(doc.docRelations), function (docRelation) {
      docRelation.docDataHtml = $sce.trustAsHtml(docRelation.docDataHtml);
      docRelation.docDescriptionHtml = $sce.trustAsHtml(docRelation.docDescriptionHtml);

      return docRelation;
    });

    $scope.viewDoc = function (docId) {
      return $state.go('root.docs.edit.view', { id: docId });
    };

    $scope.gotoAopApp = function () {
      return $state.go('root.apps.edit', { id: $scope.aopApplicationId });
    };
  }

  DocsCaseCtrl.$inject = [
    '$scope',
    '$state',
    '$sce',
    'Aop',
    'doc'
  ];

  angular.module('ems').controller('DocsCaseCtrl', DocsCaseCtrl);
}(angular, _));
