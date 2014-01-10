/*global angular*/
(function (angular) {
  'use strict';

  function DocClassificationsCtrl(
    $scope
  ) {

    $scope.removeDocClassification = function () {
    };

    $scope.addDocClassification = function () {
    };

  }

  DocClassificationsCtrl.$inject = [
    '$scope'
  ];

  angular.module('ems').controller('DocClassificationsCtrl', DocClassificationsCtrl);
}(angular));
