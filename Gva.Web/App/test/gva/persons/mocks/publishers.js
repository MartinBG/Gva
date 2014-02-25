/*global angular, require*/
(function (angular, require) {
  'use strict';

  var organizations = require('./organization'),
      otherDocPublishers = require('./otherDocPublisher');

  angular.module('app').constant('publishers', {
    organizations: organizations,
    otherDocPublishers: otherDocPublishers
  });
}(angular, require));
