/*global angular*/
(function (angular) {
  'use strict';

  function SModeCodeDataCtrl(
    $scope,
    $state,
    SModeCodes,
    scFormParams,
    scModal
  ) {
    $scope.isNew = scFormParams.isNew;
    if ($scope.isNew) {
      $scope.$watch('model.type', function () {
        if ($scope.model && $scope.model.type) {
          SModeCodes.getNextCode({typeId: $scope.model.type.nomValueId})
          .$promise
          .then (function (result) {
            $scope.model.codeHex = result.code;
          });
        }
      });
    }

    $scope.connectToAircraftSModeCode = function () {
      var modalInstance = scModal.open('chooseAircraft');

      modalInstance.result.then(function (selectedAircraftId) {
        $scope.model.aircraftId = selectedAircraftId;
      });

      return modalInstance.opened;
    };

    $scope.viewAircraft = function () {
      return $state.go('root.aircrafts.view', {id: $scope.model.aircraftId});
    };
  }

  SModeCodeDataCtrl.$inject = [
    '$scope',
    '$state',
    'SModeCodes',
    'scFormParams',
    'scModal'
  ];

  angular.module('gva').controller('SModeCodeDataCtrl', SModeCodeDataCtrl);
}(angular));
