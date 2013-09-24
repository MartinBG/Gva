(function (angular) {
  'use strict';

  function NavbarConfigProvider() {
    this.items = [];
  }
  
  NavbarConfigProvider.prototype.$get = function () {
    return {
      items: this.items,
      userFullName: this.userFullName,
      userHasPassword: this.userHasPassword
    };
  };
  
  NavbarConfigProvider.prototype.addItem = function (item) {
    this.items.push(item);
    return this;
  };
  
  NavbarConfigProvider.prototype.setUserFullName = function (userFullName) {
    this.userFullName = userFullName;
    return this;
  };
  NavbarConfigProvider.prototype.setUserHasPassword = function (userHasPassword) {
    this.userHasPassword = userHasPassword;
    return this;
  };
  
  angular.module('navigation').provider('navigation.NavbarConfig', NavbarConfigProvider);
}(angular));