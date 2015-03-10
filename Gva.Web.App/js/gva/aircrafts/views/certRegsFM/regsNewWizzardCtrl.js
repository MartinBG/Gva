/*global angular*/
(function (angular) {
  'use strict';

  function CertRegsFMNewWizzardCtrl(
    $scope,
    $state,
    $stateParams,
    Aircrafts,
    register,
    actNumber,
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
      regMark: $scope.$parent.aircraft.mark,
      actNumber: actNumber,
      certNumber: actNumber
    };

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
                  {}, {},
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
    register: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.get({
          alias: 'registers',
          valueAlias: 'register1'
        }).$promise;
      }
    ],
    actNumber: [
      'Aircrafts',
      function (Aircrafts) {
        return Aircrafts.getNextActNumber({
          registerAlias: 'register1'
        }).$promise.then(function (result) {
          return result.actNumber;
        });
      }
    ]
  };

  CertRegsFMNewWizzardCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Aircrafts',
    'register',
    'actNumber',
    'gvaConstants'
  ];

  angular.module('gva').controller('CertRegsFMNewWizzardCtrl', CertRegsFMNewWizzardCtrl);
}(angular));
