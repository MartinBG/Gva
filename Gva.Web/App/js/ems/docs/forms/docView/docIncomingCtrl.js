/*global angular*/
(function (angular) {
  'use strict';

  function DocIncomingCtrl($scope) {
    $scope.requireCorrs = function () {
      return $scope.model.docCorrespondents.length > 0;
    };
  }

  DocIncomingCtrl.$inject = ['$scope'];

  angular.module('ems').controller('DocIncomingCtrl', DocIncomingCtrl);
}(angular));
