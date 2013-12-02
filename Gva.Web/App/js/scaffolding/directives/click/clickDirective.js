// Usage: <button type="button" sc-click="save()" name="saveButton">Save</button>

/*global angular*/
(function (angular) {
  'use strict';

  function ClickDirective() {
    return {
      restrict: 'A',
      scope: {
        scClick: '&'
      },
      link: function (scope, element, attrs) {
        var elementCtrl = {};

        scope.$parent[attrs.name] = elementCtrl;

        element.on('click', function (event) {
          if (elementCtrl.$pending) {
            return;
          }

          scope.$apply(function () {
            // add $event local variables to the expression context as ngClick does
            var result = scope.scClick({ $event: event });

            // check if the result is promise
            if (result.then && typeof (result.then) === 'function') {
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

  ClickDirective.$inject = [];

  angular.module('scaffolding').directive('scClick', ClickDirective);
}(angular));