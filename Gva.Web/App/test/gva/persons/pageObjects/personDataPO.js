/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScText = require('../../../scaffolding/pageObjects/scText'),
      ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature'),
      ScDate = require('../../../scaffolding/pageObjects/scDate');

  function PersonDataPO(context) {
    
    this.firstName = new ScText(context.findElement(by.input('model.firstName')));
    this.middleName = new ScText(context.findElement(by.input('model.middleName')));
    this.lastName = new ScText(context.findElement(by.input('model.lastName')));
    this.firstNameAlt = new ScText(context.findElement(by.input('model.firstNameAlt')));
    this.middleNameAlt = new ScText(context.findElement(by.input('model.middleNameAlt')));
    this.lastNameAlt = new ScText(context.findElement(by.input('model.lastNameAlt')));
    this.lin = new ScText(context.findElement(by.input('model.lin')));
    this.uin = new ScText(context.findElement(by.input('model.uin')));
    this.sex = new ScNomenclature('model.sex', context);
    this.dateOfBirth = new ScDate(context.findElement(by.css('div[name=dateOfBirth]')), context);
    this.placeOfBirth = new ScNomenclature('model.placeOfBirth', context);
    this.country = new ScNomenclature('model.country', context);
    this.email = new ScText(context.findElement(by.input('model.email')));
    this.fax = new ScText(context.findElement(by.input('model.fax')));
    this.phone1 = new ScText(context.findElement(by.input('model.phone1')));
    this.phone2 = new ScText(context.findElement(by.input('model.phone2')));
    this.phone3 = new ScText(context.findElement(by.input('model.phone3')));
    this.phone4 = new ScText(context.findElement(by.input('model.phone4')));
    this.phone5 = new ScText(context.findElement(by.input('model.phone5')));

    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  PersonDataPO.prototype.save = function () {
    this.saveBtn.click();
  };

  PersonDataPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = PersonDataPO;
}(module, by, require));