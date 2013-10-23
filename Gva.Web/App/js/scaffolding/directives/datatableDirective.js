/*
Usage <sc-datatable ng-model="data"
        filterable="true|false"
        pageable="true|false"
        sortable="true|false"
        dynamic-columns="true|false">
 </sc-datatable>
*/
(function (angular) {
  'use strict';
  function DatatableDirective(l10n, $dialog) {
    return {
      restrict: 'E',
      replace: false,
      transclude: true,
      templateUrl:'scaffolding/templates/datatableTemplate.html',
      scope: {
        ngModel: '=',
        filterable: '@',
        pageable: '@',
        sortable: '@',
        dynamicColumns: '@'
      },
      link: function (scope) {
        var table;

        if(scope.dynamicColumns) {
          scope.open = function() {
            var dialog = $dialog.dialog(scope.dialogOptions);
            dialog.open().then(function(result){
              if(result) {
                angular.forEach(result, function(c, i){
                  scope.aoColumnDefs[i].bVisible = c.visible;
                  table.fnSetColumnVis(i, c.visible);
                });
              }
            });
          };

          scope.dialogOptions = {
            dialogFade: true,
            backdrop: true,
            keyboard: true,
            backdropClick: true,
            templateUrl:'scaffolding/templates/datatableModalTemplate.html',
            controller:'scaffolding.DatatableModalCtrl',
            resolve: {
              columnDefs: function () {
                return _.map(scope.aoColumnDefs, function(c) {
                  return { title: c.sTitle, visible: c.bVisible };
                });
              }
            }
          };
        }

        scope.$watch('ngModel', function(){
          if(scope.ngModel) {

            table = angular.element('.datatable').dataTable({
              aaData: scope.ngModel,
              bAutoWidth: true,
              bFilter: scope.filterable,
              bPaginate: scope.pageable,
              bSort: scope.sortable,
              aaSorting: scope.sortingData,
              sPaginationType: 'full_numbers',
              aoColumnDefs: scope.aoColumnDefs,
              bDeferRender: true,
              oLanguage: {
                sInfo: l10n.get('datatableDirective.info'),
                sLengthMenu: l10n.get('datatableDirective.displayRecords'),
                sEmptyTable: l10n.get('datatableDirective.noDataAvailable'),
                sInfoEmpty: l10n.get('datatableDirective.noDataAvailable'),
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
            sTitle: column.title || '',
            mData: column.data || '',
            bSortable: column.sortable === 'false'? false : true,
            bVisible: column.visible === 'false'? false : true,
            sType: column.type || '',
            aTargets: [columnIndex++],
            fnCreatedCell: column.createCell,
            sDefaultContent: column.defaultValue || '',
            sWidth: column.width
          });

        };
      }
    };
  }

  DatatableDirective.$inject = ['l10n', '$dialog'];

  angular.module('scaffolding').directive('scDatatable', DatatableDirective);


}(angular));