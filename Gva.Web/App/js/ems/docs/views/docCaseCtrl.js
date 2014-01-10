/*global angular*/
(function (angular) {
  'use strict';

  function DocCaseCtrl(
    $scope,
    $state
  ) {

    $scope.viewDoc = function (docId) {
      return $state.go('docs/edit/addressing', { docId: docId });
    };

  }

  DocCaseCtrl.$inject = [
    '$scope',
    '$state'
  ];

  angular.module('ems').controller('DocCaseCtrl', DocCaseCtrl);
}(angular));
