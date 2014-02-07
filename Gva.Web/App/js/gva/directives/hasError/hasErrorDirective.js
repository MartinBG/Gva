// Usage: <div gva-has-error="fieldName"></div>
/*global angular*/
(function (angular) {
  'use strict';

  function HasErrorDirective() {

    return {
      restrict: 'A',
      link: function (scope, element, attrs) {
        scope.$watchCollection('[form["' + attrs.gvaHasError + '"].$invalid, form.$validated]',
          function (newValue, oldValue) {
            if (newValue === oldValue) {
              return;
            }
            if (newValue[0] && newValue[1]) {
              element.addClass('has-error');
            }
            else {
              element.removeClass('has-error');
            }
          }
        );
      }
    };
  }

  angular.module('gva')
    .directive('gvaHasError', HasErrorDirective);
}(angular));