/*global module, by, require*/
( function (module, by, require) {
  'use strict';

  var ScSearch = require('../../../pageObjects/directives/scSearch'),
      Q = require('q'),
      _ = require('lodash');

  function SearchPO( context ) {
    this.context = context;

    this.searchForm = new ScSearch(context.findElement(by.css('form[name=search]')), context);
  }

  SearchPO.prototype.getNumClicks = function (btnName) {
    return this.context.findElement(by.name(btnName)).then(function (label) {
      return label.getText().then(function (text) {
        return parseInt(text, 10);
      });
    });
  };

  SearchPO.prototype.getVisibleInputs = function () {
    return this.context.findElements(by.css('form[name=modelForm] div.form-group > input'))
          .then(function (inputs) {
      return Q.all(_.map(inputs, function (input) {
        return input.getAttribute('name');
      }));
    });
  };

  SearchPO.prototype.getInput = function ( inputName ) {
    return this.context.findElement(by.name(inputName)).then(function(input) {
      return input.getAttribute('value');
    });
  };

  module.exports = SearchPO;
}(module, by, require));