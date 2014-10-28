/*global angular*/
(function (angular) {
  'use strict';
  function CommonInspectorsCtrl($scope, scModal, scFormParams) {
    $scope.addInspectorText = scFormParams.addInspectorText;
    $scope.inspectorText = scFormParams.inspectorText;
    $scope.inspectorsText = scFormParams.inspectorsText;
    $scope.noAvailableInspectorsText = scFormParams.noAvailableInspectorsText;

    $scope.addInspector = function () {
      var modalInstance = scModal.open('chooseInspectors', {
        includedInspectors: $scope.model
      });

      modalInstance.result.then(function (selectedInspectors) {
        $scope.model.splice.apply($scope.model,
          [$scope.model.length, 0].concat(selectedInspectors));
      });

      return modalInstance.opened;
    };

    $scope.deleteInspector = function removeInspector (inspector) {
      var index = $scope.model.indexOf(inspector);
      $scope.model.splice.apply($scope.model, [index, 1]);
    };
  }

  CommonInspectorsCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

  angular.module('gva')
    .controller('CommonInspectorsCtrl', CommonInspectorsCtrl);
}(angular));