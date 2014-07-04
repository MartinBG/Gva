// Usage: <sc-field type="int" model="" text="" validations=""></sc-field>

/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function FieldDirective($compile, $parse) {
    function FieldCompile(tElement, tAttrs) {
      var type = tAttrs.type,
          customType = tAttrs.customType,
          model = tAttrs.ngModel,
          text = tAttrs.l10nText,
          validations = tAttrs.validations,
          cssClass = tAttrs['class'],
          hasValidations = false,
          currentValidations = [],
          fieldName,
          containerElement,
          labelElement,
          validationElement,
          dirElement;

      var validationsList = {
        'ng-required': 'required',
        'ng-pattern': 'pattern',
        'minlength': 'minlength',
        'maxlength': 'maxlength',
        'min': 'min',
        'max': 'max'
      };

      if (!tAttrs.name) {
        fieldName = _.contains(model, '.') ? _.last(model.split('.')) : model;
      }
      else {
        fieldName = tAttrs.name;
      }

      if (!cssClass && type === 'date') {
        cssClass = 'col-sm-3 col-md-2';
      }

      if (customType !== undefined) {
        dirElement = $('<' + customType + '></' + customType + '>');
      }
      else {
        dirElement = $('<sc-' + type + '></sc-' + type + '>');
      }

      dirElement.attr('ng-model', model)
          .attr('name', fieldName);

      if (!tAttrs.ngReadonly) {
        dirElement.attr('ng-readonly', 'form.$readonly');
      }

      _.forOwn(tAttrs, function (val, key) {
        if (key === 'type' || key === 'ngModel' || key === '$$element' || key === '$attr') {
          return;
        }
        tElement.removeAttr(tAttrs.$attr[key]);
        if (key === 'l10nText' || key === 'validations' || key === 'class' || key === 'ngShow') {
          return;
        }
        if (val) {
          dirElement.attr(tAttrs.$attr[key], val);
        }
        else {
          dirElement.attr(tAttrs.$attr[key], true);
        }
        if (_.has(validationsList, tAttrs.$attr[key])) {
          hasValidations = true;
          currentValidations.push(tAttrs.$attr[key]);
        }
      });

      containerElement = $('<div></div>').addClass('form-group ' + cssClass);
      labelElement = $('<label></label>').addClass('control-label').attr('l10n-text', text);
      containerElement.append(labelElement);

      if (tAttrs.ngShow) {
        containerElement.attr(tAttrs.$attr.ngShow, tAttrs.ngShow);
      }

      if (validations) {
        var mergedValidation = $parse(validations)(null);
        if (hasValidations) {
           _(currentValidations).forEach(function (valName) {
             if (!_.has(mergedValidation, validationsList[valName])) {
               mergedValidation[validationsList[valName]] = 'default';
             }
           });
        }
        containerElement.attr('sc-has-error', fieldName);
        validationElement =
          $('<sc-validation-error></sc-validation-error>')
            .attr('field-name', fieldName)
            .attr('validations', JSON.stringify(mergedValidation));
        containerElement.append(' ');
        containerElement.append(validationElement);
      }
      else if (hasValidations) {
        var defaultValidation = {};
        _(currentValidations).forEach(function (valName) {
          defaultValidation[validationsList[valName]] = 'default';
        });
        containerElement.attr('sc-has-error', fieldName);
        validationElement =
          $('<sc-validation-error></sc-validation-error>')
            .attr('field-name', fieldName)
            .attr('validations', JSON.stringify(defaultValidation));
        containerElement.append(' ');
        containerElement.append(validationElement);
      }

      containerElement.append(dirElement);
      tElement.append(containerElement);

      return {
        pre: function preLink(scope, iElement) {
          $compile(iElement.children())(scope);
        }
      };
    }

    return {
      restrict: 'E',
      terminal: true,
      priority: 120,
      compile: FieldCompile
    };
  }

  FieldDirective.$inject = ['$compile', '$parse'];

  angular.module('scaffolding')
    .directive('scField', FieldDirective);
}(angular, _, $));
