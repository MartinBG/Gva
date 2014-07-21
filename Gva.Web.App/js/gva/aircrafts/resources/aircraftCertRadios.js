﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('AircraftCertRadios', ['$resource', function ($resource) {
    return $resource('api/aircrafts/:id/aircraftCertRadios/:ind');
  }]);
}(angular));