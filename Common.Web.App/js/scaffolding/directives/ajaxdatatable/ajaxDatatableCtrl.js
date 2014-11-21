/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AjaxDatatableCtrl(
    $scope,
    $element,
    $attrs,
    $parse,
    l10n,
    $filter,
    scAjaxDatatableConfig,
    $interpolate,
    $exceptionHandler) {
    $scope.columnDefs = [];
    $scope.items = [];
    $scope.filter = '';
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.pageCount = 1;
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
            return $filter('date')(parsedExpression(item), scAjaxDatatableConfig.format);
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

    var updateDataTableTexts = function () {
      $scope.dataTableTexts.filtered = $interpolate(l10n.get('scaffolding.scDatatable.filtered'))({
        max: numberWithCommas($scope.items.length)
      });

      if ($scope.items.length === 0) {
        $scope.dataTableTexts.info = l10n.get('scaffolding.scDatatable.noDataAvailable');
        return;
      }
      $scope.dataTableTexts.info = $interpolate(l10n.get('scaffolding.scDatatable.info'))({
        total: numberWithCommas($scope.total),
        start: $scope.currentPage === 1 ?
          1 :
          numberWithCommas($scope.pageSize * ($scope.currentPage - 1) + 1),
        end: $scope.items.length < $scope.pageSize ?
          numberWithCommas($scope.currentPage * $scope.pageSize -
                          ($scope.pageSize - $scope.items.length)) :
          numberWithCommas($scope.currentPage * $scope.pageSize)
      });
    };

    var numberWithCommas = function (x) {
      return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    };

    var isLoading = false;

    $scope.setItems = function (page, set, count) {
      $scope.items = [];
      $scope.total = count;
      $scope.currentPage = page;

      for (var i = 0, l1 = set.length; i < l1; i++) {
        var mappedItem = [];
        mappedItem.item = set[i];
        for (var j = 0, l2 = $scope.columnDefs.length; j < l2; j++) {
          if ($scope.columnDefs[j].dataFunction) {
            mappedItem.push($scope.columnDefs[j].dataFunction(set[i]));
          }
          else {
            mappedItem.push(null);
          }
        }

        $scope.items.push(mappedItem);
      }

      $scope.pageCount = Math.ceil($scope.total / $scope.pageSize);
      $scope.setPaging();

      updateDataTableTexts();
    };

    $scope.setFilter = function (filter) {
      if (isLoading) {
        return;
      }

      $scope.filter = filter;

      if ($scope.filter) {

        _.forEach($scope.items, function (item) {
          for (var i = 0, l = $scope.columnDefs.length; i < l; i++) {
            if (item[i] &&
                _.contains(item[i].toString().toLowerCase(), $scope.filter.toLowerCase())) {
              item.isMarked = true;

              return;
            }
          }

          item.isMarked = false;
        });
      }
      else {
        _.forEach($scope.items, function (item) {
          item.isMarked = false;
        });
      }

      $scope.render();
    };

    $scope.setCurrentPage = function (page) {
      if (isLoading) {
        return;
      }

      isLoading = true;
      $scope.currentPage = parseInt(page, 10);

      $scope.fetcher($scope.currentPage, $scope.pageSize).then(function (result) {
        $scope.setItems($scope.currentPage, result[$scope.setProp], result[$scope.countProp]);
        isLoading = false;
        $scope.setFilter($scope.filter);
        //$scope.render();
      }, function (error) {
        $exceptionHandler(error);
      });
    };

    $scope.setPageSize = function (pageSize) {
      if (isLoading) {
        return;
      }

      $scope.pageSize = pageSize;
      $scope.setCurrentPage(1);
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
    });
  }

  AjaxDatatableCtrl.$inject = [
    '$scope',
    '$element',
    '$attrs',
    '$parse',
    'l10n',
    '$filter',
    'scAjaxDatatableConfig',
    '$interpolate',
    '$exceptionHandler'
  ];

  angular.module('scaffolding')
  .constant('scAjaxDatatableConfig', {
    format: 'mediumDate'
  });

  angular.module('scaffolding').controller('AjaxDatatableCtrl', AjaxDatatableCtrl);
}(angular, _));
