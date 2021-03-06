﻿/*global angular*/
(function (angular) {
  'use strict';

  function AppEmployerNewCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    AopEmployers,
    app
  ) {
    $scope.emp = {};

    $scope.save = function save() {
      return $scope.aopEmpForm.$validate().then(function () {
        if ($scope.aopEmpForm.$valid) {
          return AopEmployers.save($scope.emp).$promise.then(function (data) {
            app.aopEmployerId = data.aopEmployerId;
            return $state.go('^', {}, {}, { inEditMode: true });
          });
        }
      });
    };

    $scope.cancel = function cancel() {
      return $state.go('^', {}, {}, { inEditMode: true });
    };
  }

  AppEmployerNewCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'AopEmployers',
    'app'
  ];

  angular.module('aop').controller('AppEmployerNewCtrl', AppEmployerNewCtrl);
}(angular));
