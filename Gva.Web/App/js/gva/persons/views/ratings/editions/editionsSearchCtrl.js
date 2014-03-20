/*global angular*/
(function (angular) {
  'use strict';

  function EditionsSearchCtrl($scope, $state, $stateParams, PersonEdition, editions) {
    $scope.editions = editions;

    $scope.editEdition = function (item) {
      return $state.go('root.persons.view.ratings.editions.edit', {
        id: $stateParams.id,
        ind: $stateParams.ind,
        childInd: item.ratingEdition.partIndex
      });
    };

    $scope.deleteEdition = function (item) {
      return PersonEdition
        .remove({
          id: $stateParams.id,
          ind: $stateParams.ind,
          childInd: item.ratingEdition.partIndex
        }).$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newEdition = function () {
      return $state.go('root.persons.view.ratings.editions.new');
    };
  }

  EditionsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonEdition', 'editions'];

  EditionsSearchCtrl.$resolve = {
    editions: [
      '$stateParams',
      'PersonRating',
      'PersonEdition',
      function ($stateParams, PersonRating, PersonEdition) {
        return PersonRating.get($stateParams).$promise.then(function (rating) {
          return PersonEdition.query($stateParams).$promise.then(function (editions) {
            return editions.map(function (item) {
              var mappedItem = {
                ratingEdition: item,
                rating: {
                  firstEditionValidFrom: rating.firstEditionValidFrom,
                  ratingTypeOrRatingLevel: rating.rating.personRatingLevel ?
                    rating.rating.personRatingLevel : rating.rating.ratingType,
                  authorizationAndLimitations: rating.rating.authorization ?
                    rating.rating.authorization.name : '',
                  personRatingModel: rating.rating.personRatingModel
                }
              };

              var subClasses = '', i;
              if (item.part.ratingSubClasses) {
                for (i = 0; i < item.part.ratingSubClasses.length; i++) {
                  subClasses += item.part.ratingSubClasses[i].name + ', ';
                }
                subClasses = ' ( ' + subClasses.substr(0, subClasses.length - 2) + ' )';
              }

              mappedItem.rating.classOrSubclass = rating.rating.aircraftTypeGroup ?
                rating.rating.aircraftTypeGroup.name : rating.rating.ratingClass.name + subClasses;

              var limitations = '';
              if (item.part.limitations) {
                for (i = 0; i < item.part.limitations.length; i++) {
                  limitations += item.part.limitations[i].name + ', ';
                }
                limitations = limitations.substring(0, limitations.length - 2);
                rating.authorizationAndLimitations += ' ' + limitations;
              }

              return mappedItem;
            });
          });
        });
      }
    ]
  };

  angular.module('gva').controller('EditionsSearchCtrl', EditionsSearchCtrl);
}(angular));