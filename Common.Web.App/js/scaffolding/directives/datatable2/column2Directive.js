﻿/*
Usage: <sc-column2 model-name="property"
        type="string|numeric|date"
        sortable="true|false"
        visible="true|false"
        class=""
        [title]
        has-content="true|false">
      </sc-column>
*/

/*global angular*/
(function (angular) {
  'use strict';

  function Column2Directive() {
    return {
      restrict: 'E',
      require: '^scDatatable2',
      transclude: true,
      link: function (scope, iElement, iAttrs, scDatatable1, childTranscludeFn) {
        scDatatable1.addColumn({
          transcludeFn: childTranscludeFn,
          hasContent: iAttrs.hasContent ? true : false,
          type: iAttrs.type,
          data: iAttrs.data,
          title: iAttrs.title,
          sortable: iAttrs.sortable,
          visible: iAttrs.visible,
          defaultValue: iAttrs.defaultValue,
          width: iAttrs.width || null,
          'class': iAttrs['class']
        });
      }
    };
  }

  Column2Directive.$inject = [];

  angular.module('scaffolding').directive('scColumn2', Column2Directive);
}(angular));
