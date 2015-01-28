// Usage: <sc-button type=""
//                   name=""
//                   btn-click=""
//                   btn-sref="{state: '...', params: {...}, options: {...}}"
//                   text=""
//                   class=""
//                   icon=""
//                   icon-disabled="">
// </sc-button>

/*global angular*/
(function (angular) {
  'use strict';

  function ButtonDirective($parse, $exceptionHandler, l10n, $state) {

    function ButtonLink(scope, element, attrs) {
      var elementCtrl = {};

      var sref;
      if (attrs.btnSref) {
        sref = $parse(attrs.btnSref)(scope.$parent);

        attrs.$set('href', $state.href(sref.state, sref.params, sref.options));
      }

      scope.$parent[attrs.name] = elementCtrl;

      element.on('click', function (event) {
        if (event.which !== 1) {
          // not left click
          return;
        }

        event.preventDefault();

        if (elementCtrl.$pending) {
          return;
        }

        scope.$apply(function () {
          var result;
          if (sref) {
            result = $state.go(sref.state, sref.params, sref.options);
          }
          else {
            // add $event local variables to the expression context as ngClick does
            result = scope.btnClick({ $event: event });
          }

          // check if the result is promise
          if (result && result.then && typeof (result.then) === 'function') {
            scope.$pending = true;
            elementCtrl.$pending = true;
            result['catch'](function (error) {
              $exceptionHandler(error);
            })['finally'](function () {
              delete elementCtrl.$pending;
              delete scope.$pending;
            });
          }
        });
      });

      attrs.$observe('text', function (text) {
        scope.text = l10n.get(text);
        if (!scope.text) {
          scope.text = text;
        }
      });

      scope.isDisabled = false;
      if (attrs.iconDisabled) {
        scope.$parent.$watch(attrs.ngDisabled, function (newVal) {
          scope.isDisabled = newVal;
        });
      }

      scope.iconBaseClass = scope.icon ? scope.icon.substring(0, scope.icon.indexOf('-')) : null;
    }
    return {
      restrict: 'E',
      replace: true,
      scope: {
        btnClick: '&',
        icon: '@',
        iconDisabled: '@'
      },
      templateUrl: 'js/scaffolding/directives/button/buttonDirective.html',
      compile: function compile(tElement, tAttrs) {
        if(!tAttrs.type) {
          tAttrs.type = 'button';
        }

        return ButtonLink;
      }
    };
  }

  ButtonDirective.$inject = ['$parse', '$exceptionHandler', 'l10n', '$state'];

  angular.module('scaffolding').directive('scButton', ButtonDirective);
}(angular));