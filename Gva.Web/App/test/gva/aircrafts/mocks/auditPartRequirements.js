/*global angular, require*/
(function (angular, require) {
  'use strict';

  var auditPartRequirements = require('./auditPartRequirement');

  angular.module('app').constant('auditPartRequirements', auditPartRequirements);
}(angular, require));
