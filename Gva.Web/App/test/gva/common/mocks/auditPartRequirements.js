/*global angular, require*/
(function (angular, require) {
  'use strict';

  var auditPartRequirmants = require('./auditPartRequirement');

  angular.module('app').constant('auditPartRequirmants', auditPartRequirmants);
}(angular, require));
