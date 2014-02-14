// Usage:
//<gva-validation-error field-name="fieldName" validations="{val1:'l10n', val2: null, ...}">
//</gva-validation-error>

/*global angular,_*/
(function (angular, _) {
  'use strict';

  function ValidationErrorDirective(l10n, gvaValidationErrorConfig) {

    return {
      restrict: 'E',
      scope: {
        fieldName: '@',
        getValidations: '&validations'
      },
      templateUrl: 'gva/directives/validationError/validationErrorDirective.html',
      link: function (scope, element) {
        scope.form = element.parents('ng-form').first().controller('form');
        scope.validations = [];
        _.forOwn(scope.getValidations(), function (text, type) {
          scope.validations.push({
            type: type,
            text: l10n.get(text || gvaValidationErrorConfig.defaultErrorTexts[type])
          });
        });
      }
    };
  }

  ValidationErrorDirective.$inject = ['l10n', 'gvaValidationErrorConfig'];

  angular.module('gva')
    .constant('gvaValidationErrorConfig', {
      defaultErrorTexts: {
        required: 'errorTexts.required',
        min: 'errorTexts.min'
      }
    })
    .directive('gvaValidationError', ValidationErrorDirective);
}(angular, _));