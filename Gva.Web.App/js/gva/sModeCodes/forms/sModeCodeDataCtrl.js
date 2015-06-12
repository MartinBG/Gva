/*global angular*/
(function (angular) {
  'use strict';

  function convert(num) {
    return {
      from: function (baseFrom) {
        return {
          to: function (baseTo) {
              return parseInt(num, baseFrom).toString(baseTo);
          }
        };
      }
    };
  }

  function SModeCodeDataCtrl(
    $scope,
    $state,
    SModeCodes,
    scFormParams
  ) {
    $scope.isNew = scFormParams.isNew;

    $scope.aircraftRegistration = scFormParams.aircraftRegistration ? 
      scFormParams.aircraftRegistration : null;

    var updateCodes = function () {
      $scope.decimal = convert($scope.model.codeHex).from(16).to(10);
      $scope.octal = convert($scope.model.codeHex).from(16).to(8);
      $scope.binary = convert($scope.model.codeHex).from(16).to(2);
    };

    if ($scope.isNew) {
      $scope.$watch('model.type', function () {
        if ($scope.model && $scope.model.type) {
          SModeCodes.getNextCode({typeId: $scope.model.type.nomValueId})
          .$promise
          .then (function (result) {
            $scope.model.codeHex = result.code;
            updateCodes();
          });
        }
      });
    } else {
      updateCodes();
    }

    $scope.viewAircraft = function () {
      return $state.go('root.aircrafts.view.edit', {
        id: $scope.model.aircraftId
      });
    };
  }

  SModeCodeDataCtrl.$inject = [
    '$scope',
    '$state',
    'SModeCodes',
    'scFormParams'
  ];

  angular.module('gva').controller('SModeCodeDataCtrl', SModeCodeDataCtrl);
}(angular));
