/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMNewWizzardCtrl(
    $scope,
    $state,
    $stateParams,
    Aircrafts,
    oldReg,
    register,
    gvaConstants
  ) {
    $scope.steps = {
      chooseRegMark: {},
      confirmRegMark: {},
      regMarkInUse: {}
    };
    $scope.regMarkPattern = gvaConstants.regMarkPattern;
    $scope.currentStep = $scope.steps.chooseRegMark;

    $scope.model = {
      register: register,
      regMark: $scope.$parent.aircraft.mark
    };

    $scope.getNextActNumber = function () {
      return Aircrafts.getNextActNumber({
        registerId: $scope.model.register.nomValueId
      }).$promise.then(function (result) {
        if (!$scope.reregMode) {
          $scope.model.certNumber = result.actNumber;
        }
        $scope.model.actNumber = result.actNumber;
      });
    };

    var nextActNumber = $scope.getNextActNumber();
    $scope.oldInd = $stateParams.oldInd;
    $scope.reregMode = !!(oldReg && oldReg.part);
    if ($scope.reregMode) {
      $scope.model.certNumber = oldReg.part.certNumber;
    } else {
      $scope.model.certNumber = nextActNumber;
    }
    $scope.model.actNumber = nextActNumber;

    $scope.forward = function () {
      return $scope.newRegForm.$validate()
        .then(function () {
          if ($scope.newRegForm.$valid) {
            $scope.newRegForm.$validated = false;

            switch ($scope.currentStep) {
              case $scope.steps.chooseRegMark:
                return Aircrafts.checkRegMark({
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

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.regsFM.search');
    };
  }

  CertRegsFMNewWizzardCtrl.$resolve = {
    oldReg: [
      '$stateParams',
      'AircraftCertRegistrationsFM',
      function ($stateParams, AircraftCertRegistrationsFM) {
        if ($stateParams.oldInd) {
          return AircraftCertRegistrationsFM.get({ id: $stateParams.id, ind: $stateParams.oldInd })
            .$promise;
        }
        else {
          return null;
        }
      }
    ],
    register: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.get({
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
    'Aircrafts',
    'oldReg',
    'register',
    'gvaConstants'
  ];

  angular.module('gva').controller('CertRegsFMNewWizzardCtrl', CertRegsFMNewWizzardCtrl);
}(angular));
