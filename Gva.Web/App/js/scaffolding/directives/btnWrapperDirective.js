// Usage: <div btn-wrapper btn-classes=""></div>
(function (angular) {
  'use strict';

  function BtnWrapperDirective() {
    return {
      restrict: 'A',
      replace: true,
      transclude: true,
      scope: {
        btnClasses: '@'
      },
      template: '<div class="labeless pull-right {{btnClasses}}" ng-transclude></div>'
    };
  }

  angular.module('scaffolding').directive('btnWrapper', BtnWrapperDirective);
}(angular));