/*global angular*/
(function (angular) {
  'use strict';

  function RootCtrl($scope) {
    $scope.alerts = [];
    $scope.$on('exceptionHandlerError', function (event, error) {
      try {
        $scope.alerts.push(error);
      } catch (e) {
        //swallow all exception so that we don't end up in an infinite loop
      }
    });
  }

  RootCtrl.$inject = ['$scope' ];

  angular.module('common').controller('RootCtrl', RootCtrl);
}(angular));
