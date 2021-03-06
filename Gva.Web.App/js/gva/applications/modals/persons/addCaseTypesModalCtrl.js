﻿/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function AddCaseTypesModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    PersonsInfo,
    personInfo,
    allCaseTypes
  ) {
    $scope.selectedCaseTypes = [];
    $scope.personInfo = personInfo;

    var addedCaseTypeNomValueIds = $scope.personInfo.personData.caseTypes;
    $scope.caseTypes = _.filter(allCaseTypes, function (caseType) {
      return !_.contains(addedCaseTypeNomValueIds, caseType.nomValueId);
    });

    $scope.selectCaseType = function (event, caseType) {
      if ($(event.target).is(':checked')) {
        $scope.selectedCaseTypes.push(caseType.nomValueId);
      }
      else {
        $scope.selectedCaseTypes = _.without($scope.selectedCaseTypes, caseType.nomValueId);
      }
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.saveCaseTypes = function () {
      $scope.personInfo.personData.caseTypes =
        $scope.personInfo.personData.caseTypes.concat($scope.selectedCaseTypes);

      return PersonsInfo
        .save({ id: scModalParams.lotId }, $scope.personInfo)
        .$promise
        .then(function () {
          return $modalInstance.close();
        });
    };
  }

  AddCaseTypesModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'PersonsInfo',
    'personInfo',
    'allCaseTypes'
  ];

  AddCaseTypesModalCtrl.$resolve = {
    allCaseTypes: [
      '$q',
      'Nomenclatures',
      'scModalParams',
      function ($q, Nomenclatures) {
        return Nomenclatures.query({
          alias: 'personCaseTypes'
        }).$promise
        .then(function (caseTypes) {
          return _.filter(caseTypes, function (caseType) {
            return caseType.alias !== 'staffExaminer' && caseType.alias !== 'inspector';
          });
        });
      }
    ],
    personInfo: [
      'PersonsInfo', 'scModalParams',
      function ( PersonsInfo, scModalParams) {
        return PersonsInfo.get({ id: scModalParams.lotId }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AddCaseTypesModalCtrl', AddCaseTypesModalCtrl);
}(angular, _, $));
