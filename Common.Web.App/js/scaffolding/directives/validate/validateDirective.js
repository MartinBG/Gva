// Usage: <input type="text" ng-model="username" name="username" sc-validate="{unique: isUnique}"/>

/*global angular,_*/
(function (angular, _) {
  'use strict';

  function ValidateDirective($parse, $q) {
    function addPendingValidation(control) {
      if (!control.$error.$pending) {
        control.$error.$pending = 0;
      }

      control.$error.$pending++;
    }

    function removePendingValidation(control) {
      control.$error.$pending--;

      if (control.$error.$pending === 0) {
        delete control.$error.$pending;
      }
    }

    return {
      restrict: 'A',
      require: ['?ngModel', '?form'],
      scope: false,
      link: function (scope, element, attrs, controllers) {
        var parentForms = _.map(element.parents('ng-form, [ng-form]'), function (formElem) {
              return angular.element(formElem).controller('form');
            }),
            scValidate = $parse(attrs.scValidate)(scope),
            validators = [],
            immediate = attrs.scValidateImmediate|| false,
            control,
            form;

        if (controllers[1]) {
          form = controllers[1];

          control = {
            $setValidity: function (validationErrorKey, isValid) {
              form.$setValidity(validationErrorKey, isValid, control);
            },
            $parsers: [],
            $error: {},
            $name: '__scValidateDummy'
          };

          form.$addControl(control);
        } else {
          control = controllers[0];
        }

        _.forOwn(scValidate, function (validationFn, validationErrorKey) {
          var validator = function (value) {
            var isValid = validationFn(value);

            // check if the result is promise
            if (isValid && isValid.then && typeof (isValid.then) === 'function') {
              control.$setValidity(validationErrorKey, false);
              addPendingValidation(control);
              parentForms.forEach(function (parentForm) {
                addPendingValidation(parentForm);
              });

              return isValid.then(function (result) {
                control.$setValidity(validationErrorKey, result);

                removePendingValidation(control);
                parentForms.forEach(function (parentForm) {
                  removePendingValidation(parentForm);
                });

                return result;
              }, function (reason) {
                removePendingValidation(control);
                parentForms.forEach(function (parentForm) {
                  removePendingValidation(parentForm);
                });

                return $q.reject(reason);
              });
            } else {
              control.$setValidity(validationErrorKey, isValid);

              return $q.when(isValid);
            }
          };

          if (immediate) {
            var validatorFn = function (value) {
              validator(value);

              return value;
            };

            control.$parsers.push(validatorFn);
            control.$formatters.push(validatorFn);
          } else {
            validators.push(validator);
          }
        });

        (form || control).$validate = function () {
          var validationResultPromises =
            _.map(validators, function (validator) { return validator(); });

          if (form) {
            _.forOwn(form, function (value, key) {
              // child controls are all properties not starting with $
              if (key.indexOf('$') >= 0) {
                return;
              }

              // check if child control has custom validation with scValidate
              if (value.$validate) {
                validationResultPromises.push(value.$validate());
              }
            });
          }

          return $q.all(validationResultPromises).then(function () {
            if (form) {
              form.$validated = true;

              var unwatch = scope.$watch('form.$readonly', function (readonly) {
                if (readonly) {
                  form.$validated = false;
                  unwatch();
                }
              });
            }

            return (form || control).$valid;
          });
        };
      }
    };
  }

  ValidateDirective.$inject = ['$parse', '$q'];

  angular.module('scaffolding').directive('scValidate', ValidateDirective);
}(angular, _));