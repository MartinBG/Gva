/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseInspectorsModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    inspectors
  ) {
    $scope.inspectors = inspectors;

    $scope.selectedInspectors = [];

    var includedInspectorsNames = _.pluck(scModalParams.includedInspectors, 'name');
    $scope.inspectors = _.filter(inspectors, function (inspector) {
      return !_.contains(includedInspectorsNames, inspector.name);
    });

    $scope.addInspectors = function () {
      return $modalInstance.close($scope.selectedInspectors);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectInspector = function (event, inspector) {
      var index;
      if ($(event.target).is(':checked')) {
        $scope.selectedInspectors.push(inspector);
      } else {
        index = $scope.selectedInspectors.indexOf(inspector);
        $scope.selectedInspectors.splice.apply($scope.selectedInspectors, [index, 1]);
      }
    };
  }

  ChooseInspectorsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'inspectors'
  ];

  ChooseInspectorsModalCtrl.$resolve = {
    inspectors: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.query({
          alias: 'inspectors'
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseInspectorsModalCtrl', ChooseInspectorsModalCtrl);
}(angular, _, $));
