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

    $scope.onUnitSelected = null;
    $scope.unitAddFrom = function () {
      $scope.onUnitSelected = function (unit) {
        $scope.doc.docUnitsFrom.push(unit);
        $scope.onUnitSelected = null;
      };
      $scope.chooseUnit();
    };
    $scope.unitAddTo = function () {
      $scope.onUnitSelected = function (unit) {
        $scope.doc.docUnitsTo.push(unit);
        $scope.onUnitSelected = null;
      };
      $scope.chooseUnit();
    };

    $scope.chooseCorr = function () {
      return $state.go('docs/edit/chooseCorr');
    };

    $scope.chooseUnit = function () {
      return $state.go('docs/edit/chooseUnit');
    };

    $scope.save = function () {
      if ($scope.editDocForm.$valid) {
        return Doc
          .save($stateParams, $scope.doc).$promise
          .then(function () {
            return $state.transitionTo($state.current, $stateParams, { reload: true });
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
