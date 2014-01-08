/*global angular*/
(function (angular) {
  'use strict';

  function NavbarCtrl($scope, $state, navigationConfig) {
    function mapItems(items) {
      return items.filter(function (item) {
        return (item.permissions || []).reduce(function (hasPermissions, permission) {
          permission = permission;
          return hasPermissions && true;
        }, true);
      }).map(function (item) {
        var newItem = {
          active: false,
          text: item.text,
          icon: item.icon,
          newTab: item.newTab,
          items: item.items ? mapItems(item.items) : []
        };

        if (item.state) {
          newItem.url = $state.href(item.state, item.stateParams);
        } else {
          newItem.url = item.url;
        }

        return newItem;
      });
    }

    $scope.items = mapItems(navigationConfig.items);
    $scope.userFullName = navigationConfig.userFullName;
    $scope.userHasPassword = navigationConfig.userHasPassword;

  }
  NavbarCtrl.$inject = [ '$scope', '$state', 'NavigationConfig'];

  angular.module('navigation').controller('NavbarCtrl', NavbarCtrl);
}(angular));
