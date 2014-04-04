﻿/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ScaffoldingProvider($compileProvider) {
    this.form = function (options) {
      $compileProvider.directive(options.name, function ($parse) {
        return {
          restrict: 'E',
          replace: true,
          scope: {
            model: '=ngModel'
          },
          templateUrl: options.templateUrl,
          controller: options.controller,
          link: function (scope, element, attrs) {
            var eventHandlers = {};

            if (attrs.readonly) {
              scope.$parent.$watch(attrs.readonly, function (readonly) {
                scope.readonly = readonly;
              });
            }

            scope.$parent[attrs.name] = scope[attrs.name];
            scope.form = scope[attrs.name];
            scope.$raise = function (eventName, message) {
              if (eventHandlers[eventName]) {
                return eventHandlers[eventName](message);
              }
            };

            _.forOwn(attrs, function (value, key) {
              if (key.indexOf('scOn') === 0) {
                var parsedFunc = $parse(value);

                eventHandlers[key] = function (message) {
                  return parsedFunc(scope.$parent, { $message: message });
                };
              }
            });
          }
        };
      });
    };
  }

  ScaffoldingProvider.$inject = ['$compileProvider'];

  ScaffoldingProvider.prototype.$get = function () {
  };

  angular.module('scaffolding').provider('scaffolding', ScaffoldingProvider);
}(angular, _));
