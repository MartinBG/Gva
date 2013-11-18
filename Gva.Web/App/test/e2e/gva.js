/*global exports, require, protractor, exports*/
(function(protractor){
  'use strict';
  function createSelector (model, index, data) {
    var selector ='';
    if(model) {
      selector += 'div[ng-model=' + model + '] table.datatable';
    } else {
      selector += 'div table.datatable';
    }
    if(index || data){
      selector += ' tbody';
      if(index) {
        selector += ' tr:nth-child(' + index +')';
      }
      if(data) {
        if(!index) {
          selector += ' tr td[data*=' + data + ']';
        }
        else {
          selector += ' td[data*=' + data + ']';
        }
      }
    }
    return selector;
  }

  var util = require('util'),
    GvaBy = {},
    ProtractorBy = function() {};

  ProtractorBy.prototype = protractor.By;
  util.inherits(GvaBy, ProtractorBy);

  GvaBy.datatable = function(model) {
    return {
      findOverride: function(driver) {
        return driver.findElement(protractor.By.css('div[ng-model=' + model + '] table.datatable'));
      },
      findArrayOverride: function(driver) {
        return driver.findElements(protractor.By.css('table[class*=datatable]'));
      },
      row: function (index) {
        return {
          findOverride: function(driver) {
            var selector = createSelector(model, index);
            return driver.findElement(protractor.By.css(selector));
          },
          column: function (data) {
            return {
              findOverride: function(driver) {
                var selector = createSelector(model, index, data);
                return driver.findElement(protractor.By.css(selector));
              }
            };
          }
        };
      },
      column: function(data) {
        return {
          findArrayOverride: function(driver) {
              var selector = createSelector(model, '', data);
              return driver.findElements(protractor.By.css(selector));
            }
        };
      }
    };


  };

  exports.GvaBy = GvaBy;
}(protractor));