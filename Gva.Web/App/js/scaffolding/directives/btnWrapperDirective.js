// Usage: <sc-btn-wrapper btn-classes="" btn-actions=""></sc-btn-wrapper>
(function (angular) {
  'use strict';

  function BtnWrapperDirective() {
    function BtnWrapperController ($scope) {
      var btnActions = $scope.btnActions;

      this.getBtnAction = function (actionName) {
        return btnActions[actionName];
      };
    }

    function BtnWrapperCompile(tElement, tAttrs) {
      if (!tAttrs.btnClasses) {
        tAttrs.btnClasses = 'col-sm-3';
      }
    }

    return {
      restrict: 'E',
      replace: true,
      transclude: true,
      scope: {
        btnClasses: '@',
        btnActions: '='
      },
      templateUrl: 'scaffolding/templates/btnWrapperTemplate.html',
      controller: BtnWrapperController,
      compile: BtnWrapperCompile
    };
  }

  angular.module('scaffolding').directive('scBtnWrapper', BtnWrapperDirective);
}(angular));