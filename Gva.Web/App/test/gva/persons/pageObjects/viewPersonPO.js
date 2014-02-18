/*global module, by*/
(function (module, by) {
  'use strict';

  function ViewPersonPO(context) {
    this.name = context.findElement(by.name('names'));
    this.company = context.findElement(by.name('organization'));
    this.emplCategory = context.findElement(by.name('employment'));
    this.uin = context.findElement(by.name('uin'));
    this.lin = context.findElement(by.name('lin'));
    this.age = context.findElement(by.name('age'));

    this.editBtn = context.findElement(by.name('editBtn'));
  }

  ViewPersonPO.prototype.edit = function () {
    this.editBtn.click();
  };

  module.exports = ViewPersonPO;
}(module, by));