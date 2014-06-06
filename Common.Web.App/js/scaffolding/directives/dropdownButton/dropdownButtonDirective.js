/* Usage:
  <sc-dropdown-button label="l10nText"
      options="[
          {
            text: l10nText1
            action: actionFn,
            visible: expr1
          },
          {
            text: l10nText2,
            state: stateName
            params: {key: value, ...},
            visible: expr2
          }
      ]">
  </sc-dropdown-button>
*/
/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DropdownButtonDirective($parse, l10n) {

    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'js/scaffolding/directives/dropdownButton/dropdownButtonDirective.html',
      scope: {
        icon: '@'
      },
      link: function (scope, element, attrs) {
        scope.label = l10n.get(attrs.label);
        scope.buttons =  _.map($parse(attrs.options)(scope.$parent),
          function(button){
            return {
              action: button.action,
              state: button.state,
              text: l10n.get(button.text),
              params: button.params,
              visible: button.visible !== undefined ? button.visible : true,
              btnClass: button.btnClass
            };
          });
      }
    };
  }

  DropdownButtonDirective.$inject = ['$parse', 'l10n'];

  angular.module('scaffolding').directive('scDropdownButton', DropdownButtonDirective);
}(angular, _));
