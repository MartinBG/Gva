/*global angular*/
(function (angular) {
  'use strict';

  angular.module('common').factory('NomenclatureValuesRsrc', ['$resource', function ($resource) {
    return $resource('api/admin/nomenclatures/:nomId/values/:id', {});
  }]);
}(angular));
