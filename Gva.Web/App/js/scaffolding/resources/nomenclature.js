/*global angular*/
(function (angular) {
  'use strict';

  angular.module('scaffolding').factory('scaffolding.Nomenclature', [
    '$resource',
    function ($resource) {
      return $resource('/api/nomenclatures/:alias', {alias:'@alias'});
    }
  ]);
}(angular));
