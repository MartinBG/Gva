// Uage: <sc-search selected-filters="" btn-classes=""></sc-search>
(function (angular) {
  'use strict';

  function SearchDirective() {
    function SearchController ($scope) {
      var filters = $scope.filters = {};

      this.selectedFilters = $scope.selectedFilters;
      this.nonSelectedFilters = $scope.nonSelectedFilters = [];

      this.registerFilter = function (name, filterScope) {
        filters[name] = filterScope;
      };
    }

    function SearchCompile(tElement, tAttrs, transcludeFn) {
      if (!tAttrs.btnClasses) {
        tAttrs.btnClasses = 'col-sm-3';
      }

      return function ($scope, element) {
        $scope.$watch('selectedFilters', function (newObj, oldObj) {
          for (var propt in newObj) {
            if (newObj.hasOwnProperty(propt) && newObj[propt] !== oldObj[propt]) {
              $scope.filters[propt].model = newObj[propt];
            }
          }

          $scope.nonSelectedFilters.length = 0;
          for (var name in $scope.filters) {
            if ($scope.filters.hasOwnProperty(name) &&
                !(name in newObj) &&
                $scope.filters[name].label) {
              $scope.nonSelectedFilters.push({
                label: $scope.filters[name].label,
                name: name
              });
            }
          }
        }, true);

        transcludeFn($scope.$parent, function (clone) {
          var rowBlock = element.find('div.row');
          var buttonBlock = element.find('div.btns-block');
          var transcludedElements = clone;
          angular.forEach(transcludedElements, function (elem) {
            if (angular.element(elem).hasClass('btn-div')) {
              buttonBlock.append(elem);
            } else {
              rowBlock.append(elem);
            }
          });
        });
      };
    }

    return {
      restrict: 'E',
      transclude: true,
      replace: true,
      templateUrl: 'scaffolding/templates/searchTemplate.html',
      scope: {
        selectedFilters: '=',
        btnClasses: '@'
      },
      controller: ['$scope', SearchController],
      compile: SearchCompile
    };
  }

  angular.module('scaffolding').directive('scSearch', SearchDirective);
}(angular));