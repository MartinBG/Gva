/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var PersonDataPO = require('./personDataPO'),
      PersonAddressPO = require('./addresses/addressPO'),
      PersonDocumentIdPO = require('./documentIds/documentIdPO');

  function NewPersonPO(context) {
    
    this.personAddress = new PersonAddressPO(context);
    this.breadcrumb = this.personAddress.breadcrumb;
    this.personDocumentId = new PersonDocumentIdPO(context);
    this.personData = new PersonDataPO(context);
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  NewPersonPO.prototype.save = function () {
    this.saveBtn.click();
  };

  NewPersonPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = NewPersonPO;
}(module, by, require));
