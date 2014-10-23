/*global angular*/
(function (angular) {
  'use strict';

  function AircraftDocumentDebtFMCtrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;
  }

  AircraftDocumentDebtFMCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('AircraftDocumentDebtFMCtrl', AircraftDocumentDebtFMCtrl);
}(angular));
