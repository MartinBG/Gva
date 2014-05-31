// Usage: <a sc-sref="{state: '...', params: {...}}" name="linkName">text</a>

/*global angular*/
(function (angular) {
  'use strict';

  function SrefDirective($parse, $state) {
    return {
      restrict: 'A',
      priority: 110,
      link: function (scope, element, attrs) {
        var elementCtrl = {},
          srefExpr = $parse(attrs.scSref)(scope),
          state = srefExpr.state,
          params = srefExpr.params,
          link =  $state.href(state, params);

        attrs.$set('href', link);

        scope[attrs.name] = elementCtrl;
      }
    };
  }

  SrefDirective.$inject = ['$parse', '$state'];

  angular.module('scaffolding').directive('scSref', SrefDirective);
}(angular));
