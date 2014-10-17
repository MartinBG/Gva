/*global angular*/
(function (angular) {
  'use strict';

  function PersonCommonDocDetailsCtrl($scope, scFormParams) {
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.categoryAlias = scFormParams.categoryAlias;
  }

  PersonCommonDocDetailsCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonCommonDocDetailsCtrl', PersonCommonDocDetailsCtrl);
}(angular));
