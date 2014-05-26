// Usage: <sc-tabs tab-list="<object>"></sc-tabs>

/*global angular, _*/
(function (angular, _) {
  'use strict';

  function TabsDirective($state, $stateParams, $exceptionHandler, l10n) {
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      templateUrl: 'js/scaffolding/directives/tabs/tabsDirective.html',
      scope: {
        tabList: '&'
      },
      link: function ($scope, iElem, iAttrs) {
        var tabsObject = $scope.tabList(),
          loading = false;

        iAttrs.$observe('disabled', function (value) {
          $scope.isDisabled = !!value;
        });

        $scope.tabList = [];
        $scope.secondTabList = [];

        angular.forEach(_.keys(tabsObject), function (tabTitle) {
          var newTab = {
            isActive: false,
            title: l10n.get(tabTitle) || tabTitle,
            className: tabTitle.replace(' ', '-')
          },
              tab = tabsObject[tabTitle];

          if (_.isString(tab)) {
            newTab.isState = true;
            newTab.name = $state.get(tab).name;
            newTab.state = $state.getWrapper(tab)['abstract'] ?
              $state.getWrapper(tab).defaultChild : $state.getWrapper(tab);
          }
          else {
            if (!!tab.state && !!tab.stateParams) {
              newTab.isState = true;
              newTab.name = $state.get(tab.state).name;
              newTab.state = $state.getWrapper(tab.state)['abstract'] ?
                $state.getWrapper(tab.state).defaultChild :
                $state.getWrapper(tab.state);
              newTab.stateParams = tab.stateParams;
            }
            else {
              newTab.isState = false;
              newTab.children = [];

              angular.forEach(_.keys(tab), function (childTabTitle) {
                var childTab = $state.get(tab[childTabTitle]);
                newTab.children.push({
                  parent: newTab,
                  title: l10n.get(childTabTitle) || childTabTitle,
                  isActive: false,
                  isState: true,
                  name: childTab.name,
                  state: $state.getWrapper(childTab.name)['abstract'] ?
                    $state.getWrapper(childTab.name).defaultChild :
                    $state.getWrapper(childTab.name),
                  className: childTabTitle.replace(' ', '-')
                });
              });
            }
          }

          $scope.tabList.push(newTab);
        });

        $scope.$on('$stateChangeStart', function (event, toState) {
          if (!loading) {
            activateTab(toState.name);
          }
          loading = false;
        });

        $scope.$on('$stateChangeSuccess', function (event, toState) {
          stopLoader(toState.name);
        });


        $scope.openTab = function (newSection) {
          loading = true;
          if ($scope.isDisabled) {
            loading = false;
            return;
          }
          if (newSection.isActive) {
            loading = false;
            return;
          }

          if (newSection.isState) {
            if (!!newSection.stateParams) {
              var newStateParams = _.assign(_.cloneDeep($stateParams), newSection.stateParams);
              selectTab($scope.tabList, newSection, true);
              $state.go(newSection.state, newStateParams)['catch'](function (error) {
                $exceptionHandler(error);
              });
            }
            else {
              if (newSection.parent) {
                selectTab($scope.secondTabList, newSection, true);
                $state.go(newSection.state)['catch'](function (error) {
                  $exceptionHandler(error);
                });
              } else {
                $scope.secondTabList = [];
                selectTab($scope.tabList, newSection, true);
                $state.go(newSection.state)['catch'](function (error) {
                  $exceptionHandler(error);
                });
              }
            }
          }
          else {
            selectTab($scope.tabList, newSection, true);
            $scope.secondTabList = newSection.children;
            selectTab($scope.secondTabList, newSection.children[0], true);
            return $state.go(newSection.children[0].state)['catch'](function (error) {
              $exceptionHandler(error);
            });
          }
        };

        function stopLoader(tabName) {
          var tab;

          for (var i = 0; i < $scope.tabList.length; i++) {
            tab = $scope.tabList[i];

            if (tab.isState && _(tabName).include(tab.name)) {
              if (tab.hasOwnProperty('stateParams')) {
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
              tab.parent.loading = false;
              return;
            }
          }
          closeTabs();
        }

        function activateTab(tabName) {
          for (var i = 0; i < $scope.tabList.length; i++) {
            var tab = $scope.tabList[i];

            if (tab.isState) {
              if (!_(tabName).include(tab.name)) {
                continue;
              }

              if (tab.hasOwnProperty('stateParams') && !stateMatch(tab.stateParams)) {
                continue;
              }

              selectTab($scope.tabList, tab, false);

              $scope.secondTabList = [];
              return;
            }
            else {
              for (var j = 0; j < tab.children.length; j++) {
                var childTab = tab.children[j];

                if (_(tabName).include(childTab.name)) {
                  selectTab($scope.tabList, tab, false);
                  $scope.secondTabList = tab.children;
                  selectTab($scope.secondTabList, childTab, false);
                  return;
                }
              }
            }
          }
          closeTabs();
        }

        function closeTabs() {
          $scope.secondTabList = [];
          angular.forEach($scope.tabList, function (tab) {
            tab.isActive = false;
          });
        }

        function selectTab(tabList, tab, loading) {
          angular.forEach(tabList, function (tab) {
            tab.isActive = false;
          });
          tab.loading = loading;
          tab.isActive = true;
        }

        function stateMatch(tabStateParams) {
          return _.pairs(tabStateParams).reduce(function (stateMatching, kvp) {
            var param = kvp[0],
                value = kvp[1],
                paramUndefined = !value && !$stateParams[param],
                paramPresent = $stateParams.hasOwnProperty(param) &&
                  $stateParams[param] === value;
            return stateMatching && (paramUndefined || paramPresent);
          }, true);
        }

        activateTab($state.$current.name, false);
      }
    };
  }

  TabsDirective.$inject = ['$state', '$stateParams', '$exceptionHandler', 'l10n'];

  angular.module('scaffolding').directive('scTabs', TabsDirective);
}(angular, _));
