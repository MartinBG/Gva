﻿/* Usage:
  <sc-dropdown-simple 
    items-source="dropdownModel"
    selected-item="selectedItem" 
    selected-value="selectedValue"
    is-disabled="isEditMode"
    item-text-field="name"
    item-value-field="value">
  </sc-dropdown-simple>>
*/

// From outside select it only trough Selected Item, not trough Value

/*global angular*/
(function (angular) {
  'use strict';

  function DropdownSimpleDirective() {

    return {
      restrict: 'E',
      replace: true,
      templateUrl: 'js/scaffolding/directives/dropdownSimple/dropdownSimpleDirective.html',
      scope: {
        selectedItem: '=',
        selectedValue: '=',
        itemsSource: '&',
        isDisabled: '&',
        itemTextField: '@',
        itemValueField: '@'
      },
      link: function (scope) {
        scope.selectItem = function (item) {
          scope.selectedItem = item;
          scope.selectedValue = item[scope.itemValueField];
        };
      }
    };
  }

  DropdownSimpleDirective.$inject = [];

  angular.module('scaffolding').directive('scDropdownSimple', DropdownSimpleDirective);
}(angular));
