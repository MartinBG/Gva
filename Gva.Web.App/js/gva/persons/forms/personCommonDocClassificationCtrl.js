/*global angular*/
(function (angular) {
  'use strict';

  function PersonCommonDocClassificationCtrl($scope, scFormParams) {
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.categoryAlias = scFormParams.categoryAlias;
  }

  PersonCommonDocClassificationCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonCommonDocClassificationCtrl', PersonCommonDocClassificationCtrl);
}(angular));
