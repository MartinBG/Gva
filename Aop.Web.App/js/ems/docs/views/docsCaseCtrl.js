/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsCaseCtrl(
    $scope,
    $state,
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

    $scope.docRelations = _.cloneDeep(doc.docRelations);

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
    'Aop',
    'doc'
  ];

  angular.module('ems').controller('DocsCaseCtrl', DocsCaseCtrl);
}(angular, _));
