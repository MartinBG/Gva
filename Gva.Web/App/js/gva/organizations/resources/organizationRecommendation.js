﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationRecommendation', ['$resource', function ($resource) {
    return $resource('/api/organizations/:id/recommendations/:ind');
  }]);
}(angular));