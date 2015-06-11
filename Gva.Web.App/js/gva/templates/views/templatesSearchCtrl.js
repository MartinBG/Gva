/*global angular*/
(function (angular) {
  'use strict';

  function TemplatesSearchCtrl(
    $scope,
    templates
  ) {
    $scope.templates = templates;
  }

  TemplatesSearchCtrl.$inject = [
    '$scope',
    'templates'
  ];

  TemplatesSearchCtrl.$resolve = {
    templates: [
      'WordTemplates',
      function (WordTemplates) {
        return WordTemplates.getTemplates().$promise;
      }
    ]
  };

  angular.module('gva').controller('TemplatesSearchCtrl', TemplatesSearchCtrl);
}(angular));