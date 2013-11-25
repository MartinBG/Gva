/*global angular*/
(function (angular) {
  'use strict';

  function ScNomenclatureCtrl($scope) {
    $scope.gender = {nomTypeValueId: 2, code: '', name: 'Жена', nameAlt: 'Female'};
    $scope.change = function () {
      $scope.gender = {nomTypeValueId: 3, code: '', name: 'Неопределен', nameAlt: 'Unknown'};
    };
  }

  ScNomenclatureCtrl.$inject = ['$scope'];

  angular
    .module('directive-tests')
    .controller('directive-tests.ScNomenclatureCtrl', ScNomenclatureCtrl);
}(angular));
