/*global angular*/
(function (angular) {
  'use strict';

  function ScaffoldingProvider($compileProvider) {
    this.form = function (options) {
      $compileProvider.directive(options.name, function () {
        return {
          restrict: 'E',
          replace: true,
          scope: {
            model: '=ngModel'
          },
          templateUrl: options.templateUrl,
          controller: options.controller,
          link: function (scope, element, attrs) {
            scope.$parent[attrs.name] = scope[attrs.name];
          }
        };
      });
    };
  }

  ScaffoldingProvider.$inject = ['$compileProvider'];

  ScaffoldingProvider.prototype.$get = function () {
  };

  angular.module('scaffolding').provider('scaffolding', ScaffoldingProvider);
}(angular));