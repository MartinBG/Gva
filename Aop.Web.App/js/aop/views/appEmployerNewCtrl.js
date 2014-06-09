/*global angular*/
(function (angular) {
  'use strict';

  function AppEmployerNewCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    AopEmployer,
    app
  ) {
    $scope.emp = {};

    $scope.save = function save() {
      return $scope.aopEmpForm.$validate().then(function () {
        if ($scope.aopEmpForm.$valid) {
          return AopEmployer.save($scope.emp).$promise.then(function (data) {
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
    'AopEmployer',
    'app'
  ];

  //AppEmployerNewCtrl.$resolve = {
  //  app: [
  //    '$stateParams',
  //    'Aop',
  //    function resolveApp($stateParams, Aop) {
  //      return Aop.get({ id: $stateParams.id }).$promise;
  //    }
  //  ],
  //  selectDoc: [function () {
  //    return [];
  //  }]
  //};

  angular.module('aop').controller('AppEmployerNewCtrl', AppEmployerNewCtrl);
}(angular));
