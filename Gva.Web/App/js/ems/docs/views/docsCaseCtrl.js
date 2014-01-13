/*global angular*/
(function (angular) {
  'use strict';

  function DocsCaseCtrl(
    $scope,
    $state
  ) {

    $scope.viewDoc = function (docId) {
      return $state.go('docs/edit/addressing', { docId: docId });
    };

  }

  DocsCaseCtrl.$inject = [
    '$scope',
    '$state'
  ];

  angular.module('ems').controller('DocsCaseCtrl', DocsCaseCtrl);
}(angular));
