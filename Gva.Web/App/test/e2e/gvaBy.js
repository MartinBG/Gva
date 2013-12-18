/*global module, require, global*/
(function (module, require, global){
  'use strict';

  var util = require('util'),
    GvaBy = function() {},
    ProtractorBy = function() {},
    by = global.by;

  ProtractorBy.prototype = by;
  util.inherits(GvaBy, ProtractorBy);

  GvaBy.prototype.datatable = function(model) {
    var datatableContainerCss = 'div[ng-model=' + model + ']';

    function createCellSelector(rowIndex, columnData) {
      var selector = datatableContainerCss + ' table.dataTable',
          isHeader;

      if (rowIndex >= 0) {
        if(rowIndex === 0) {
          isHeader = true;
          selector += ' thead';
          rowIndex = 1;
        } else {
          selector += ' tbody';
        }

        selector += ' tr:nth-child(' + rowIndex +')';

        if (columnData) {
          if (isHeader) {
            selector += ' th.scdt-' + columnData;
          } else {
            selector += ' td.scdt-' + columnData;
          }
        }
      } else {
        selector += ' tbody td.scdt-' + columnData;
      }

      return selector;
    }

    return {
      findElementsOverride: function(driver, using) {
        return (using || driver).findElements(by.css(datatableContainerCss));
      },
      row: function (index) {
        return {
          findElementsOverride: function(driver, using) {
            return (using || driver).findElements(by.css(createCellSelector(index)));
          },
          column: function (data) {
            return {
              findElementsOverride: function(driver, using) {
                return (using || driver)
                  .findElements(by.css(createCellSelector(index, data)));
              }
            };
          }
        };
      },
      column: function(data) {
        return {
          findElementsOverride: function(driver, using) {
            return (using || driver)
              .findElements(by.css(createCellSelector(undefined, data)));
          }
        };
      },
      header: function() {
        return {
          findElementsOverride: function(driver, using) {
            return (using || driver).findElements(by.css(createCellSelector(0)));
          },
          column: function (data) {
            return {
              findElementsOverride: function(driver, using) {
                return (using || driver)
                  .findElements(by.css(createCellSelector(0, data)));
              }
            };
          }
        };
      },
      filterInput: function() {
        var filterInputCss =
          datatableContainerCss +
          ' div[class=dataTables_filter] input';
        return {
          findElementsOverride: function (driver, using) {
            return (using || driver).findElements(by.css(filterInputCss));
          }
        };
      },
      infoText: function() {
        var infoTextCss =
          datatableContainerCss +
          ' div[class=dataTables_info]';

        return {
          findElementsOverride: function (driver, using) {
            return (using || driver).findElements(by.css(infoTextCss));
          }
        };
      },
      hideColumnsButton: function() {
        var hideColumnsButtonCss =
          datatableContainerCss +
          ' button[data-toggle=dropdown]';

        return {
          findElementsOverride: function (driver, using) {
            return (using || driver).findElements(by.css(hideColumnsButtonCss));
          }
        };
      },
      hideColumnCheckbox: function(number) {
        var hideColumnCheckboxCss =
          datatableContainerCss +
          ' div[class=dropdown-menu] ' +
          'div:nth-child(' + (number+1) + ') ' +
          'input[type=checkbox]';

        return {
          findElementsOverride: function(driver, using) {
            return (using || driver).findElements(by.css(hideColumnCheckboxCss));
          }
        };
      },
      lengthFilter: function () {
        var lengthFilterCss =
          datatableContainerCss +
          ' div[class=dataTables_length] ' +
          'select';

        return {
          findElementsOverride: function(driver, using) {
            return (using || driver).findElements(by.css(lengthFilterCss));
          },
          option: function (number) {
            var lengthFilterOptionCss =
              lengthFilterCss +
              ' option:nth-child(' + (number+1) + ')';
            return {
              findElementsOverride: function(driver, using) {
                return (using || driver).findElements(by.css(lengthFilterOptionCss));
              }
            };
          }
        };
      },
      pageButton: function (pageNumber) {
        var pageButtonCss =
          datatableContainerCss +
          ' ul[class=pagination] li:nth-child(' + (pageNumber + 1) + ') a';

        return {
          findElementsOverride: function(driver, using) {
            return (using || driver).findElements(by.css(pageButtonCss));
          }
        };
      }
    };
  };

  GvaBy.prototype.nomenclature = function(model) {
    /*jshint -W101*/
    /* jshint -W109*/
    var containerXPath =  ".//div[contains(@class,'select2-container') and following-sibling::*[1][self::input and @ng-model='" + model + "']]",
        containerAnchorXPath = containerXPath + "/a[contains(@class, 'select2-choice')]",
        nameSpanXPath = containerAnchorXPath + "/span[contains(@class, 'select2-chosen')]",
        deselectBtnXPath = containerAnchorXPath + "/abbr",
        dropdownInputXPath = "//div[@id='select2-drop']/div[contains(@class, 'select2-search')]/input[contains(@class, 'select2-input')]";
    /* jshint +W101*/
    /* jshint +W109*/

    return {
      findElementsOverride: function(driver, using) {
        return (using || driver).findElements(by.xpath(containerXPath));
      },
      text: function () {
        return {
          findElementsOverride: function(driver, using) {
            return (using || driver).findElements(by.xpath(nameSpanXPath));
          }
        };
      },
      deselectButton: function () {
        return {
          findElementsOverride: function(driver, using) {
            return (using || driver).findElements(by.xpath(deselectBtnXPath));
          }
        };
      },
      dropdownInput: function () {
        return {
          findElementsOverride: function(driver, using) {
            return (using || driver).findElements(by.xpath(dropdownInputXPath));
          }
        };
      }
    };
  };

  module.exports = GvaBy;
}(module, require, global));
