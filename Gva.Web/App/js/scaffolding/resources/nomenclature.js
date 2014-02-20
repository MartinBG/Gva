/*global angular*/
(function (angular) {
  'use strict';

  angular.module('scaffolding').factory('Nomenclature', [
    '$resource',
    function ($resource) {
      return $resource('/api/nomenclatures/:alias?term=:term&id=:id&va=:valueAlias', {
        id: '@nomValueId'
      });
    }
  ]);
}(angular));
