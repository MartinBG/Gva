// Usage: <ng-form sc-form-name="<formNameExpr>"></div>
/*global angular*/
(function (angular) {
  'use strict';

  function FormNameDirective($parse) {
    return {
      restrict: 'A',
      require: '?form',
      link: function (scope, element, attrs, elementForm) {
        var formName,
            localName,
            parentName,
            parentForm;

        if (!elementForm) {
          return;
        }

        if (attrs.scFormName) {
          formName = $parse(attrs.scFormName)(scope);
          if (formName.local || formName.parent) {
            localName = formName.local;
            parentName = formName.parent;
          } else {
            parentName = formName;
          }
        }

        if (localName) {
          scope[localName] = elementForm;
          element.on('$destroy', function () {
            scope[localName] = undefined;
          });
        }

        if (parentName && !elementForm.$name) {
          parentForm = element.parent().controller('form');
          elementForm.$name = parentName;
          parentForm.$addControl(elementForm);
        }
      }
    };
  }

  FormNameDirective.$inject = ['$parse'];

  angular.module('scaffolding')
    .directive('scFormName', FormNameDirective);
}(angular));
