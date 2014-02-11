/*global angular*/
(function (angular) {
  'use strict';

  function DocsCaseCtrl(
    $scope,
    $state
  ) {

    $scope.viewDoc = function (docId) {
      return $state.go('root.docs.edit.addressing', { docId: docId });
    };

    $scope.viewApplication = function (applicationId) {
      return $state.go('root.applications.edit.case', { id: applicationId });
    };
  }

  DocsCaseCtrl.$inject = [
    '$scope',
    '$state'
  ];

  angular.module('ems').controller('DocsCaseCtrl', DocsCaseCtrl);
}(angular));
