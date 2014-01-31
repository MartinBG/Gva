/*global angular*/
(function (angular) {
  'use strict';

  function RatingsSearchCtrl($scope, $state, $stateParams, PersonRating) {
    PersonRating.query($stateParams).$promise.then(function (ratings) {
      $scope.ratings = ratings.map(function (item) {
        item.rating.ratingTypeOrRatingLevel = item.rating.personRatingLevel ?
          item.rating.personRatingLevel : item.rating.ratingType;
        var subClasses = '', i;
        if (item.ratingEdition.part.ratingSubClasses &&
          item.ratingEdition.part.ratingSubClasses.length > 0) {
          for (i = 0; i < item.ratingEdition.part.ratingSubClasses.length; i++) {
            subClasses += item.ratingEdition.part.ratingSubClasses[i].name + ', ';
          }
          subClasses = ' ( ' + subClasses.substr(0, subClasses.length - 2) + ' )';
        }

        item.rating.authorizationAndLimitations = item.rating.authorization ?
          item.rating.authorization.name : '';

        var limitations = '';
        if (item.ratingEdition.part.limitations) {
          for (i = 0; i < item.ratingEdition.part.limitations.length; i++) {
            limitations += item.ratingEdition.part.limitations[i].name + ', ';
          }
          limitations = limitations.substring(0, limitations.length - 2);
          item.rating.authorizationAndLimitations +=  ' ' + limitations;
        }

        item.rating.classOrSubclass = item.rating.aircraftTypeGroup ?
        item.rating.aircraftTypeGroup.name : item.rating.ratingClass.name + subClasses;
        return item;
      });
    });

    $scope.viewEdition = function (item) {
      return $state.go('persons.editions.search', { id: $stateParams.id, ind: item.partIndex });
    };

    $scope.newRating = function () {
      return $state.go('persons.ratings.new');
    };
  }

  RatingsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonRating'];

  angular.module('gva').controller('RatingsSearchCtrl', RatingsSearchCtrl);
}(angular));