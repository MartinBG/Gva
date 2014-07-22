﻿/*global angular*/
(function (angular) {
  'use strict';

  function ChooseDocModalCtrl(
    $scope,
    $modalInstance,
    Applications,
    docs
  ) {
    $scope.docs = docs.documents;

    $scope.filters = {
      fromDate: null,
      toDate: null,
      regUri: null,
      docName: null,
      docTypeId: null,
      docStatusId: null,
      corrs: null,
      units: null
    };

    $scope.search = function () {
      return Applications.notLinkedDocs($scope.filters).$promise.then(function (docs) {
        $scope.docs = docs.documents;
      });
    };

    $scope.selectDoc = function (doc) {
      return $modalInstance.close(doc);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ChooseDocModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Applications',
    'docs'
  ];

  angular.module('gva').controller('ChooseDocModalCtrl', ChooseDocModalCtrl);
}(angular));
