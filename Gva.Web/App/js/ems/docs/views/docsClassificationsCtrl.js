/*global angular*/
(function (angular) {
  'use strict';

  function DocsClassificationsCtrl(
    $scope
  ) {

    $scope.removeDocClassification = function () {
    };

    $scope.addDocClassification = function () {
    };

  }

  DocsClassificationsCtrl.$inject = [
    '$scope'
  ];

  angular.module('ems').controller('DocsClassificationsCtrl', DocsClassificationsCtrl);
}(angular));
