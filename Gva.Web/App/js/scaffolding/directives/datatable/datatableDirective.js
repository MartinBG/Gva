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
      replace: false,
      transclude: true,
      templateUrl:'scaffolding/directives/datatable/datatableDirective.html',
      scope: {
        ngModel: '=',
        filterable: '@',
        pageable: '@',
        sortable: '@',
        dynamicColumns: '@'
      },
      link: function (scope, iElement) {
        var table;

        if(scope.dynamicColumns) {
          scope.hideColumn = function (value, i) {
              table.fnSetColumnVis(i, !scope.aoColumnDefs[i].bVisible);
            };
        }
        
        scope.$watch('ngModel', function(){
          if(scope.ngModel) {
            table = iElement.find('table').dataTable({
              aaData: scope.ngModel,
              bDestroy: true,
              bFilter: scope.filterable === 'false' ? false : true,
              bPaginate: scope.pageable === 'false'? false : true,
              bSort: scope.sortable === 'false'? false : true,
              aaSorting: scope.sortingData,
              aoColumnDefs: scope.aoColumnDefs,
              sDom: '<<"span4"l><"span4"f>r>t' +
                  '<"row-fluid"<"span4 pull-left"i><"span4"p>>',
              bDeferRender: true,
              fnPreDrawCallback: function() {
                  angular.element('.dataTables_length select').select2();
                },
              oLanguage: {
                sInfo: l10n.get('datatableDirective.info'),
                sLengthMenu: l10n.get('datatableDirective.displayRecords'),
                sEmptyTable: l10n.get('datatableDirective.noDataAvailable'),
                sInfoEmpty: '',
                sZeroRecords: l10n.get('datatableDirective.noDataAvailable'),
                sSearch: l10n.get('datatableDirective.search'),
                sInfoFiltered: l10n.get('datatableDirective.filtered'),
                oPaginate: {
                  sFirst: l10n.get('datatableDirective.firstPage'),
                  sLast: l10n.get('datatableDirective.lastPage'),
                  sNext: l10n.get('datatableDirective.nextPage'),
                  sPrevious: l10n.get('datatableDirective.previousPage')
                }
              }
            }).fnSetFilteringDelay();
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
            sTitle: column.title || '',
            mData: column.data || '',
            bSortable: column.sortable === 'false'? false : true,
            bVisible: column.visible === 'false'? false : true,
            sType: column.type || 'string',
            aTargets: [columnIndex++],
            fnCreatedCell: column.createCell,
            sDefaultContent: ''
          });

        };
      }
    };
  }

  DatatableDirective.$inject = ['l10n'];

  angular.module('scaffolding').directive('scDatatable', DatatableDirective);

}(angular));