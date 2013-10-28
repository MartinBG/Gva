// Usage: <sc-button action="" class="" text="" icon=""></sc-button>
(function (angular) {
  'use strict';

  function ButtonDirective($parse) {
    function ButtonCompile (tElement) {
      var btnClasses = tElement.attr('class'),
          buttons = tElement.find('button');

      if (!btnClasses) {
        btnClasses = 'btn-sm btn-default';
      }

      buttons.each(function (index, button) {
        button.className = btnClasses;
      });

      tElement.removeClass();
      tElement.addClass('btn-div');

      return ButtonLink;
    }

    function ButtonLink($scope, element, attrs, scSearch) {
      if (!scSearch) {
        return;
      }

      $scope.addFilter = undefined;
      $scope.nonSelectedFilters = undefined;
      $scope.btnAction = undefined;
      $scope.text = undefined;

      if ($scope.action === 'add') {
        $scope.nonSelectedFilters = scSearch.nonSelectedFilters;
        $scope.addFilter = function (filter) {
          scSearch.selectedFilters[filter.name] = null;
        };
      }
      else {
        $scope.text = $scope.$root.l10n.get(attrs.text);
        $scope.btnAction = $parse(attrs.action)($scope.$parent);
      }
    }

    return {
      restrict: 'E',
      require: '?^scSearch',
      replace: true,
      scope: {
        action: '@',
        icon: '@'
      },
      templateUrl: 'scaffolding/templates/buttonTemplate.html',
      compile: ButtonCompile
    };
  }

  ButtonDirective.$inject = ['$parse'];

  angular.module('scaffolding').directive('scButton', ButtonDirective);
}(angular));