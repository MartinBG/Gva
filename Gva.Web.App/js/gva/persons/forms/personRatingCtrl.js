/*global angular*/
(function (angular) {
  'use strict';

  function PersonRatingCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
  }

  PersonRatingCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonRatingCtrl', PersonRatingCtrl);
}(angular));
