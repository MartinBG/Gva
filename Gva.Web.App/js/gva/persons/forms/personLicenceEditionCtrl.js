/*global angular, _, moment*/
(function (angular, _, moment) {
  'use strict';

  function PersonLicenceEditionCtrl(
    $scope,
    scFormParams
  ) {
    $scope.lotId = scFormParams.lotId;
    $scope.isNew = scFormParams.isNew;

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

    if($scope.isNew && $scope.model.part.documentDateValidFrom === undefined){
      $scope.model.part.documentDateValidFrom = moment(new Date());
    }
  }

  PersonLicenceEditionCtrl.$inject = [
    '$scope',
    'scFormParams'
  ];

  angular.module('gva').controller('PersonLicenceEditionCtrl', PersonLicenceEditionCtrl);
}(angular, _, moment));
