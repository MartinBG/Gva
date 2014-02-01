/*global angular*/
(function (angular) {
  'use strict';

  function ЕditionsSearchCtrl($scope, $state, $stateParams, PersonEdition) {
    PersonEdition.query($stateParams).$promise.then(function (editions) {
      $scope.editions = editions.map(function (item) {
        item.rating.ratingTypeOrRatingLevel = item.rating.personRatingLevel ?
          item.rating.personRatingLevel : item.rating.ratingType;
        var subClasses = '', i;
        if (item.ratingEdition.part.ratingSubClasses) {
          for (i = 0; i < item.ratingEdition.part.ratingSubClasses.length; i++) {
            subClasses += item.ratingEdition.part.ratingSubClasses[i].name + ', ';
          }
          subClasses = ' ( ' + subClasses.substr(0, subClasses.length - 2) + ' )';
        }

        item.rating.classOrSubclass = item.rating.aircraftTypeGroup ?
        item.rating.aircraftTypeGroup.name : item.rating.ratingClass.name + subClasses;

        item.rating.authorizationAndLimitations = item.rating.authorization ?
          item.rating.authorization.name : '';
        var limitations = '';
        if (item.ratingEdition.part.limitations) {
          for (i = 0; i < item.ratingEdition.part.limitations.length; i++) {
            limitations += item.ratingEdition.part.limitations[i].name + ', ';
          }
          limitations = limitations.substring(0, limitations.length - 2);
          item.rating.authorizationAndLimitations += ' ' + limitations;
        }

        return item;
      });
    });
    
    $scope.editEdition = function (item) {
      return $state.go('persons.editions.edit', {
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
      return $state.go('persons.editions.new');
    };
  }

  ЕditionsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonEdition'];

  angular.module('gva').controller('ЕditionsSearchCtrl', ЕditionsSearchCtrl);
}(angular));