/*global angular*/
(function (angular) {
  'use strict';

  function ScButtonCtrl($scope, $q) {

    $scope.promise = null;
    $scope.deferred = null;
    $scope.btnClicks = 0;

    $scope.btnClick = function () {
      $scope.btnClicks++;
    };

    $scope.create = function () {
      $scope.deferred = $q.defer();

      $scope.promise = $scope.deferred.promise;
      return $scope.promise;
    };

    $scope.resolve = function () {
      $scope.deferred.resolve('Hurray, resolved :)');
      $scope.promise = null;
      $scope.deferred = null;
    };

  }

  ScButtonCtrl.$inject = ['$scope', '$q'];

  angular.module('scaffolding').controller('ScButtonTestbedCtrl', ScButtonCtrl);
}(angular));