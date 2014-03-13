/*global angular*/
(function (angular) {
  'use strict';

  function DocClassificationCtrl(
    $scope
  ) {

    $scope.removeDocClassification = function removeDocClassification() {
    };

    $scope.addDocClassification = function addDocClassification() {
    };
  }

  DocClassificationCtrl.$inject = [
    '$scope'
  ];

  angular.module('ems').controller('DocClassificationCtrl', DocClassificationCtrl);
}(angular));
