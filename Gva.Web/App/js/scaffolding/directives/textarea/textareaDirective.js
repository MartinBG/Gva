// Usage: <sc-textarea 
//          ng-model="<model_name>"
//          rows="<number_of_rows>"
//          cols="<number_of_cols>">
//        </sc-textarea>

/*global angular*/
(function (angular) {
  'use strict';

  function TextareaDirective() {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      template: '<textarea></textarea>'
    };
  }

  angular.module('scaffolding').directive('scTextarea', TextareaDirective);
}(angular));