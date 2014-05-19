/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonEdition', ['$resource', function($resource) {
    return $resource('/api/persons/:id/ratings/:ind/editions/:childInd');
  }]);
}(angular));
