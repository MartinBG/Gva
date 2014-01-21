/*global angular*/
(function (angular) {
  'use strict';

  function DocsAddressingCtrl(
    $scope,
    $stateParams
  ) {
    $scope.test = $stateParams.docId;
  }

  DocsAddressingCtrl.$inject = [
    '$scope',
    '$stateParams'
  ];

  angular.module('ems').controller('DocsAddressingCtrl', DocsAddressingCtrl);
}(angular));
