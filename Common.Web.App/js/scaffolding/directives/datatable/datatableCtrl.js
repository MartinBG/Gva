/*global angular, _, moment*/
(function (angular, _, moment) {
  'use strict';

  function DatatableCtrl(
    $scope,
    $element,
    $attrs,
    $parse,
    l10n,
    $filter,
    scDatatableConfig,
    $interpolate)
  {
    $scope.columnDefs = [];
    $scope.items = [];
    $scope.filteredItems = [];
    $scope.sortedItems = [];
    $scope.filter = '';
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.pageCount = 1;
    $scope.pageItems = [];
    $scope.pagingContents = [];
    $scope.numOfPageButtons = 7;
    $scope.sortingColumnIndex = null;
    $scope.sortingType = null;
    $scope.dataTableTexts = {
      info: null,
      filtered: null,
      previousPage: l10n.get('scaffolding.scDatatable.previousPage'),
      nextPage: l10n.get('scaffolding.scDatatable.nextPage'),
      search: l10n.get('scaffolding.scDatatable.search'),
      displayRecords: l10n.get('scaffolding.scDatatable.displayRecords'),
      noDataAvailable: l10n.get('scaffolding.scDatatable.noDataAvailable')
    };
    var columnIndex = 0;

    this.addColumn = function (column) {
      var dataFunction = null;
      if (column.data) {
        var parsedExpression = $parse(column.data);
        dataFunction = function (item) {
          if (column.type === 'date') {
            return $filter('date')(parsedExpression(item), scDatatableConfig.format);
          }
          else if (column.type === 'boolean') {
            var value = parsedExpression(item);
            if (value === null || value === undefined) {
              return null;
            }

            return value ? 'Да' : 'Не';
          }
          else {
            return parsedExpression(item);
          }
        };
      }

      $scope.columnDefs.push({
        transcludeFn: column.transcludeFn,
        hasContent: column.hasContent,
        data: column.data,
        title: l10n.get(column.title) || '',
        l10ntitle: column.title || '',
        dataFunction: dataFunction,
        sortable: column.sortable === 'false' ? false : true,
        visible: column.visible === 'false' ? false : true,
        type: column.type || 'string',
        index: [columnIndex++],
        defaultContent: '',
        columnClass:
          (column['class'] || '') +
          ' scdt-' + (column.data ? column.data.replace(/[\[\]\.]/g, '_') : 'empty'),
        width: column.width
      });
    };

    var updateFilteredItems = function () {
      if ($scope.filter) {
        $scope.filteredItems = _.filter($scope.items, function (item) {
          for (var i = 0, l = $scope.columnDefs.length; i < l; i++) {
            if (item[i] &&
                _.contains(item[i].toString().toLowerCase(), $scope.filter.toLowerCase())) {
              return true;
            }
          }
          return false;
        });
      }
      else {
        $scope.filteredItems = $scope.items;
      }

      updatePageCount();
      updateSortedItems();
    };

    var updateSortedItems = function () {
      if ($scope.sortingColumnIndex !== null && $scope.sortingColumnIndex !== undefined) {
        $scope.sortedItems = $scope.filteredItems.slice();
        $scope.sortedItems = _.sortBy($scope.sortedItems, function (x) {
          if ($scope.columnDefs[$scope.sortingColumnIndex].type === 'date') {
            if (x[$scope.sortingColumnIndex] !== null &&
              x[$scope.sortingColumnIndex] !== undefined) {
              return moment(x[$scope.sortingColumnIndex], 'DD.MM.YYYY').toDate();
            } else {
              return undefined;
            }
          } else {
            return x[$scope.sortingColumnIndex];
          }
        });

        if ($scope.sortingType !== 'asc') {
          $scope.sortedItems.reverse();
        }
      }
      else {
        $scope.sortedItems = $scope.filteredItems;
      }

      updatePageItems();
    };

    var updatePageItems = function () {
      var size = parseInt($scope.pageSize, 10);

      $scope.pageItems =
        $scope.sortedItems.slice(
          ($scope.currentPage - 1) * size,
          ($scope.currentPage - 1) * size + size);

      updateDataTableTexts();
    };

    var updateDataTableTexts = function () {
      $scope.dataTableTexts.filtered = $interpolate(l10n.get('scaffolding.scDatatable.filtered'))({
        max: numberWithCommas($scope.items.length)
      });

      if ($scope.pageItems.length === 0) {
        $scope.dataTableTexts.info = l10n.get('scaffolding.scDatatable.noDataAvailable');
        return;
      }
      $scope.dataTableTexts.info = $interpolate(l10n.get('scaffolding.scDatatable.info'))({
        total: numberWithCommas($scope.sortedItems.length),
        start: $scope.currentPage === 1 ?
          1 :
          numberWithCommas($scope.pageSize * ($scope.currentPage - 1) + 1),
        end: $scope.pageItems.length < $scope.pageSize ?
          numberWithCommas($scope.currentPage * $scope.pageSize -
                          ($scope.pageSize - $scope.pageItems.length)) :
          numberWithCommas($scope.currentPage * $scope.pageSize)
      });
    };

    var numberWithCommas = function (x) {
      return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    };

    var updatePageCount = function () {
      $scope.pageCount = Math.ceil($scope.filteredItems.length / $scope.pageSize);
      $scope.setPaging();
    };

    $scope.setItems = function (items) {
      $scope.items = [];
      $scope.currentPage = 1;
      if (!$scope.pageable) {
        $scope.pageSize = items.length || 1;
      }

      for (var i = 0, l1 = items.length; i < l1; i++) {
        var mappedItem = [];
        mappedItem.item = items[i];
        for (var j = 0, l2 = $scope.columnDefs.length; j < l2; j++) {
          if ($scope.columnDefs[j].dataFunction) {
            mappedItem.push($scope.columnDefs[j].dataFunction(items[i]));
          }
          else {
            mappedItem.push(null);
          }
        }

        $scope.items.push(mappedItem);
      }

      updateFilteredItems();
    };

    $scope.setSortingData = function (index, type) {
      $scope.sortingColumnIndex = index;
      $scope.sortingType = type;
      $scope.setCurrentPage(1);

      updateSortedItems();
    };

    $scope.setFilter = function (filter) {
      $scope.filter = filter;
      $scope.currentPage = 1;

      updateFilteredItems();
    };

    $scope.setCurrentPage = function (page) {
      if (!page || page <= 0 || page > $scope.pageCount || $scope.pageCount === 1) {
        return;
      }

      $scope.currentPage = parseInt(page, 10);
      $scope.setPaging();
      updatePageItems();
    };

    $scope.setPageSize = function (pageSize) {
      $scope.currentPage = 1;
      $scope.pageSize = pageSize;

      updatePageCount();
      updatePageItems();
    };

    $scope.setPaging = function () {
      var i, l, pageCount = $scope.pageCount;
      if (pageCount <= $scope.numOfPageButtons) {
        $scope.pagingContents = _.range(1, pageCount + 1);
      }
      else {
        if ($scope.currentPage < $scope.numOfPageButtons - 2) {
          $scope.pagingContents = _.range(1, $scope.numOfPageButtons - 1);
          $scope.pagingContents.push(null);
          $scope.pagingContents.push(pageCount);
        }
        else if ($scope.currentPage > pageCount - $scope.numOfPageButtons + 3) {
          $scope.pagingContents = [];
          $scope.pagingContents.push(1);
          $scope.pagingContents.push(null);
          for (i = pageCount - $scope.numOfPageButtons + 3, l = pageCount; i <= l; i++) {
            $scope.pagingContents.push(i);
          }
        }
        else {
          $scope.pagingContents = [];
          $scope.pagingContents.push(1);
          $scope.pagingContents.push(null);
          for (i = $scope.currentPage - 1, l = $scope.currentPage + 2; i < l; i++) {
            $scope.pagingContents.push(i);
          }
          $scope.pagingContents.push(undefined);
          $scope.pagingContents.push(pageCount);
        }
      }
    };

    $scope.$on('$destroy', function () {
      $scope.columnDefs = null;
      $scope.items = null;
      $scope.filteredItems = null;
      $scope.sortedItems = null;
      $scope.pageItems = null;
    });
  }

  DatatableCtrl.$inject = [
    '$scope',
    '$element',
    '$attrs',
    '$parse',
    'l10n',
    '$filter',
    'scDatatableConfig',
    '$interpolate'
  ];

  angular.module('scaffolding')
  .constant('scDatatableConfig', {
    format: 'mediumDate'
  });

  angular.module('scaffolding').controller('DatatableCtrl', DatatableCtrl);
}(angular, _, moment));
