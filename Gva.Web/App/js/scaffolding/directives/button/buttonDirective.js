// Usage: <sc-button name="" btn-click="" text="" class="" icon=""></sc-button>

/*global angular*/
(function (angular) {
  'use strict';

  function ButtonDirective($parse, l10n) {

    return {
      restrict: 'E',
      replace: true,
      scope: {
        btnClick: '&',
        icon: '@'
      },
      templateUrl: 'scaffolding/directives/button/buttonDirective.html',
      link: function (scope, element, attrs) {

        var elementCtrl = {};

        scope.$parent[attrs.name] = elementCtrl;

        element.on('click', function (event) {
          if (elementCtrl.$pending) {
            return;
          }

          scope.$apply(function () {
            // add $event local variables to the expression context as ngClick does

            var result = scope.btnClick({ $event: event });

            // check if the result is promise
            if (result && result.then && typeof (result.then) === 'function') {
              scope.$pending = true;
              elementCtrl.$pending = true;
              result['finally'](function () {
                delete elementCtrl.$pending;
                delete scope.$pending;
              });
            }
          });
        });

        scope.text = l10n.get(attrs.text);
        if (!scope.text) {
          scope.text = attrs.text;
        }

      }
    };
  }

  ButtonDirective.$inject = ['$parse', 'l10n'];

  angular.module('scaffolding').directive('scButton', ButtonDirective);
}(angular));