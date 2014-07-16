﻿/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChooseInspectionsModalCtrl(
    $scope,
    $stateParams,
    $modalInstance,
    inspections,
    includedInspections
  ) {
    $scope.inspections = _.chain(inspections)
      .filter(function (inspection) {
        return inspection.part.auditDetails.length !== 0;
      })
      .map(function (inspection) {
        if (_.contains(includedInspections, inspection.partIndex)) {
          inspection.checked = true;
        }

        return inspection;
      }).value();

    $scope.addInspections = function () {
      var selectedInspections = _.filter($scope.inspections, { 'checked': true });

      return $modalInstance.close(selectedInspections);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ChooseInspectionsModalCtrl.$inject = [
    '$scope',
    '$stateParams',
    '$modalInstance',
    'inspections',
    'includedInspections'
  ];

  ChooseInspectionsModalCtrl.$resolve = {
    inspections: [
      '$stateParams',
      'OrganizationInspections',
      function ($stateParams, OrganizationInspections) {
        return OrganizationInspections.query({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseInspectionsModalCtrl', ChooseInspectionsModalCtrl);
}(angular, _));
