// Usage: <sc-search selected-filters="" btn-classes=""></sc-search>

/*globals angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function SearchDirective($timeout, $parse) {
    function SearchController ($scope, $state, $window, scMessage) {
      var filters = {},
          //an object used as the special value of the watch expression
          //when the watched property does not exist on the object
          emptyValue = {};

      this.selectedFilters = $scope.selectedFilters;
      this.dropdownFilters = $scope.dropdownFilters = [];
      this.defaultAction = $scope.defaultAction;

      this.registerFilter = function (name, filterScope) {
        filters[name] = filterScope;
      };

      if ($window.localStorage.getItem($state.current.name)) {
        $scope.savedFiltersSets = JSON.parse($window.localStorage.getItem($state.current.name));
      } else {
        $scope.savedFiltersSets = [];
      }

      this.saveFiltersSet = function (name) {
        $scope.savedFiltersSets.push({
          name: name,
          data: $scope.selectedFilters
        });
        $window.localStorage.setItem(
          $state.current.name,
          JSON.stringify($scope.savedFiltersSets));
      };

      $scope.setFiltersData = function (data) {
        _.assign($scope.selectedFilters, data);
        $scope.defaultAction($scope.$parent);
      };

      $scope.removeFiltersSet = function (set) {
        return scMessage('scaffolding.scSearch.confirmDelete')
          .then(function (result) {
            if (result === 'OK') {
              $scope.savedFiltersSets = _.without($scope.savedFiltersSets, set);
              $window.localStorage.setItem(
                $state.current.name,
                JSON.stringify($scope.savedFiltersSets));
            }
          });
      };

      this.initialize = function () {
        _.forOwn(filters, function (filterScope, filterName) {
          var dropdownFilter = {
            label: filterScope.label,
            name: filterName,
            visible: false
          };
          $scope.dropdownFilters.push(dropdownFilter);

          $scope.$watch(
            function () {
              return $scope.selectedFilters.hasOwnProperty(filterName) ?
                $scope.selectedFilters[filterName] :
                emptyValue;
            },
            function (newVal) {
              filterScope.model = newVal === emptyValue ? null : newVal;
              dropdownFilter.visible = filterScope.label && newVal === emptyValue;
            },
            true);

          filterScope.$watch('model', function (newVal, oldVal) {
            if (newVal !== oldVal &&
              $scope.selectedFilters.hasOwnProperty(filterName)
            ) {
              $scope.selectedFilters[filterName] = newVal;
            }
          }, true);
        });
      };
    }

    function SearchCompile(tElement, tAttrs) {
      if (!tAttrs.btnClasses) {
        tAttrs.btnClasses = 'col-sm-3';
      }

      return function ($scope, element, attrs, scSearch, transcludeFn) {

        transcludeFn($scope.$parent, function (clone) {
          var buttonBlock = element.find('div.btns-block');

          angular.forEach(clone, function (elem) {
            if (angular.element(elem).hasClass('btn-div')) {
              buttonBlock.append(elem);
            } else {
              buttonBlock.before(elem);
            }
          });

          $scope.$on('$destroy', function () {
            clone.remove();
          });
        });

        if (attrs.defaultAction) {
          $scope.defaultAction = $parse(attrs.defaultAction);
          element.bind('keypress', function (event) {
            if(event.keyCode === 13) {
              $scope.defaultAction($scope.$parent);
            }
          });
        }

        scSearch.initialize();

        //delaying the focus, because using a templateUrl leads to
        //an asynchronous resolve which delays the inner directives' linking
        $timeout(function () {
          if ($scope.selectedFilters !== null) {
            $('form[selected-filters] input').first().focus();
          }
        },
        0);
      };
    }

    return {
      restrict: 'E',
      transclude: true,
      replace: true,
      templateUrl: 'js/scaffolding/directives/search/searchDirective.html',
      scope: {
        selectedFilters: '=',
        btnClasses: '@',
        defaultAction: '&'
      },
      controller: ['$scope', '$state', '$window', 'scMessage', SearchController],
      compile: SearchCompile
    };
  }

  SearchDirective.inject = ['$timeout', '$parse'];

  angular.module('scaffolding').directive('scSearch', SearchDirective);
}(angular, _, $));
