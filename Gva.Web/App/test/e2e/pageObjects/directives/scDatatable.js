/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var Q = require('q'),
      _ = require('lodash');

  function ScDatatable(element) {
    this.element = element;
  }

  ScDatatable.prototype.getColumn = function (columnName) {
    return this.element.findElements(by.css('tbody td.scdt-' + columnName)).then(function (cells) {
      return Q.all(_.map(cells, function (cell) {
        return cell.getText();
      }));
    });
  };

  ScDatatable.prototype.getColumns = function () {
    var columns = _.map(arguments, function (argument) {
      return 'scdt-' + argument.replace(/[\[\]\.]/g, '_');
    });

    return this.element.findElements(by.css('tbody tr')).then(function(rows) {
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

  module.exports = ScDatatable;
}(module, by, require));