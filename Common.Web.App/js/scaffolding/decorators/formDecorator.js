/*global angular*/
(function (angular) {
  'use strict';

  function formDecorator($provide) {

    var decorator = function ($delegate) {

      var form = $delegate[0], controller = form.controller;

      form.controller = ['$scope', '$element', '$attrs', '$injector',
      function (scope, element, attrs, $injector) {
        var $interpolate = $injector.get('$interpolate');
        attrs.$set('name', $interpolate(attrs.name || attrs.ngForm || '')(scope));
        $injector.invoke(controller, this, {
          '$scope': scope,
          '$element': element,
          '$attrs': attrs
        });
      }];

      return $delegate;
    };

    $provide.decorator('formDirective', ['$delegate', decorator]);
    $provide.decorator('ngFormDirective', ['$delegate', decorator]);
  }

  angular.module('scaffolding').config(formDecorator);
}(angular));