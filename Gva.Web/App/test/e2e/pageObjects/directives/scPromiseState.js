/*global module*/
(function (module) {
  'use strict';

  function ScPromiseState(element) {
    this.element = element;
  }

  ScPromiseState.prototype.title = function () {
    return this.element.getAttribute('title');
  };

  ScPromiseState.prototype.isActive = function () {
    return this.element.isDisplayed();
  };

  ScPromiseState.prototype.isPending = function () {
    return this.element.getAttribute('class').then(function (className) {
      return className.split(' ').indexOf('glyphicon-loading') !== -1;
    });
  };

  ScPromiseState.prototype.isResolved = function () {
    return this.element.getAttribute('class').then(function (className) {
      return className.split(' ').indexOf('glyphicon-ok') !== -1;
    });
  };

  ScPromiseState.prototype.isRejected = function () {
    return this.element.getAttribute('class').then(function (className) {
      return className.split(' ').indexOf('glyphicon-ban-circle') !== -1;
    });
  };

  module.exports = ScPromiseState;
}(module));