/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScPromiseState = require('../../../pageObjects/directives/scPromiseState');

  function PromiseState(context) {
    this.promiseStateDir = new ScPromiseState(context.findElement(by.id('promiseStateDir')));
    this.createBtn = context.findElement(by.id('createBtn'));
    this.resolveBtn = context.findElement(by.id('resolveBtn'));
    this.rejectBtn = context.findElement(by.id('rejectBtn'));
    this.destroyBtn = context.findElement(by.id('destroyBtn'));
    this.createResolvedBtn = context.findElement(by.id('createResolvedBtn'));
  }

  PromiseState.prototype.create = function () {
    return this.createBtn.click();
  };

  PromiseState.prototype.resolve = function () {
    return this.resolveBtn.click();
  };

  PromiseState.prototype.reject = function () {
    return this.rejectBtn.click();
  };

  PromiseState.prototype.destroy = function () {
    return this.destroyBtn.click();
  };

  PromiseState.prototype.createResolved = function () {
    return this.createResolvedBtn.click();
  };

  module.exports = PromiseState;
}(module, by, require));