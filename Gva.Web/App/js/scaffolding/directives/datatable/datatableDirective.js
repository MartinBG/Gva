/*
Usage <sc-datatable ng-model="data"
        filterable="true|false"
        pageable="true|false"
        sortable="true|false"
        dynamic-columns="true|false">
 </sc-datatable>
*/
/*global angular, _*/
(function (angular, _) {
  'use strict';
  function DatatableDirective(l10n, $timeout, $parse, $filter, scDatatableConfig) {
    return {
      restrict: 'E',
      replace: true,
      transclude: true,
      templateUrl:'scaffolding/directives/datatable/datatableDirective.html',
      require: '?ngModel',
      scope: {
        filterable: '&',
        pageable: '&',
        sortable: '&',
        dynamicColumns: '&'
      },

      link: function (scope, iElement, iAttrs, ngModel) {
        var table,
            select2,
            resizeTimeout,
            filterable = scope.filterable() === undefined? true : scope.filterable(),
            pageable = scope.pageable() === undefined? true : scope.pageable(),
            sortable = scope.sortable() === undefined? true : scope.sortable(),
            dynamicColumns = scope.dynamicColumns() === undefined? true : scope.dynamicColumns();

        if(dynamicColumns) {
          //TODO find a way to do that with an inline filter
          scope.nonEmpty = function (col) {
            return col.sTitle !== '';
          };
          scope.canHideColumns = true;
          scope.hideColumn = function (value, i) {
            if (table) {
              table.fnSetColumnVis(i, !scope.aoColumnDefs[i].bVisible);
            }
          };
        }

        if(ngModel) {
          ngModel.$render = function () {
            if (!table) {
              iElement.hide();
              table = iElement.find('table').dataTable({
                aaData: ngModel.$viewValue,
                bDestroy: true,
                bFilter: filterable,
                bPaginate: pageable,
                bAutoWidth: false,
                bSort: sortable,
                aaSorting: scope.sortingData,
                aoColumnDefs: scope.aoColumnDefs,
                sDom: '<<"span4"l><"span4"f>>t' +
                    '<"row-fluid"<"span4 pull-left"i><"span4"p>>',
                oLanguage: {
                  sInfo: l10n.get('scaffolding.scDatatable.info'),
                  sLengthMenu: l10n.get('scaffolding.scDatatable.displayRecords'),
                  sEmptyTable: l10n.get('scaffolding.scDatatable.noDataAvailable'),
                  sInfoEmpty: '',
                  sZeroRecords: l10n.get('scaffolding.scDatatable.noDataAvailable'),
                  sSearch: l10n.get('scaffolding.scDatatable.search'),
                  sInfoFiltered: l10n.get('scaffolding.scDatatable.filtered'),
                  oPaginate: {
                    sFirst: l10n.get('scaffolding.scDatatable.firstPage'),
                    sLast: l10n.get('scaffolding.scDatatable.lastPage'),
                    sNext: l10n.get('scaffolding.scDatatable.nextPage'),
                    sPrevious: l10n.get('scaffolding.scDatatable.previousPage')
                  }
                },
                sScrollX: '100%'
              });

              resizeTimeout = $timeout(function () {
                select2 = iElement
                  .find('.dataTables_length select')
                  .addClass('input-sm')
                  .select2({
                    width: '50px',
                    minimumResultsForSearch: -1
                  });
                iElement.show();
                table.css('width', '100%');
                table.fnAdjustColumnSizing();
              }, 0, false);
            } else {
              table.fnClearTable(false);

              _(ngModel.$viewValue).forEach(function(obj) {
                table.fnAddData(obj, false);
              });

              resizeTimeout = $timeout(function () {
                table.fnAdjustColumnSizing();
              }, 0, false);
            }
          };

          // Make sure datatable is destroyed and removed.
          // Can't use scope.on('$destroy',..) as it is fired 
          // after the element has been removed from the dom
          // and jQuery have already cleared its '.data()'
          // which is required for select2 to properly dispose
          iElement.bind('$destroy', function onDestroyDatatable() {
            if (resizeTimeout) {
              $timeout.cancel(resizeTimeout);
            }
            if (select2) {
              select2.select2('destroy');
            }
            if (table) {
              table.fnDestroy();
            }
          });
        }
      },
      controller: function ScDatatableController($scope) {
        var columnIndex = 0;
        $scope.sortingData = [];
        $scope.aoColumnDefs = [];

        this.addColumn =  function(column){
          if(column.sorting) {
            $scope.sortingData.push([columnIndex, column.sorting]);
          }

          var dataFunction = null;
          if (column.data) {
            var parsedExpression = $parse(column.data);
            dataFunction = function (item) {
              if (column.type === 'date') {
                return $filter('date')(parsedExpression(item), scDatatableConfig.format);
              }
              else {
                return parsedExpression(item);
              }
            };
          }

          $scope.aoColumnDefs.push({
            sTitle: l10n.get(column.title) || '',
            mData: dataFunction,
            bSortable: column.sortable === 'false'? false : true,
            bVisible: column.visible === 'false' ? false : true,
            sType: column.type || 'string',
            aTargets: [columnIndex++],
            fnCreatedCell: column.createCell,
            sDefaultContent: '',
            sClass:
              (column['class'] || '') +
              ' scdt-' + (column.data ? column.data.replace(/[\[\]\.]/g, '_') : 'empty'),
            sWidth: column.width
          });
        };
      }
    };
  }

  DatatableDirective.$inject = ['l10n', '$timeout', '$parse', '$filter', 'scDatatableConfig'];

  angular.module('scaffolding')
    .constant('scDatatableConfig', {
      format: 'mediumDate'
    })
    .directive('scDatatable', DatatableDirective);

}(angular, _));
