/*global angular*/
(function (angular) {
  'use strict';

  angular.module('common').factory('NomenclaturesRsrc', ['$resource', function ($resource) {
    return $resource('api/admin/nomenclatures/:id', {});
  }]);
}(angular));
