/* Usage:
  <sc-info class="active|success|info|warning|danger|muted"
		icon="glyphicon glyphicon-remove"
		text="label">
	<sc-info>
*/
/*global angular*/
(function (angular) {
  'use strict';

  function InfoDirective(l10n) {
    
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'js/scaffolding/directives/info/infoDirective.html',
      scope: {
        icon: '@'
      },
      link: function (scope, element, attrs) {
        scope.text = l10n.get(attrs.text);
        scope.class = 'text-' + attrs.class.match(/active|success|info|warning|danger|muted/);
      }
    };
  }

  InfoDirective.$inject = ['l10n'];

  angular.module('scaffolding').directive('scInfo', InfoDirective);
}(angular));
