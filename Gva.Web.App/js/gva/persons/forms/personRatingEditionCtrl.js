/*global angular*/
(function (angular) {
  'use strict';

  function PersonRatingEditionCtrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;
  }

  PersonRatingEditionCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonRatingEditionCtrl', PersonRatingEditionCtrl);
}(angular));
