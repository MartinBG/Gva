/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ScaffoldingProvider($compileProvider) {
    this.form = function (options) {
      $compileProvider.directive(options.name, function ($parse) {
        return {
          restrict: 'E',
          replace: true,
          scope: {
            model: '=ngModel',
            readonly: '=readonly'
          },
          templateUrl: options.templateUrl,
          controller: options.controller,
          link: function (scope, element, attrs) {
            scope.$parent[attrs.name] = scope[attrs.name];
            scope.form = scope[attrs.name];

            _.forOwn(attrs, function (value, key) {
              if (key.indexOf('scOn') === 0) {
                var parsedFunc = $parse(value);

                scope.$on(key, function (event, message) {
                  event.stopPropagation();
                  parsedFunc(scope.$parent, { $message: message });
                });
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
