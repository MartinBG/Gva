/*global angular, require*/
(function (angular, require) {
  'use strict';

  var auditResults = require('./auditResult');

  angular.module('app').constant('auditResults', auditResults);
}(angular, require));
