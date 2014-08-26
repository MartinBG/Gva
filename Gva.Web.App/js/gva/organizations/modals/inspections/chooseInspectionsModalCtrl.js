/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChooseInspectionsModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    inspections
  ) {
    $scope.inspections = _.chain(inspections)
      .filter(function (inspection) {
        return inspection.part.inspectionDetails.length > 0;
      })
      .map(function (inspection) {
        if (_.contains(scModalParams.includedInspections, inspection.partIndex)) {
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
    '$modalInstance',
    'scModalParams',
    'inspections'
  ];

  ChooseInspectionsModalCtrl.$resolve = {
    inspections: [
      'OrganizationInspections',
      'scModalParams',
      function (OrganizationInspections, scModalParams) {
        return OrganizationInspections.query({ id: scModalParams.lotId }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseInspectionsModalCtrl', ChooseInspectionsModalCtrl);
}(angular, _));
