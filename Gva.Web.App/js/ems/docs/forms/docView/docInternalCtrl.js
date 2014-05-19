/*global angular*/
(function (angular) {
  'use strict';

  function DocInternalCtrl($scope) {
    $scope.requireDocUnitsFrom = function () {
      return $scope.model.docUnitsFrom.length > 0;
    };
  }

  DocInternalCtrl.$inject = ['$scope'];

  angular.module('ems').controller('DocInternalCtrl', DocInternalCtrl);
}(angular));
