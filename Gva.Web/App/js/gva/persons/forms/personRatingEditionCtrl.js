/*global angular, require*/
(function (angular) {
  'use strict';

  function PersonRatingEditionCtrl($scope) {
    var nomenclatures = require('./nomenclatures.sample');

    $scope.limitationTypes = nomenclatures.ratingLimitationTypes.map(function (limitation) {
      limitation.text = limitation.name;
      limitation.id = limitation.nomValueId;
      return limitation;
    });

    $scope.ratingSubClasses = nomenclatures.ratingSubClasses.map(function (subclass) {
      subclass.text = subclass.name;
      subclass.id = subclass.nomValueId;
      return subclass;
    });
  }

  PersonRatingEditionCtrl.$inject = ['$scope'];

  angular.module('gva').controller('PersonRatingEditionCtrl', PersonRatingEditionCtrl);
}(angular));
