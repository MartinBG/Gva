/*
Usage <sc-datatable2 items="data"
        items-count = "count"
        fetch-fn = "fetchResult"
        set-prop = "result"
        count-prop = "resultCount"
        filterable="true|false"
        scrollable="true|false"
        has-info-text="true|false"
        dynamic-columns="true|false"
        row-class="{'class' : expression}">
 </sc-datatable>
*/
/*global angular, $, _*/
(function (angular, $, _) {
  'use strict';
  function Datatable2Directive(l10n, $timeout, $parse) {
    return {
      restrict: 'E',
      replace: true,
      transclude: true,
      templateUrl: 'js/scaffolding/directives/datatable2/datatable2Directive.html',
      scope: {
        filterable: '&',
        //pageable: '&',
        //sortable: '&',
        dynamicColumns: '&',
        scrollable: '&',
        hasInfoText: '&'
      },
      link: function ($scope, $element, $attrs) {
        var select2,
            tableHeader,
            tableRows = [],
            dataTable = $element.find('table'),
            rowClassExpr = $parse($attrs.rowClass),
            //sortable = $scope.sortable() === undefined ? true : $scope.sortable(),
            items = _.cloneDeep($scope.$parent[$attrs.items]) || [],
            itemsCount = $scope.$parent[$attrs.itemsCount] || 0,
            setProp = $attrs.setProp || 'set',
            countProp = $attrs.countProp || 'count';

        $scope.fetcher = $scope.$parent[$attrs.fectchFn];
        $scope.setProp = setProp;
        $scope.countProp = countProp;
        $scope.filterable = $scope.filterable() === undefined ? true : $scope.filterable();
        $scope.scrollable = $scope.scrollable() === undefined ? true : $scope.scrollable();
        //$scope.pageable = $scope.pageable() === undefined ? true : $scope.pageable();
        $scope.hasInfoText =
          $scope.hasInfoText() === undefined ? true : $scope.hasInfoText();
        $scope.dynamicColumns =
          $scope.dynamicColumns() === undefined ? true : $scope.dynamicColumns();

        select2 = $element
          .find('select')
          .select2({
            minimumResultsForSearch: -1
          });

        if ($scope.dynamicColumns) {
          //TODO find a way to do that with an inline filter
          $scope.nonEmpty = function (col) {
            return col.title !== '';
          };

          $scope.hideColumn = function (i) {
            $scope.columnDefs[i].visible = !$scope.columnDefs[i].visible;
            renderHeader();
            $scope.render();
          };
        }

        var renderHeader = function () {
          if (tableHeader) {
            tableHeader.remove();
          }

          tableHeader = $('<thead></thead>');
          var headerRow = $('<tr></tr>');

          _($scope.columnDefs).forEach(function (columnDef) {
            if (!columnDef.visible) {
              return;
            }

            var headerCell = $('<th></th>')
              .html(columnDef.title + ' <span></span>')
              .addClass(columnDef.columnClass);

            //if (sortable) {
            //  if (columnDef.sortable) {
            //    var sortingSpan = $('span', headerCell);

            //    headerCell.addClass('sorting');
            //    sortingSpan.addClass('glyphicon glyphicon-sort');

            //    headerCell.on('click', function () {
            //      $scope.$apply(function () {
            //        if (sortingSpan.hasClass('glyphicon-sort-by-attributes')) {
            //          $('th.sorting span', headerRow).removeClass();
            //          $('th.sorting span', headerRow).addClass('glyphicon glyphicon-sort');

            //          sortingSpan.removeClass();
            //          sortingSpan.addClass('glyphicon glyphicon-sort-by-attributes-alt');

            //          $scope.setSortingData(index, 'desc');
            //        }
            //        else {
            //          $('th.sorting span', headerRow).removeClass();
            //          $('th.sorting span', headerRow).addClass('glyphicon glyphicon-sort');

            //          sortingSpan.removeClass();
            //          sortingSpan.addClass('glyphicon glyphicon-sort-by-attributes');

            //          $scope.setSortingData(index, 'asc');
            //        }
            //      });
            //    });
            //  }
            //}

            headerRow.append(headerCell);
          });

          tableHeader.append(headerRow);
          dataTable.append(tableHeader);
        };

        var destroyRows = function () {
          var i, row;

          if (tableRows.length > 0) {
            for (i = 0; i < tableRows.length; i++) {
              row = tableRows[i];

              row.rowElement.remove();
              if (row.scope) {
                row.scope.$destroy();
                row.scope = null;
              }
            }

            tableRows = [];
          }
        };

        $scope.render = function () {
          var i, j, l1, l2, row, childScope, rowElement, columnDef;

          destroyRows();

          for (i = 0, l1 = $scope.items.length; i < l1; i++) {
            childScope = null;
            row = {};
            rowElement = $('<tr></tr>');

            // disable W083: Don't make functions within a loop.
            // because this function is not used as a callback in the future
            // jshint -W083
            _.forOwn(rowClassExpr($scope.$parent, { item: $scope.items[i].item }),
              function (value, key) {
                if (value) {
                  rowElement.addClass(key);
                }
              });
            // jshint +W083

            if ($scope.items[i].isMarked) {
              rowElement.addClass('success');
            }

            for (j = 0, l2 = $scope.columnDefs.length; j < l2; j++) {
              columnDef = $scope.columnDefs[j];

              if (!columnDef.visible) {
                continue;
              }

              var cellData = $scope.items[i][j];
              var cell = $('<td></td>').addClass(columnDef.columnClass);

              if (columnDef.hasContent) {
                if (!childScope) {
                  childScope = $scope.$parent.$new();
                  childScope.item = $scope.items[i].item;
                  row.scope = childScope;
                  rowElement.on('$destroy', angular.bind(childScope, childScope.$destroy));
                }

                var clone = columnDef.transcludeFn(childScope, angular.noop);

                rowElement.append(
                  cell.append(clone));
              }
              else if (cellData !== undefined && cellData !== null) {
                rowElement.append(
                  cell.html(cellData));
              }
              else {
                rowElement.append(
                  cell.html(columnDef.defaultContent));
              }
            }

            dataTable.append(rowElement);
            row.rowElement = rowElement;
            tableRows.push(row);
          }

          if ($scope.items.length === 0) {
            row = {};
            rowElement = $('<tr></tr>');

            rowElement.append(
              $('<td></td>')
                .attr('colspan', _.filter($scope.columnDefs, 'visible').length)
                  .append(
                    $('<div></div>')
                      .html($scope.dataTableTexts.noDataAvailable)));

            dataTable.append(rowElement);
            row.rowElement = rowElement;
            tableRows.push(row);
          }
        };

        var initializing = true;

        if (initializing) {
          renderHeader();
        }

        if (items) {
          $scope.setItems(1, items, itemsCount || items.length);
          $scope.render();
          items = undefined;
        } else {
          $scope.setCurrentPage(1);
        }

        $scope.$watch('pageSize', function (pageSize) {
          if (initializing) {
            $timeout(function () {
              initializing = false;
            });
          } else {
            $scope.setPageSize(pageSize);
          }
        });

        $scope.$watch('filter', function (filter) {
          $scope.setFilter(filter);
        });

        $element.bind('$destroy', function onDestroyDatatable() {
          if (select2) {
            select2.select2('destroy');
          }

          destroyRows();

          dataTable.remove();
        });
      },
      controller: 'Datatable2Ctrl'
    };
  }

  Datatable2Directive.$inject = ['l10n', '$timeout', '$parse'];

  angular.module('scaffolding').directive('scDatatable2', Datatable2Directive);

}(angular, $, _));
