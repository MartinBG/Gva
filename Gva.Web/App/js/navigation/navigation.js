/*global angular*/
(function (angular) {
  'use strict';
  angular.module('navigation', [
    'ng',
    'ui.router',
    'ui.bootstrap',
    'navigation.templates'
  ]).config(['$stateProvider', function ($stateProvider) {
    $stateProvider.state({
      name: 'root',
      views: {
        'rootView': {
          templateUrl: 'navigation/templates/navbar.html'
        }
      }
    });
  }]);
}(angular));
