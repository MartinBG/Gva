/*global angular*/
(function (angular) {
  'use strict';

  function DocsEditCtrl(
    $q,
    $scope,
    $filter,
    $state,
    $stateParams,
    Doc
  ) {
    $scope.doc = Doc.get({ docId: $stateParams.docId });

    //Doc.get({ docId: $stateParams.docId }).$promise.then(function (doc) {
    //  $scope.doc = doc;
    //});

    $scope.inEditMode = false;

    $scope.markAsRead = function () {
      $scope.doc.isRead = true;
    };

    $scope.markAsUnread = function () {
      //todo call to backend and set DocUser.HasRead flags
      $scope.doc.isRead = false;
    };

    $scope.enterEditMode = function () {
      $scope.inEditMode = true;
    };

    $scope.exitEditMode = function () {
      $scope.inEditMode = false;
    };

    $scope.save = function () {
      if ($scope.editDocForm.$valid) {
        return Doc
          .save($stateParams, $scope.doc).$promise
          .then(function () {
            //todo refresh state ! $state.go('docs/edit', { docId: $stateParams.docId });
            $scope.inEditMode = false;
            return true;
          });
      }
    };

  }

  DocsEditCtrl.$inject = [
    '$q',
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Doc'
  ];

  angular.module('ems').controller('DocsEditCtrl', DocsEditCtrl);
}(angular));
