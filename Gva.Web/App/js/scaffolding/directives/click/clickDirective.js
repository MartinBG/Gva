// Usage: <button type="button" sc-click="save()" name="saveButton">Save</button>

/*global angular*/
(function (angular) {
  'use strict';

  function ClickDirective($parse) {
    return {
      restrict: 'A',
      link: function (scope, element, attrs) {
        var elementCtrl = {};
        var clickExpr = $parse(attrs.scClick);

        scope[attrs.name] = elementCtrl;

        element.on('click', function (event) {
          if (elementCtrl.$pending) {
            return;
          }

          scope.$apply(function () {
            // add $event local variables to the expression context as ngClick does
            var result = clickExpr(scope, { $event: event });

            // check if the result is promise
            if (result && result.then && typeof (result.then) === 'function') {
              elementCtrl.$pending = true;
              result['finally'](function () {
                delete elementCtrl.$pending;
              });
            }
          });
        });
      }
    };
  }

  ClickDirective.$inject = ['$parse'];

  angular.module('scaffolding').directive('scClick', ClickDirective);
}(angular));