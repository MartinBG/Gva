/*global module, require, global*/
(function (module, require, global){
  'use strict';

  var util = require('util'),
    GvaBy = function() {},
    ProtractorBy = function() {},
    protractor = global.protractor;

  ProtractorBy.prototype = protractor.By;
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
      findOverride: function(driver, element) {
        return (element || driver).findElement(protractor.By.css(datatableContainerCss));
      },
      findArrayOverride: function(driver, element) {
        return (element || driver).findElements(protractor.By.css(datatableContainerCss));
      },
      row: function (index) {
        return {
          findOverride: function(driver, element) {
            return (element || driver).findElement(protractor.By.css(createCellSelector(index)));
          },
          findArrayOverride: function(driver, element) {
            return (element || driver).findElements(protractor.By.css(createCellSelector(index)));
          },
          column: function (data) {
            return {
              findOverride: function(driver, element) {
                return (element || driver)
                  .findElement(protractor.By.css(createCellSelector(index, data)));
              },
              findArrayOverride: function(driver, element) {
                return (element || driver)
                  .findElements(protractor.By.css(createCellSelector(index, data)));
              }
            };
          }
        };
      },
      column: function(data) {
        return {
          findOverride: function(driver, element) {
            return (element || driver)
              .findElement(protractor.By.css(createCellSelector(undefined, data)));
          },
          findArrayOverride: function(driver, element) {
            return (element || driver)
              .findElements(protractor.By.css(createCellSelector(undefined, data)));
          }
        };
      },
      header: function() {
        return {
          findOverride: function(driver, element) {
            return (element || driver).findElement(protractor.By.css(createCellSelector(0)));
          },
          findArrayOverride: function(driver, element) {
            return (element || driver).findElements(protractor.By.css(createCellSelector(0)));
          },
          column: function (data) {
            return {
              findOverride: function(driver, element) {
                return (element || driver)
                  .findElement(protractor.By.css(createCellSelector(0, data)));
              },
              findArrayOverride: function(driver, element) {
                return (element || driver)
                  .findElements(protractor.By.css(createCellSelector(0, data)));
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
          findOverride: function (driver, element) {
            return (element || driver).findElement(protractor.By.css(filterInputCss));
          },
          findArrayOverride: function (driver, element) {
            return (element || driver).findElements(protractor.By.css(filterInputCss));
          }
        };
      },
      infoText: function() {
        var infoTextCss =
          datatableContainerCss +
          ' div[class=dataTables_info]';

        return {
          findOverride: function (driver, element) {
            return (element || driver).findElement(protractor.By.css(infoTextCss));
          },
          findArrayOverride: function (driver, element) {
            return (element || driver).findElements(protractor.By.css(infoTextCss));
          }
        };
      },
      hideColumnsButton: function() {
        var hideColumnsButtonCss =
          datatableContainerCss +
          ' button[data-toggle=dropdown]';

        return {
          findOverride: function (driver, element) {
            return (element || driver).findElement(protractor.By.css(hideColumnsButtonCss));
          },
          findArrayOverride: function (driver, element) {
            return (element || driver).findElement(protractor.By.css(hideColumnsButtonCss));
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
          findOverride: function(driver, element) {
            return (element || driver).findElement(protractor.By.css(hideColumnCheckboxCss));
          },
          findArrayOverride: function(driver, element) {
            return (element || driver).findElements(protractor.By.css(hideColumnCheckboxCss));
          }
        };
      },
      lengthFilter: function () {
        var lengthFilterCss =
          datatableContainerCss +
          ' div[class=dataTables_length] ' +
          'select';

        return {
          findOverride: function(driver, element) {
            return (element || driver).findElement(protractor.By.css(lengthFilterCss));
          },
          findArrayOverride: function(driver, element) {
            return (element || driver).findElements(protractor.By.css(lengthFilterCss));
          },
          option: function (number) {
            var lengthFilterOptionCss =
              lengthFilterCss +
              ' option:nth-child(' + (number+1) + ')';
            return {
              findOverride: function(driver, element) {
                return (element || driver).findElement(protractor.By.css(lengthFilterOptionCss));
              },
              findArrayOverride: function(driver, element) {
                return (element || driver).findElements(protractor.By.css(lengthFilterOptionCss));
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
          findOverride: function(driver, element) {
            return (element || driver).findElement(protractor.By.css(pageButtonCss));
          },
          findArrayOverride: function(driver, element) {
            return (element || driver).findElements(protractor.By.css(pageButtonCss));
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
      findOverride: function(driver, element) {
        return (element || driver).findElement(protractor.By.xpath(containerXPath));
      },
      findArrayOverride: function(driver, element) {
        return (element || driver).findElements(protractor.By.xpath(containerXPath));
      },
      text: function () {
        return {
          findOverride: function(driver, element) {
            return (element || driver).findElement(protractor.By.xpath(nameSpanXPath));
          },
          findArrayOverride: function(driver, element) {
            return (element || driver).findElements(protractor.By.xpath(nameSpanXPath));
          }
        };
      },
      deselectButton: function () {
        return {
          findOverride: function(driver, element) {
            return (element || driver).findElement(protractor.By.xpath(deselectBtnXPath));
          },
          findArrayOverride: function(driver, element) {
            return (element || driver).findElements(protractor.By.xpath(deselectBtnXPath));
          }
        };
      },
      dropdownInput: function () {
        return {
          findOverride: function(driver, element) {
            return (element || driver).findElement(protractor.By.xpath(dropdownInputXPath));
          },
          findArrayOverride: function(driver, element) {
            return (element || driver).findElements(protractor.By.xpath(dropdownInputXPath));
          }
        };
      }
    };
  };

  module.exports = GvaBy;
}(module, require, global));
