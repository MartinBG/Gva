// Usage: <input type="text" ng-model="username" name="username" sc-validate="{unique: isUnique}"/>

/*global angular,_*/
(function (angular, _) {
  'use strict';

  function ValidateDirective() {
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
      require: 'ngModel',
      scope: {
        scValidate: '&'
      },
      link: function (scope, element, attrs, control) {
        var forms = _.map(element.parents('ng-form'), function (formElem) {
              return angular.element(formElem).controller('form');
            });

        _.forOwn(scope.scValidate(), function (validationFn, validationErrorKey) {
          var validator = function (value) {
            var isValid = validationFn(value);

            // check if the result is promise
            if (isValid && isValid.then && typeof (isValid.then) === 'function') {
              control.$setValidity(validationErrorKey, false);
              addPendingValidation(control);
              forms.forEach(function (form) {
                addPendingValidation(form);
              });

              isValid.then(function (result) {
                control.$setValidity(validationErrorKey, result);

                removePendingValidation(control);
                forms.forEach(function (form) {
                  removePendingValidation(form);
                });
              }, function () {
                removePendingValidation(control);
                forms.forEach(function (form) {
                  removePendingValidation(form);
                });
              });
            } else {
              control.$setValidity(validationErrorKey, isValid);
            }

            return value;
          };

          control.$parsers.push(validator);
        });
      }
    };
  }

  ValidateDirective.$inject = [];

  angular.module('scaffolding').directive('scValidate', ValidateDirective);
}(angular, _));