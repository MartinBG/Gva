/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonEditionView', ['$resource', function($resource) {
    return $resource('/api/persons/:id/ratings/:ind/editionViews');
  }]);
}(angular));
