// Usage: <sc-link-button name="" sc-sref="{state: '...', params: {...}}" text="" class="" icon="">
//</sc-link-button>

/*global angular*/
(function (angular) {
  'use strict';

  function LinkButtonDirective($parse, l10n) {

    return {
      restrict: 'E',
      priority: 110,
      scope:{
        icon: '@'
      },
      replace: true,
      templateUrl: 'js/scaffolding/directives/linkButton/linkButtonDirective.html',
      link: function (scope, element, attrs) {
        var elementCtrl = {};

        scope.$parent[attrs.name] = elementCtrl;

        scope.text = l10n.get(attrs.text);
        if (!scope.text) {
          scope.text = attrs.text;
        }
      }
    };
  }

  LinkButtonDirective.$inject = ['$parse','l10n'];

  angular.module('scaffolding').directive('scLinkButton', LinkButtonDirective);
}(angular));