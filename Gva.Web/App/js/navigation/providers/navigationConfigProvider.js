/*global angular*/
(function (angular) {
  'use strict';

  function NavigationConfigProvider() {
    this.items = [];
  }

  NavigationConfigProvider.prototype.$get = function () {
    return {
      items: this.items,
      userFullName: this.userFullName,
      userHasPassword: this.userHasPassword,
      showBreadcrumbBar: this.showBreadcrumbBar
    };
  };

  NavigationConfigProvider.prototype.addItem = function (item) {
    this.items.push(item);
    return this;
  };

  NavigationConfigProvider.prototype.setUserFullName = function (userFullName) {
    this.userFullName = userFullName;
    return this;
  };
  NavigationConfigProvider.prototype.setUserHasPassword = function (userHasPassword) {
    this.userHasPassword = userHasPassword;
    return this;
  };

  NavigationConfigProvider.prototype.showBreadcrumbBar = function (showBreadcrumbBar ) {
    this.showBreadcrumbBar = showBreadcrumbBar;
    return this;
  };

  angular.module('navigation').provider('NavigationConfig', NavigationConfigProvider);
}(angular));