/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditNewFileCtrl(
    $scope,
    $state,
    $stateParams
    ) {
    $scope.docPartType = null;

    $scope.cancel = function () {
      return $state.transitionTo('root.applications.edit.case', $stateParams, { reload: true });
    };

    $scope.addPart = function () {
      return $scope.addDocPartType.$validate()
        .then(function () {
          if ($scope.addDocPartType.$valid) {
            return $state.go('root.applications.edit.case.addPart', {
              docId: $stateParams.docId,
              docFileId: $stateParams.docFileId,
              setPartAlias: $scope.docPartType.alias
            });
          }
        });
    };
  }

  ApplicationsEditNewFileCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams'
  ];

  angular.module('gva').controller('ApplicationsEditNewFileCtrl', ApplicationsEditNewFileCtrl);
}(angular));
