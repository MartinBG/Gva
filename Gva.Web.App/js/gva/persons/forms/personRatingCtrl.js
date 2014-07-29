/*global angular*/
(function (angular) {
  'use strict';

  function PersonRatingCtrl($scope, scFormParams) {
    $scope.isNew = scFormParams.isNew;
  }

  PersonRatingCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonRatingCtrl', PersonRatingCtrl);
}(angular));
