/*global angular*/
(function (angular) {
  'use strict';

  function PersonRatingEditionCtrl($scope, scFormParams) {
    $scope.lotId = scFormParams.lotId;

    $scope.notesShouldBeFilled = function () {
      if(!$scope.model.part.notes && $scope.model.part.notesAlt) {
        return false;
      }
      return true;
    };

    $scope.notesAltShouldBeFilled = function () {
      if($scope.model.part.notes && !$scope.model.part.notesAlt) {
        return false;
      }
      return true;
    };
  }

  PersonRatingEditionCtrl.$inject = ['$scope', 'scFormParams'];

  angular.module('gva').controller('PersonRatingEditionCtrl', PersonRatingEditionCtrl);
}(angular));
