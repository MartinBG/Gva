/*global angular*/
(function (angular) {
  'use strict';

  function PersonDocIdCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.hideCaseType = scFormParams.hideCaseType;
  }

  PersonDocIdCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonDocIdCtrl', PersonDocIdCtrl);
}(angular));
