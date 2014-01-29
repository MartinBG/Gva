﻿/*global angular*/
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
      showBreadcrumbBar: this.showBreadcrumbBar,
      breadcrumbBarHomeState: this.breadcrumbBarHomeState
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

  NavigationConfigProvider.prototype.showBreadcrumbBar = function (showBreadcrumbBar) {
    this.showBreadcrumbBar = showBreadcrumbBar;
    return this;
  };

  NavigationConfigProvider.prototype.setBreadcrumbBarHomeState = function (breadcrumbBarHomeState) {
    this.breadcrumbBarHomeState = breadcrumbBarHomeState;
    return this;
  };

  angular.module('common').provider('NavigationConfig', NavigationConfigProvider);
}(angular));