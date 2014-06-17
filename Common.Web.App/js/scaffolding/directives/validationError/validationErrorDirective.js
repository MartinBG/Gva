// Usage:
//<sc-validation-error field-name="fieldName" validations="{val1:'l10n', val2: null, ...}">
//</sc-validation-error>

/*global angular,_*/
(function (angular, _) {
  'use strict';

  function ValidationErrorDirective(l10n, scValidationErrorConfig) {

    return {
      restrict: 'E',
      scope: {
        fieldName: '@',
        getValidations: '&validations'
      },
      templateUrl: 'js/scaffolding/directives/validationError/validationErrorDirective.html',
      link: function (scope, element) {
        scope.form = element.parent().controller('form');
        scope.validations = [];
        _.forOwn(scope.getValidations(), function (text, type) {
          scope.validations.push({
            type: type,
            text: l10n.get(text !== 'default' ?
              text :
              scValidationErrorConfig.defaultErrorTexts[type])
          });
        });
      }
    };
  }

  ValidationErrorDirective.$inject = ['l10n', 'scValidationErrorConfig'];

  angular.module('scaffolding')
    .constant('scValidationErrorConfig', {
      defaultErrorTexts: {
        required: 'defaultErrorTexts.required',
        pattern: 'defaultErrorTexts.pattern',
        minlength: 'defaultErrorTexts.minlength',
        maxlength: 'defaultErrorTexts.maxlength',
        min: 'defaultErrorTexts.min',
        max: 'defaultErrorTexts.max',
        unique: 'defaultErrorTexts.unique'
      }
    })
    .directive('scValidationError', ValidationErrorDirective);
}(angular, _));
