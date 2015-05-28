/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function IndividualActRefusalCtrl(
    $scope,
    Docs
  ) {
    Docs.getRioEditableFile({ id: $scope.model.docId }).$promise.then(function (result) {
      $scope.model.jObject = result.content;

      if ($scope.model.jObject.individualAdministrativeActRefusalAppealTerm) {
        $scope.deadlinePeriod = {
          days: moment.duration($scope.model.jObject.individualAdministrativeActRefusalAppealTerm)
            .asDays()
        };
      }
      else {
        $scope.deadlinePeriod = {
          days: null
        };
      }

      $scope.isLoaded = true;
    });

    $scope.deadlinePeriodChange = function () {
      if ($scope.deadlinePeriod.days) {
        $scope.model.jObject.individualAdministrativeActRefusalAppealTerm =
          'P' + $scope.deadlinePeriod.days + 'D';
      }
    };
  }

  IndividualActRefusalCtrl.$inject = [
    '$scope',
    'Docs'
  ];

  angular.module('ems').controller('IndividualActRefusalCtrl', IndividualActRefusalCtrl);
}(angular, moment));
