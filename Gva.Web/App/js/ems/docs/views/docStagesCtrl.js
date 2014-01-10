/*global angular*/
(function (angular) {
  'use strict';

  function DocStagesCtrl(
    $scope
  ) {

    $scope.blabla = '<b>asdf</b>';

  }

  DocStagesCtrl.$inject = [
    '$scope'
  ];

  angular.module('ems').controller('DocStagesCtrl', DocStagesCtrl);
}(angular));
