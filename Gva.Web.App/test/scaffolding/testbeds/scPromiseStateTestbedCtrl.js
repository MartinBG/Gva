/*global angular*/
(function (angular) {
  'use strict';

  function ScPromiseStateCtrl($scope, $q) {
    $scope.promise = null;
    $scope.deferred = null;

    $scope.create = function () {
      $scope.deferred = $q.defer();

      $scope.promise = $scope.deferred.promise;
    };

    $scope.resolve = function () {
      $scope.deferred.resolve('Hurray, resolved :)');
    };

    $scope.reject = function () {
      $scope.deferred.reject('Oh no, rejected :(');
    };

    $scope.destroy = function () {
      $scope.promise = null;
      $scope.deferred = null;
    };

    $scope.createResolved = function () {
      $scope.promise = $q.when('Already resolved ;)');
    };
  }

  ScPromiseStateCtrl.$inject = ['$scope', '$q'];

  angular.module('scaffolding').controller('ScPromiseStateTestbedCtrl', ScPromiseStateCtrl);
}(angular));
