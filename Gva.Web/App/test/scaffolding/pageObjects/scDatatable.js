/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var Q = require('q'),
      _ = require('lodash');

  function ScDatatable(element) {
    this.element = element;
    this.hideColumnsButton = this.element.findElement(by.css('button[data-toggle=dropdown]'));
  }

  ScDatatable.prototype.getColumn = function (columnName) {
    return this.element.findElements(
      by.css('.dataTables_scrollBody table tbody td.scdt-' + columnName)
    ).then(function (cells) {
      return Q.all(_.map(cells, function (cell) {
        return cell.getText();
      }));
    });
  };

  ScDatatable.prototype.getColumns = function () {
    var columns = _.map(arguments, function (argument) {
      return 'scdt-' + argument.replace(/[\[\]\.]/g, '_');
    });

    return this.element.findElements(
      by.css('.dataTables_scrollBody table tbody tr')
    ).then(function(rows) {
      return Q.all(_.map(rows, function (row) {
        return row.findElements(by.css('td'));
      })).then(function (resolvedRows) {
        return Q.all(_.map(resolvedRows, function (resolvedRow) {
          return Q.all(_.map(resolvedRow, function (cell) {
            var showCell = cell.getAttribute('class').then(function (classNames) {
              return _.some(classNames.split(' '), function (className) {
                return _.contains(columns, className);
              });
            });

            var cellText = cell.getText();

            return Q.all([showCell, cellText]).then(function (result) {
              return { showCell: result[0], text: result[1] };
            });
          })).then(function (mappedCells) {
            var cellsToShow = _.filter(mappedCells, function (cell) {
              return cell.showCell;
            });

            return _.map(cellsToShow, function (cell) {
              return cell.text;
            });
          });
        }));
      });
    });
  };

  ScDatatable.prototype.getRowButtons = function (number) {
    return this.element.findElements(
      by.css('.dataTables_scrollBody table tbody tr:nth-child(' + number + ') button'));
  };

  ScDatatable.prototype.getRow = function (number) {
    return this.element.findElements(
      by.css('.dataTables_scrollBody table tbody tr:nth-child(' + number + ') td')
    ).then(function(columns) {
      return Q.all(_.map(columns, function (cell) {
        return cell.getText();
      }));
    });
  };

  ScDatatable.prototype.getHeaders = function () {
    return this.element.findElements(
      by.css('.dataTables_scrollHeadInner table thead tr th')
    ).then(function(columns) {
      return Q.all(_.map(columns, function (column) {
        return column.getText();
      }));
    });
  };

  ScDatatable.prototype.clickHeader = function (headerName) {
    return this.element.findElements(
      by.css('.dataTables_scrollHeadInner table thead tr th')
    ).then(function (columns) {
      return Q.all(_.map(columns, function (column) {
        column.getAttribute('class').then(function (classNames) {
          if (_.contains(classNames.split(' '), 'scdt-' + headerName)) {
            column.click();
            return;
          }
        });
      }));
    });
  };

  ScDatatable.prototype.getColumnsClasses = function () {
    return this.element.findElements(
      by.css('.dataTables_scrollHeadInner table thead tr th')
    ).then(function (columns) {
      return Q.all(_.map(columns, function (column) {
        return column.getAttribute('class');
      }));
    });
  };

  ScDatatable.prototype.setLengthFilterOption = function (number) {
    var cssOptionSelector = ' .dataTables_length select' +
      ' option:nth-child(' + (number + 1) + ')';
    this.element.findElement(by.css(cssOptionSelector)).click();
  };

  ScDatatable.prototype.clickHideColumnsButton = function () {
    return this.hideColumnsButton.click();
  };
  
  ScDatatable.prototype.clickHideColumnCheckbox = function (number) {
    var hideColumnCheckboxCss = ' .dropdown-menu ' +
      'div:nth-child(' + (number + 1) + ') input[type=checkbox]';
    this.element.findElement(by.css(hideColumnCheckboxCss)).click();
  };

  ScDatatable.prototype.goToPage = function (pageNumber) {
    this.element
      .findElement(by.css(' ul[class=pagination] li:nth-child(' + (pageNumber + 1) + ') a'))
      .click();
  };

  ScDatatable.prototype.filterInput = function () {
    return this.element.findElement(by.css('div[class=dataTables_filter] input'));
  };

  ScDatatable.prototype.isHideColumnButtonDisplayed = function () {
    var hideColumnsButton = this.element.findElement(by.css('.dynamic-columns-btn'));
    return hideColumnsButton.getAttribute('class').then(function (classNames) {
      return !(_.contains(classNames.split(' '), 'ng-hide'));
    });
  };

  ScDatatable.prototype.isFilterDisplayed = function () {
    return this.element.isElementPresent(by.css('.dataTables_filter'));
  };

  ScDatatable.prototype.isPaginationDisplayed = function () {
    return this.element.isElementPresent(by.css('ul[class=pagination]'));
  };

  ScDatatable.prototype.isLengthRangeDisplayed = function () {
    return this.element.isElementPresent(by.css('.dataTables_length'));
  };

  ScDatatable.prototype.setFilterInput = function (text) {
    return this.element.findElement(by.css('div[class=dataTables_filter] input'))
    .sendKeys(text);
  };

  ScDatatable.prototype.getInfoText = function () {
    return this.element.findElement(by.css('div[class=dataTables_info]')).getText();
  };

  module.exports = ScDatatable;
}(module, by, require));