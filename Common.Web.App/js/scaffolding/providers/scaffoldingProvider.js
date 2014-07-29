/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ScaffoldingProvider($compileProvider) {
    this.form = function (options) {
      $compileProvider.directive(options.name, function ($parse, $controller) {
        return {
          restrict: 'E',
          replace: true,
          scope: {
            model: '=ngModel'
          },
          templateUrl: options.templateUrl,
          link: {
            pre: function (scope, element, attrs) {
              if (options.controller) {
                var scFormParams = $parse(attrs.scFormParams)(scope.$parent);
                var locals = {
                  $scope: scope,
                  $element: element,
                  $attrs: attrs,
                  scFormParams: scFormParams ? scFormParams : {}
                };

                $controller(options.controller, locals);
              }
              var eventHandlers = {};

              scope.$raise = function (eventName, message) {
                if (eventHandlers[eventName]) {
                  return eventHandlers[eventName](message);
                }
              };

              _.forOwn(attrs, function (value, key) {
                // event handler
                if (key.indexOf('scOn') === 0) {
                  var parsedFunc = $parse(value);

                  eventHandlers[key] = function (message) {
                    return parsedFunc(scope.$parent, { $message: message });
                  };
                }

                // data attribute
                if (key.indexOf('scData') === 0) {
                  var dataKey = key.substring('scData'.length);
                  dataKey = dataKey[0].toLowerCase() + dataKey.substring(1);
                  scope[dataKey] = $parse(value)(scope.$parent);

                  scope.$parent.$watch(value, function (newValue) {
                    scope[dataKey] = newValue;
                  });
                }
              });
            },
            post: function (scope, element, attrs) {
              if (attrs.readonly) {
                scope.$parent.$watch(attrs.readonly, function (readonly) {
                  scope.readonly = readonly;
                });
              }

              scope.$parent[attrs.name] = scope[attrs.name];
              scope.form = scope[attrs.name];
            }
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
