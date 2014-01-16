/*global angular*/
(function (angular) {
  'use strict';

  function DocsStagesCtrl(
    $scope
  ) {

    $scope.blabla = '<b>asdf</b>';

  }

  DocsStagesCtrl.$inject = [
    '$scope'
  ];

  angular.module('ems').controller('DocsStagesCtrl', DocsStagesCtrl);
}(angular));
