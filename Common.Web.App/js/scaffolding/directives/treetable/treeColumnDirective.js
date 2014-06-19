/*
Usage: <sc-tree-column model-name="property"
        type="string|numeric|date"
        class=""
        [title]
        row-id="numeric"
        parent-id= "numeric"
        has-content="true|false">
      </sc-tree-column>
*/

/*global angular*/
(function (angular) {
  'use strict';

  function TreeColumnDirective() {
    return {
      restrict: 'E',
      require: '^scTreetable',
      transclude: true,
      link: function (scope, iElement, iAttrs, scTreetable, childTranscludeFn) {
        scTreetable.addColumn({
          transcludeFn: childTranscludeFn,
          hasContent: iAttrs.hasContent ? true : false,
          type: iAttrs.type,
          data: iAttrs.data,
          title: iAttrs.title,
          defaultValue: iAttrs.defaultValue,
          width: iAttrs.width || null,
          'class': iAttrs['class'],
          rowId: iAttrs.rowId,
          parentId: iAttrs.parentId
        });
      }
    };
  }

  angular.module('scaffolding').directive('scTreeColumn', TreeColumnDirective);
}(angular));
