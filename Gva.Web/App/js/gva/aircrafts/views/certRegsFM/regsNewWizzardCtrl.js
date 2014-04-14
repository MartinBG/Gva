﻿/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMNewWizzardCtrl(
    $scope,
    $state,
    Aircraft
  ) {
    $scope.steps = {
      chooseRegister: {},
      chooseRegNumber: {},
      chooseRegMark: {},
      confirmRegMark: {},
      regMarkInUse: {}
    };

    $scope.currentStep = $scope.steps.chooseRegister;

    $scope.model = {};


    $scope.forward = function () {
      return $scope.newRegForm.$validate()
        .then(function () {
          if ($scope.newRegForm.$valid) {
            $scope.newRegForm.$validated = false;


            switch ($scope.currentStep) {
              case $scope.steps.chooseRegister:
                return Aircraft.getNextCertNumber()
                  .$promise.then(function (result) {
                    $scope.model.certNumber = result.certNumber;
                    $scope.currentStep = $scope.steps.chooseRegNumber;
                  });
                break;
              case $scope.steps.chooseRegNumber:
                $scope.currentStep = $scope.steps.chooseRegMark;
                break;
              case $scope.steps.chooseRegMark:
                return Aircraft.checkRegMark({ regMark: $scope.model.regMark })
                  .$promise.then(function (result) {
                    if (result.isValid) {
                      $scope.currentStep = $scope.steps.confirmRegMark;
                    }
                    else {
                      $scope.currentStep = $scope.steps.regMarkInUse;
                    }
                  });
                break;
              case $scope.steps.confirmRegMark:
                return $state.go('root.aircrafts.view.regsFM.new', {}, {}, $scope.model);
                break;
            }
          }
        });
    };

    $scope.back = function () {
      switch ($scope.currentStep) {
        case $scope.steps.chooseRegNumber:
          $scope.currentStep = $scope.steps.chooseRegister;
          break;
        case $scope.steps.chooseRegMark:
          $scope.currentStep = $scope.steps.chooseRegNumber;
          break;
        case $scope.steps.confirmRegMark:
          $scope.currentStep = $scope.steps.chooseRegMark;
          break;
        case $scope.steps.regMarkInUse:
          $scope.currentStep = $scope.steps.chooseRegMark;
          break;
      }
    };

    $scope.getNextCertNumber = function () {
      return Aircraft.getNextCertNumber()
        .$promise.then(function (result) {
          $scope.model.certNumber = result.certNumber;
        });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.regsFM.search');
    };
  }

  CertRegsFMNewWizzardCtrl.$inject = ['$scope', '$state', 'Aircraft'];

  angular.module('gva').controller('CertRegsFMNewWizzardCtrl', CertRegsFMNewWizzardCtrl);
}(angular));
