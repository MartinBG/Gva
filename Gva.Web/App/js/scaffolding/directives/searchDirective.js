// Uage: <sc-search selected-filters="" btn-actions=""></sc-search>
(function (angular) {
  'use strict';

  function SearchDirective() {
    return {
      restrict: 'E',
      transclude: true,
      replace: true,
      templateUrl: 'scaffolding/templates/searchTemplate.html',
      scope: {
        selectedFilters: '=',
        btnActions: '=',
        btnClasses: '@'
      },
      controller: function ($scope) {
        this.filters = {};
        this.btnActions = $scope.btnActions;
        this.selectedFilters = $scope.selectedFilters;
        this.nonSelectedFilters = undefined;
        this.btnClasses = $scope.btnClasses || 'col-sm-3';

        $scope.$watch('selectedFilters', function (newObj, oldObj) {
          for (var propt in oldObj) {
            if (newObj[propt] !== oldObj[propt]) {
              this.filters[propt].model = newObj[propt];
            }
          }
        }, true);

        this.addFilter = function (modelName, filter) {
          this.filters[modelName] = filter;
        };
      }
    };
  }

  angular.module('scaffolding').directive('scSearch', SearchDirective);
}(angular));