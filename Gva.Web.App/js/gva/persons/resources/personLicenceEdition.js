/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('PersonLicenceEdition', ['$resource', function ($resource) {
    return $resource('/api/persons/:id/licences/:ind/editions/:childInd');
  }]);
}(angular));