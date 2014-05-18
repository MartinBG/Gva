/*global angular*/
(function (angular) {
  'use strict';

  angular.module('scaffolding', [
    'ng',
    'ui.bootstrap',
    // @ifndef DEBUG
    'scaffolding.templates',
    // @endif
    'ui.jq'
  ]);
}(angular));