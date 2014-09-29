/*global angular, _*/
(function (angular, _) {
  'use strict';

  function RatingEditionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonRatingEditions,
    currentRatingEdition,
    ratingEditions,
    scMessage
  ) {
    var originalRatingEdition = _.cloneDeep(currentRatingEdition);
    $scope.currentRatingEdition = currentRatingEdition;
    $scope.ratingEditions = ratingEditions;
    $scope.rating = $scope.$parent.rating;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.appId = $stateParams.appId;
    $scope.lastEditionIndex = _.last(ratingEditions).partIndex;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editRatingEditionForm.$validate().then(function () {
        if ($scope.editRatingEditionForm.$valid) {
          return PersonRatingEditions
            .save({
              id: $stateParams.id,
              ind: $stateParams.ind,
              index: $stateParams.index
            }, $scope.currentRatingEdition)
            .$promise
            .then(function (edition) {
              $scope.editMode = null;
              var editionIndex = null;
              _.find($scope.ratingEditions, function (ed, index) {
                if (ed.partIndex === edition.partIndex) {
                  editionIndex = index;
                }
              });
              originalRatingEdition = _.cloneDeep($scope.currentRatingEdition);
              $scope.ratingEditions[editionIndex] = _.cloneDeep(edition);
            });
        }
      });
    };

    $scope.deleteCurrentEdition = function () {

      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          $scope.ratingEditions = _.remove($scope.ratingEditions, function (le) {
            return le.partIndex !== currentRatingEdition.partIndex;
          });
          return PersonRatingEditions
            .remove({
              id: $stateParams.id,
              ind: $stateParams.ind,
              index: $stateParams.index
            })
            .$promise.then(function () {
              if ($scope.ratingEditions.length === 0) {
                return $state.go('root.persons.view.ratings.search');
              }
              else {
                return $state.go(
                  'root.persons.view.ratings.edit.editions.edit',
                  { index: _.last($scope.ratingEditions).partIndex });
              }
            });
        }
      });
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.currentRatingEdition = _.cloneDeep(originalRatingEdition);
    };
  }

  RatingEditionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonRatingEditions',
    'currentRatingEdition',
    'ratingEditions',
    'scMessage'
  ];

  RatingEditionsEditCtrl.$resolve = {
    currentRatingEdition: [
      '$stateParams',
      'PersonRatingEditions',
      function ($stateParams, PersonRatingEditions) {
        return PersonRatingEditions.get({
          id: $stateParams.id,
          ind: $stateParams.ind,
          index: $stateParams.index
        }).$promise;
      }
    ],
    ratingEditions: [
      '$stateParams',
      'PersonRatingEditions',
      function ($stateParams, PersonRatingEditions) {
        return PersonRatingEditions.query({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('RatingEditionsEditCtrl', RatingEditionsEditCtrl);
}(angular, _));
