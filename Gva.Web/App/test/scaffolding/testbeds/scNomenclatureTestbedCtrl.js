/*global angular*/
(function (angular) {
  'use strict';

  function ScNomenclatureCtrl($scope) {
    $scope.parentVal = 58;
    $scope.childVal = 3731;
    $scope.gender = {nomTypeValueId: 2, code: '', name: 'Жена', nameAlt: 'Female'};
    $scope.change = function () {
      $scope.gender = {nomTypeValueId: 3, code: '', name: 'Неопределен', nameAlt: 'Unknown'};
    };
  }

  ScNomenclatureCtrl.$inject = ['$scope'];

  angular.module('scaffolding').controller('ScNomenclatureTestbedCtrl', ScNomenclatureCtrl);
}(angular));
