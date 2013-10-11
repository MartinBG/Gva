// Usage: <sc-button action-name="" classes="" text="" icon=""></sc-button>
(function (angular) {
  'use strict';

  function ButtonDirective() {
    function ButtonCompile (tElement, tAttrs) {
      if (!tAttrs.classes) {
        tAttrs.classes = 'btn-sm btn-default';
      }

      return ButtonLink;
    }

    function ButtonLink($scope, element, attrs, controllers) {
      var scSearch = controllers[0],
          scBtnWrapper = controllers[1];

      if (!scSearch || !scBtnWrapper) {
        return;
      }

      $scope.action = undefined;
      $scope.addFilter = undefined;
      $scope.nonSelectedFilters = [];

      if ($scope.actionName === 'add') {
        $scope.nonSelectedFilters = scSearch.getNonSelectedFilters();
        $scope.addFilter = function (filter) {
          scSearch.addFilter(filter);
        };
      }
      else {
        $scope.action = scBtnWrapper.getBtnAction($scope.actionName);
      }
    }

    return {
      restrict: 'E',
      require: ['?^scSearch', '?^scBtnWrapper'],
      replace: true,
      scope: {
        actionName: '@',
        classes: '@',
        text: '@',
        icon: '@'
      },
      templateUrl: 'scaffolding/templates/buttonTemplate.html',
      compile: ButtonCompile
    };
  }

  angular.module('scaffolding').directive('scButton', ButtonDirective);
}(angular));