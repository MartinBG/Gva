// Usage: <gva-tabs tab-list="<object>"></gva-tabs>

/*global angular, _*/
(function (angular, _) {
  'use strict';

  function TabsDirective ($state) {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'gva/directives/tabs/tabsDirective.html',
      scope: {
        tabList: '&'
      },
      link: function ($scope) {
        var tabsObject = $scope.tabList();

        $scope.tabList = [];
        $scope.secondTabList = [];

        angular.forEach(_.keys(tabsObject), function (tabTitle) {
          var newTab = { isActive: false, title: tabTitle },
              tab = tabsObject[tabTitle];

          if (_.isString(tab)) {
            var tabState = $state.get(tab);
            newTab.isState = true;
            newTab.name = tabState.name;
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
                name: childTab.name
              });
            });
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
          if (newSection.isState) {
            $state.go(newSection.name);
          }
          else {
            $state.go(newSection.children[0].name);
          }
        };

        function stopLoader(tabName) {
          var tab;

          for (var i = 0; i < $scope.tabList.length; i++) {
            tab = $scope.tabList[i];

            if (tab.isState && tab.name === tabName) {
              tab.loading = false;
              return;
            }
          }

          for (var j = 0; j < $scope.secondTabList.length; j++) {
            tab = $scope.secondTabList[j];

            if (tab.isState && tab.name === tabName) {
              tab.loading = false;
              return;
            }
          }
        }

        function activateTab(tabName, loading) {
          for (var i = 0; i < $scope.tabList.length; i++) {
            var tab = $scope.tabList[i];

            if (tab.isState) {
              if (tab.name !== tabName) {
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

                if (childTab.name === tabName) {
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

        activateTab($state.$current.name, false);
      }
    };
  }

  TabsDirective.$inject = ['$state'];

  angular.module('gva').directive('gvaTabs', TabsDirective);
}(angular, _));