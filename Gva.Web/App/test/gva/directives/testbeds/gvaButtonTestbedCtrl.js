/*global angular*/
(function (angular) {
  'use strict';

  function GvaButtonCtrl($scope, $q) {

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

  GvaButtonCtrl.$inject = ['$scope', '$q'];

  angular.module('gva').controller('GvaButtonTestbedCtrl', GvaButtonCtrl);
}(angular));