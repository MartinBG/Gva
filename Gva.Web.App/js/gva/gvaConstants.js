/*global angular*/
(function (angular) {
  'use strict';

  angular.module('app').constant('gvaConstants', {
    regMarkPattern: /^LZ-[A-Z,0-9]{3,}$/
  });
}(angular));