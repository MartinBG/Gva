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
      require: ['ngModel', '^form'],
      scope: {
        scValidate: '&'
      },
      link: function (scope, element, attrs, requiredControllers) {
        var control = requiredControllers[0],
            form = requiredControllers[1];

        _.forOwn(scope.scValidate(), function (validationFn, validationErrorKey) {
          var validator = function (value) {
            var isValid = validationFn(value);

            // check if the result is promise
            if (isValid.then && typeof (isValid.then) === 'function') {
              control.$setValidity(validationErrorKey, false);
              addPendingValidation(control);
              addPendingValidation(form);

              isValid.then(function (result) {
                control.$setValidity(validationErrorKey, result);

                removePendingValidation(control);
                removePendingValidation(form);
              }, function () {
                removePendingValidation(control);
                removePendingValidation(form);
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