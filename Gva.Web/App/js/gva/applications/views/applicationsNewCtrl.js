/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsNewCtrl($scope) {
    $scope.application = undefined;
    $scope.doc = undefined;
    $scope.person = undefined;
  }

  ApplicationsNewCtrl.$inject = [
    '$scope'
  ];

  angular.module('gva').controller('ApplicationsNewCtrl', ApplicationsNewCtrl);
}(angular
));
