/*global angular, moment, _*/
(function (angular, moment, _) {
  'use strict';

  function RemovingIrregularityCtrl(
    $scope,
    $q,
    Docs,
    Nomenclatures
  ) {
    $q.all({
      jObject: Docs.getRioEditableFile({ id: $scope.model.docId }).$promise,
      irregularityTypes: Nomenclatures.query({ alias: 'irregularityType' }).$promise
    }).then(function (results) {
      $scope.model.jObject = results.jObject.content;

      if ($scope.model.jObject.deadlineCorrectionIrregularities) {
        $scope.deadlinePeriod = {
          days: moment.duration($scope.model.jObject.deadlineCorrectionIrregularities).asDays()
        };
      }
      else {
        $scope.deadlinePeriod = {
          days: null
        };
      }

      $scope.irregularitiesCollection = [];
      if ($scope.model.jObject.irregularitiesCollection.length !== 0 &&
          $scope.model.jObject.irregularitiesCollection[0].irregularityType !== '') {
        $scope.irregularitiesCollection = _.map($scope.model.jObject.irregularitiesCollection, function (item) {
          return _.find(results.irregularityTypes, function (irregularityType) {
            return irregularityType.name === item.irregularityType;
          });
        });
      }

      $scope.isLoaded = true;
    });

    $scope.deadlinePeriodChange = function () {
      if ($scope.deadlinePeriod.days) {
        $scope.model.jObject.deadlineCorrectionIrregularities =
          'P' + $scope.deadlinePeriod.days + 'D';
      }
    };

    $scope.irregularityChange = function (irregularitiesCollection) {
      $scope.model.jObject.irregularitiesCollection =
        _.map(irregularitiesCollection, function (item) {
          return {
            irregularityTypeId: item.nomValueId,
            irregularityType: item.name,
            additionalInformationSpecifyingIrregularity: item.description
          };
      });
    };
  }

  RemovingIrregularityCtrl.$inject = [
    '$scope',
    '$q',
    'Docs',
    'Nomenclatures'
  ];

  angular.module('ems').controller('RemovingIrregularityCtrl', RemovingIrregularityCtrl);
}(angular, moment, _));
