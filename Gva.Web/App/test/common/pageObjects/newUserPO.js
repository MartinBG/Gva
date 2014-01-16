/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var ScText = require('../../scaffolding/pageObjects/scText'),
      ScTextarea = require('../../scaffolding/pageObjects/scTextarea');

  function NewUserPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));

    this.username = new ScText(context.findElement(by.input('user.username')));
    this.usernameExistsError = context.findElement(by.id('usernameExists'));
    this.usernameInvalidError = context.findElement(by.id('usernameInvalid'));

    this.fullname = new ScText(context.findElement(by.input('user.fullname')));

    this.notes = new ScTextarea(context.findElement(by.tagName('textarea')));

    this.hasPassword = context.findElement(by.input('setPassword'));
    this.password = context.findElement(by.input('password'));
    this.passInvalidError = context.findElement(by.id('passInvalid'));
    this.confirmPassword = context.findElement(by.input('confirmPassword'));
    this.confirmPassInvalidError = context.findElement(by.id('confirmPassInvalid'));

    this.hasCertificate = context.findElement(by.input('setCertificate'));
    this.certificate = new ScText(context.findElement(by.input('certificate')));
    this.certificateInvalidError = context.findElement(by.id('certificateInvalid'));

    this.isActive = context.findElement(by.input('user.isActive'));

    this.saveBtn = context.findElement(by.id('saveBtn'));
    this.cancelBtn = context.findElement(by.id('cancelBtn'));
  }

  NewUserPO.prototype.save = function () {
    this.saveBtn.click();
  };

  NewUserPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = NewUserPO;
}(module, by, require));