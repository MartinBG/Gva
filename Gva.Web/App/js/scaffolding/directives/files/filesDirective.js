﻿// Usage: <sc-files ng-model="<model_name> [multiple]"> </sc-files>

/*global angular*/
(function (angular) {
  'use strict';

  angular.module('scaffolding')
    .constant('scFilesConfig', {
      fileUrl: '/api/file'
    })
    .directive('scFiles', function () {
      return {
        priority: 110,
        restrict: 'E',
        controller: 'scaffolding.FilesCtrl',
        require: ['scFiles', '?ngModel'],
        replace: true,
        templateUrl: 'scaffolding/directives/files/filesDirective.html',
        link: function link(scope, iElement, iAttrs, controllers) {
          var filesCtrl = controllers[0],
              ngModelCtrl = controllers[1];

          scope.$watch(function () {
            scope.isReadonly = !!iAttrs.readonly;
          });

          filesCtrl.setNgModelCtrl(ngModelCtrl);
        }
      };
    });
}(angular));
