// Usage: <sc-tabs tab-list="<object>"></sc-tabs>

/*global angular, _*/
(function (angular, _) {
  'use strict';

  function TabsDirective ($state, $stateParams) {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'scaffolding/directives/tabs/tabsDirective.html',
      scope: {
        tabList: '&'
      },
      link: function ($scope) {
        var tabsObject = $scope.tabList();

        $scope.tabList = [];
        $scope.secondTabList = [];

        angular.forEach(_.keys(tabsObject), function (tabTitle) {
          var newTab = { isActive: false, title: tabTitle, className: tabTitle.replace(' ', '-') },
              tab = tabsObject[tabTitle];

          if (_.isString(tab)) {
            newTab.isState = true;
            newTab.name = $state.get(tab).name;
          }
          else {
            if (!!tab.state && !!tab.stateParams) {
              newTab.isState = true;
              newTab.name = $state.get(tab.state).name;
              newTab.stateParams = tab.stateParams;
            }
            else {
              newTab.isState = false;
              newTab.children = [];

              angular.forEach(_.keys(tab), function (childTabTitle) {
                var childTab = $state.get(tab[childTabTitle]);
                newTab.children.push({
                  title: childTabTitle,
                  isActive: false,
                  isState: true,
                  name: childTab.name,
                  className: childTabTitle.replace(' ', '-')
                });
              });
            }
          }

          $scope.tabList.push(newTab);
        });

        $scope.$on('$stateChangeStart', function (event, toState) {
          activateTab(toState.name, true);
        });

        $scope.$on('$stateChangeSuccess', function(event, toState){
          stopLoader(toState.name);
        });

        $scope.openTab = function (newSection) {
          if (newSection.isActive) {
            return;
          }

          if (newSection.isState) {
            if (!!newSection.stateParams) {
              var newStateParams = _.assign(_.cloneDeep($stateParams), newSection.stateParams);
              $state.go(newSection.name, newStateParams);
            }
            else {
              $state.go(newSection.name);
            }
          }
          else {
            $state.go(newSection.children[0].name);
          }
        };

        function stopLoader(tabName) {
          var tab;

          for (var i = 0; i < $scope.tabList.length; i++) {
            tab = $scope.tabList[i];

            if (tab.isState && _(tabName).include(tab.name)) {

              if (tab.hasOwnProperty('stateParams')){
                if (stateMatch(tab.stateParams)) {
                  tab.loading = false;
                  return;
                }
              }
              else {
                tab.loading = false;
                return;
              }
            }
          }

          for (var j = 0; j < $scope.secondTabList.length; j++) {
            tab = $scope.secondTabList[j];

            if (tab.isState && _(tabName).include(tab.name)) {
              tab.loading = false;
              return;
            }
          }
        }

        function activateTab(tabName, loading) {
          for (var i = 0; i < $scope.tabList.length; i++) {
            var tab = $scope.tabList[i];

            if (tab.isState) {
              if (!_(tabName).include(tab.name)) {
                continue;
              }

              if (tab.hasOwnProperty('stateParams') && !stateMatch(tab.stateParams)) {
                continue;
              }

              selectTab($scope.tabList, tab);
              tab.loading = loading;

              $scope.secondTabList = [];
              return;
            }
            else {
              for (var j = 0; j < tab.children.length; j++) {
                var childTab = tab.children[j];

                if (_(tabName).include(childTab.name)) {
                  selectTab($scope.tabList, tab);
                  $scope.secondTabList = tab.children;
                  selectTab($scope.secondTabList, childTab);
                  childTab.loading = loading;
                  return;
                }
              }
            }
          }
        }

        function selectTab (tabList, tab) {
          angular.forEach(tabList, function (tab) {
            tab.isActive = false;
          });
          tab.isActive = true;
        }

        function stateMatch (tabStateParams) {
          return _.pairs(tabStateParams).reduce(function(stateMatching, kvp) {
            return stateMatching &&
              $stateParams.hasOwnProperty(kvp[0]) &&
              $stateParams[kvp[0]] === kvp[1];
          }, true);
        }

        activateTab($state.$current.name, false);
      }
    };
  }

  TabsDirective.$inject = ['$state', '$stateParams'];

  angular.module('scaffolding').directive('scTabs', TabsDirective);
}(angular, _));
