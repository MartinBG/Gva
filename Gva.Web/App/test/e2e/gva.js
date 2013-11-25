/*global exports, require, protractor, exports*/
(function(protractor){
  'use strict';
  function createSelector (model, index, data) {
    var selector ='';
    if(model) {
      selector += 'div[ng-model=' + model + '] table.dataTable';
    } else {
      selector += 'div table.dataTable';
    }
    if(index || data){
      if(index === 0) {
        selector += ' thead';
        index = 1;
      } else {
        selector += ' tbody';
      }
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
        var selector = createSelector(model);
        return driver.findElement(protractor.By.css(selector));
      },
      findArrayOverride: function(driver) {
        var selector = createSelector(model);
        return driver.findElements(protractor.By.css(selector));
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
      },
      inputFilter: function() {
        return {
          findOverride: function (driver) {
            return driver.findElement(
              protractor.By.css(
              'div[ng-model=' + model + '] ' +
              'div[class=dataTables_filter] ' +
              'input'));
          },
          isDisplayed: function () {
            return {
              findOverride: function(driver) {
                var selector = 'div[ng-model=' + model + '] ' +
                  'div[class=dataTables_filter]';
                return driver.isElementPresent(protractor.By.css(selector));
              }
            };
          }
        };
      },
      infoText: function() {
        return {
          findOverride: function (driver) {
            return driver.findElement(
              protractor.By.css(
              'div[ng-model=' + model + '] ' +
              'div[class=dataTables_info]'));
          }
        };
      },
      buttonHideColumns: function() {
        return {
          findOverride: function (driver) {
            return driver.findElement(
              protractor.By.css(
              'div[ng-model=' + model + '] ' +
              'button[data-toggle=dropdown]'));
          },
          isDisplayed: function () {
            return {
              findOverride: function(driver) {
                var selector = 'div[ng-model=' + model + '] ' +
                  'div[class*=ng-hide] ' +
                  'button[data-toggle=dropdown]';

                return !driver.isElementPresent(protractor.By.css(selector));
              }
            };
          }
        };
      },
      hideColumnsCheckbox: function(number) {
        return {
          findOverride: function(driver) {
            return driver.findElement(
                protractor.By.css(
                'div[ng-model=' + model + '] ' +
                'div[class=dropdown-menu] ' +
                'div:nth-child(' + (number+1) + ') ' +
                'input[type=checkbox]'));
          }
        };
      },
      lengthFilter: function () {
        return {
          findOverride: function(driver) {
            return driver.findElement(
                protractor.By.css(
                'div[ng-model=' + model + '] ' +
                'div[class=dataTables_length] ' +
                'select'));
          },
          option: function (number) {
            return {
              findOverride: function(driver) {
                return driver.findElement(
                  protractor.By.css(
                  'div[ng-model=' + model + '] ' +
                  'div[class=dataTables_length] ' +
                  'select ' +
                  'option:nth-child(' + (number+1) + ')'));
              }
            };
          },
          isDisplayed: function () {
            return {
              findOverride: function(driver) {
                var selector = 'div[ng-model=' + model + '] ' +
                  'div[class=dataTables_length] ' +
                  'select';
                return driver.isElementPresent(protractor.By.css(selector));
              }
            };
          }
        };
      }
    };
  };

  GvaBy.nomenclature = function(model) {
    /*jshint -W101*/
    /* jshint -W109*/
    var containerXPath =  "//div[contains(@class,'select2-container') and following-sibling::*[1][self::input and @ng-model='" + model + "']]",
        containerAnchorXPath = containerXPath + "/a[contains(@class, 'select2-choice')]",
        nameSpanXPath = containerAnchorXPath + "/span[contains(@class, 'select2-chosen')]",
        deselectBtnXPath = containerAnchorXPath + "/abbr",
        dropdownInputXPath = "//div[@id='select2-drop']/div[contains(@class, 'select2-search')]/input[contains(@class, 'select2-input')]";
    /* jshint +W101*/
    /* jshint +W109*/

    return {
      findOverride: function(driver) {
        return driver.findElement(protractor.By.xpath(containerXPath));
      },
      findArrayOverride: function(driver) {
        return driver.findElements(protractor.By.xpath(containerXPath));
      },
      text: function () {
        return {
          findOverride: function(driver) {
            return driver.findElement(protractor.By.xpath(nameSpanXPath));
          },
          findArrayOverride: function(driver) {
            return driver.findElements(protractor.By.xpath(nameSpanXPath));
          }
        };
      },
      deselectButton: function () {
        return {
          findOverride: function(driver) {
            return driver.findElement(protractor.By.xpath(deselectBtnXPath));
          },
          findArrayOverride: function(driver) {
            return driver.findElements(protractor.By.xpath(deselectBtnXPath));
          }
        };
      },
      dropdownInput: function () {
        return {
          findOverride: function(driver) {
            return driver.findElement(protractor.By.xpath(dropdownInputXPath));
          },
          findArrayOverride: function(driver) {
            return driver.findElements(protractor.By.xpath(dropdownInputXPath));
          }
        };
      }
    };
  };
  exports.GvaBy = GvaBy;
}(protractor));