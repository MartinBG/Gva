/*global angular*/
(function (angular) {
  'use strict';

  function ScNomenclatureCtrl($scope) {
    $scope.multipleObjVal = [{
      nomValueId: 1,
      code: 'M',
      name: 'Мъж',
      nameAlt: 'Male',
      alias: 'male'
    }];
    $scope.multipleIdVal = [6];
    $scope.parentVal = 58;
    $scope.childVal = 3731;
    $scope.gender = { nomValueId: 2, code: '', name: 'Жена', nameAlt: 'Female' };
    $scope.change = function () {
      $scope.gender = { nomValueId: 3, code: '', name: 'Неопределен', nameAlt: 'Unknown' };
    };
  }

  ScNomenclatureCtrl.$inject = ['$scope'];

  angular.module('scaffolding').controller('ScNomenclatureTestbedCtrl', ScNomenclatureCtrl);
}(angular));
