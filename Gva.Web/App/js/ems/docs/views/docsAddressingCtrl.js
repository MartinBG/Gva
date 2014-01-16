/*global angular*/
(function (angular) {
  'use strict';

  function DocsAddressingCtrl(
    $scope
  ) {
    $scope.t = undefined;

  }

  DocsAddressingCtrl.$inject = [
    '$scope'
  ];

  angular.module('ems').controller('DocsAddressingCtrl', DocsAddressingCtrl);
}(angular));
