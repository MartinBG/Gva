﻿/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMNewWizzardCtrl(
    $scope,
    $state,
    $stateParams,
    Aircraft,
    oldReg,
    certNumber
  ) {
    $scope.steps = {
      chooseRegMark: {},
      confirmRegMark: {},
      regMarkInUse: {}
    };

    $scope.currentStep = $scope.steps.chooseRegMark;

    $scope.model = {
      register: {
        nomValueId: 9008224,
        name: 'Регистър 1'
      }
    };
    $scope.oldInd = $stateParams.oldInd;
    $scope.reregMode = !!(oldReg && oldReg.part);
    if ($scope.reregMode) {
      $scope.model.certNumber = oldReg.part.certNumber;
    } else {
      $scope.model.certNumber = certNumber;
    }
    $scope.model.actNumber = certNumber;

    $scope.forward = function () {
      return $scope.newRegForm.$validate()
        .then(function () {
          if ($scope.newRegForm.$valid) {
            $scope.newRegForm.$validated = false;

            switch ($scope.currentStep) {
            case $scope.steps.chooseRegMark:
              return Aircraft.checkRegMark({
                lotId: $stateParams.id,
                regMark: $scope.model.regMark
              }).$promise.then(function (result) {
                if (result.isValid) {
                  $scope.currentStep = $scope.steps.confirmRegMark;
                }
                else {
                  $scope.currentStep = $scope.steps.regMarkInUse;
                }
              });
            case $scope.steps.confirmRegMark:
              return $state.go(
                'root.aircrafts.view.regsFM.new',
                { oldInd: $stateParams.oldInd },
                {},
                $scope.model);
            }
          }
        });
    };

    $scope.back = function () {
      switch ($scope.currentStep) {
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
      return Aircraft.getNextCertNumber({
        registerId: $scope.model.register.nomValueId,
        currentCertNumber: $scope.model.certNumber
      }).$promise.then(function (result) {
        if (!$scope.reregMode) {
          $scope.model.certNumber = result.certNumber;
        }
        $scope.model.actNumber = result.certNumber;
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.regsFM.search');
    };
  }

  CertRegsFMNewWizzardCtrl.$resolve = {
    oldReg: [
      '$stateParams',
      'AircraftCertRegistrationFM',
      function ($stateParams, AircraftCertRegistrationFM) {
        if ($stateParams.oldInd) {
          return AircraftCertRegistrationFM.get({ id: $stateParams.id, ind: $stateParams.oldInd })
            .$promise;
        }
        else {
          return null;
        }
      }
    ],
    certNumber: [
      'Aircraft',
      function (Aircraft) {
        return Aircraft.getNextCertNumber({
          registerId: 9008224,
          currentCertNumber: undefined
        }).$promise
        .then(function (result) {
          return result.certNumber;
        });
      }
    ]
  };

  CertRegsFMNewWizzardCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Aircraft',
    'oldReg',
    'certNumber'
  ];

  angular.module('gva').controller('CertRegsFMNewWizzardCtrl', CertRegsFMNewWizzardCtrl);
}(angular));