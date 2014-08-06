/*global angular*/
(function (angular) {
  'use strict';

  function ngModelDecorator($provide) {
    $provide.decorator('ngModelDirective', ['$delegate', function ($delegate) {

      var ngModel = $delegate[0], controller = ngModel.controller;

      ngModel.controller = ['$scope', '$element', '$attrs', '$injector',
        function (scope, element, attrs, $injector) {
          var $interpolate = $injector.get('$interpolate');
          attrs.$set('name', $interpolate(attrs.name || '')(scope));
          $injector.invoke(controller, this, {
            '$scope': scope,
            '$element': element,
            '$attrs': attrs
          });
        }];

      return $delegate;
    }]);
  }

  angular.module('scaffolding').config(ngModelDecorator);
}(angular));