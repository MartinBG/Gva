/*global angular*/
(function (angular) {
  'use strict';

  angular.module('scaffolding').factory('Nomenclature', [
    '$resource',
    function ($resource) {
      return $resource('/api/nomenclatures/:alias/:id/:valueAlias');
    }
  ]);
}(angular));
