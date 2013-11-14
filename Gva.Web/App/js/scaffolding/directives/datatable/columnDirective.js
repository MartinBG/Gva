/*
Usage: <sc-column model-name="property"
        type="string|numeric|date"
        sortable="true|false"
        sorting="asc|desc"
        visible="true|false"
        [title]>
        </sc-column>
*/

/*global angular*/
(function (angular) {
  'use strict';

  function ColumnDirective() {
    return {
      restrict: 'E',
      replace: true,
      require: '^scDatatable',
      transclude: true,
      compile : function(element, attrs, childTranscludeFn){
        return function(scope, iElement, iAttrs, scDatatable){
          var createCellFunc;
          var innerData = childTranscludeFn(scope.$new(), function(){});
          if(innerData.length > 1){
            createCellFunc = function(nTd, sData, oData){
              var childScope = scope.$new();
              childScope.data = sData;
              childScope.item = oData;
              childTranscludeFn(childScope, function(clone){
                angular.element(nTd).empty();
                angular.element(nTd).append(clone);
              });
            };
          }

          scDatatable.addColumn({
            type: iAttrs.type,
            data: iAttrs.data,
            title: iAttrs.title,
            sortable: iAttrs.sortable,
            visible: iAttrs.visible,
            sorting: iAttrs.sorting,
            createCell: createCellFunc,
            defaultValue: iAttrs.defaultValue
          });
        };
      }
    };
  }
  angular.module('scaffolding').directive('scColumn', ColumnDirective);
}(angular));