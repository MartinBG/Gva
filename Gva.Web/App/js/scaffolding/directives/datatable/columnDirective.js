/*
Usage: <sc-column model-name="property"
        type="string|numeric|date"
        sortable="true|false"
        sorting="asc|desc"
        visible="true|false"
        class=""
        [title]>
        </sc-column>
*/

/*global angular, navigator*/
(function (angular, navigator) {
  'use strict';

  var msie = parseInt((/msie (\d+)/.exec(navigator.userAgent.toLowerCase()) || [])[1], 10);

  function ColumnDirective($rootScope) {
    return {
      restrict: 'E',
      require: '^scDatatable',
      transclude: true,
      compile : function (){
        return function(scope, iElement, iAttrs, scDatatable, childTranscludeFn){
          var tempScope = scope.$new(),
              childContent = childTranscludeFn(tempScope, function () { }),
              hasChildContent = childContent.length > 1 || (msie < 9 && childContent.length > 0),
              createCellFunc;
          childContent.remove();
          tempScope.$destroy();

          if (hasChildContent) {
            createCellFunc = function(nTd, sData, oData){
              var childScope = scope.$new(),
                  clone = childTranscludeFn(childScope, function(){ });

              childScope.data = sData;
              childScope.item = oData;

              angular.element(nTd).empty();
              angular.element(nTd).append(clone);

              if (!$rootScope.$$phase) {
                childScope.$digest();
              }
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
            defaultValue: iAttrs.defaultValue,
            width: iAttrs.width || null,
            'class': iAttrs['class']
          });
        };
      }
    };
  }

  ColumnDirective.$inject = ['$rootScope'];

  angular.module('scaffolding').directive('scColumn', ColumnDirective);
}(angular, navigator));
