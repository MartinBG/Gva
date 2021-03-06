﻿/*global angular*/
(function (angular) {
  'use strict';

  angular.module('gva').factory('OrganizationCertGroundServiceOperatorsSnoOperationals',
    ['$resource', function ($resource) {
      var path = 'api/organizations/:id/organizationGroundServiceOperatorsSnoOperational/:ind';
      return $resource(path, {}, {
        newGroundServiceOperatorsSnoOperational: {
          method: 'GET',
          url: 'api/organizations/:id/organizationGroundServiceOperatorsSnoOperational/new'
        }
      });
    }]);
}(angular));
