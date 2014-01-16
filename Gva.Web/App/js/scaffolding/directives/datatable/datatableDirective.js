/*
Usage <sc-datatable ng-model="data"
        filterable="true|false"
        pageable="true|false"
        sortable="true|false"
        dynamic-columns="true|false">
 </sc-datatable>
*/
/*global angular*/
(function (angular) {
  'use strict';
  function DatatableDirective(l10n) {
    return {
      restrict: 'E',
      replace: true,
      transclude: true,
      templateUrl:'scaffolding/directives/datatable/datatableDirective.html',
      scope: {
        ngModel: '=',
        filterable: '&',
        pageable: '&',
        sortable: '&',
        dynamicColumns: '&'
      },

      link: function (scope, iElement) {
        var table,
          filterable = scope.filterable() === undefined? true : scope.filterable(),
          pageable = scope.pageable() === undefined? true : scope.pageable(),
          sortable = scope.sortable() === undefined? true : scope.sortable(),
          dynamicColumns = scope.dynamicColumns() === undefined? true : scope.dynamicColumns();

        if(dynamicColumns) {
          scope.hideColumn = function (value, i) {
              table.fnSetColumnVis(i, !scope.aoColumnDefs[i].bVisible);
            };
        }

        scope.$watch('ngModel', function(){
          if(scope.ngModel) {

            table = iElement.find('table').dataTable({
              aaData: scope.ngModel,
              bDestroy: true,
              bFilter: filterable,
              bPaginate: pageable,
              sAutoWidth: false,
              bSort: sortable,
              aaSorting: scope.sortingData,
              aoColumnDefs: scope.aoColumnDefs,
              sDom: '<<"span4"l><"span4"f>r>t' +
                  '<"row-fluid"<"span4 pull-left"i><"span4"p>>',
              sPaginationType: 'bootstrap',
              bDeferRender: true,
              fnPreDrawCallback: function() {
                angular.element('.dataTables_length select').select2();
              },
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
              }
            });
          }

        });
      },
      controller: function ($scope) {
        var columnIndex = 0;
        $scope.sortingData = [];
        $scope.aoColumnDefs = [];

        this.addColumn =  function(column){
          if(column.sorting) {
            $scope.sortingData.push([columnIndex, column.sorting]);
          }

          $scope.aoColumnDefs.push({
            sTitle: l10n.get(column.title) || '',
            mData: column.data || '',
            bSortable: column.sortable === 'false'? false : true,
            bVisible: column.visible === 'false'? false : true,
            sType: column.type || 'string',
            aTargets: [columnIndex++],
            fnCreatedCell: column.createCell,
            sDefaultContent:'',
            sClass: 'scdt-' + (column.data ? column.data.replace(/[\[\]\.]/g, '_') : 'empty'),
            sWidth: column.width
          });
        };
      }
    };
  }

  DatatableDirective.$inject = ['l10n'];

  angular.module('scaffolding').directive('scDatatable', DatatableDirective);

}(angular));
