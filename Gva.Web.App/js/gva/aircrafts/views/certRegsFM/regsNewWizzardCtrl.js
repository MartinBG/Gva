/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMNewWizzardCtrl(
    $scope,
    $state,
    $stateParams,
    Aircraft,
    oldReg,
    actNumber,
    register
  ) {
    $scope.steps = {
      chooseRegMark: {},
      confirmRegMark: {},
      regMarkInUse: {}
    };

    $scope.currentStep = $scope.steps.chooseRegMark;

    $scope.model = {
      register: register,
      regMark: $scope.$parent.aircraft.mark
    };

    $scope.oldInd = $stateParams.oldInd;
    $scope.reregMode = !!(oldReg && oldReg.part);
    if ($scope.reregMode) {
      $scope.model.certNumber = oldReg.part.certNumber;
    } else {
      $scope.model.certNumber = actNumber;
    }
    $scope.model.actNumber = actNumber;

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

    $scope.getNextActNumber = function () {
      return Aircraft.getNextActNumber({
        registerId: $scope.model.register.nomValueId
      }).$promise.then(function (result) {
        if (!$scope.reregMode) {
          $scope.model.certNumber = result.actNumber;
        }
        $scope.model.actNumber = result.actNumber;
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
    actNumber: [
      'Aircraft',
      function (Aircraft) {
        return Aircraft.getNextActNumber({
          registerId: 9008224
        }).$promise
        .then(function (result) {
          return result.actNumber;
        });
      }
    ],
    register: [
      'Nomenclature',
      function (Nomenclature) {
        return Nomenclature.get({
          alias: 'registers',
          valueAlias: 'register1'
        }).$promise;
      }
    ]
  };

  CertRegsFMNewWizzardCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Aircraft',
    'oldReg',
    'actNumber',
    'register'
  ];

  angular.module('gva').controller('CertRegsFMNewWizzardCtrl', CertRegsFMNewWizzardCtrl);
}(angular));
